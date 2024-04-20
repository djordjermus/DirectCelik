using DirectCelik.Interop;
using DirectCelik.Model.Enum;
using System;
using System.Text;
using System.Threading;
namespace DirectCelik
{
	public sealed class Celik : IDisposable
	{
		private static object _lock = new object();
		private static int _startupSpin = 0;
		private static int _sessionSpin = 0;
		public bool IsStartupExecuted => _startupSpin > 0;
		public bool IsDisposed { get; private set; }

		public static Celik Create()
		{
			try
			{
				if (Interlocked.Increment(ref _startupSpin) == 1)
					CelikApi.Startup(4);
				
				return new Celik();
			}
			catch
			{
				return null;
			}
		}

		public unsafe void Execute(string reader, Action<ICelikSession> action)
		{
			try
			{
				lock (_lock)
				{
					if (Interlocked.Increment(ref _sessionSpin) > 1)
					{
						throw new Exception("Session already in progress");
					}

					CardType cardType = CardType.Invalid;
					var error = CelikApi.BeginRead(reader, ref cardType);

					using (var session = new CelikSession(error, cardType))
					{
						action(session);
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref _sessionSpin);
				CelikApi.EndRead();
			}
		}

		public unsafe T Execute<T>(string reader, Func<ICelikSession, T> function)
		{
			try
			{
				lock (_lock)
				{
					if (Interlocked.Increment(ref _sessionSpin) > 1)
					{
						throw new Exception("Session already in progress");
					}

					CardType cardType = CardType.Invalid;
					var error = CelikApi.BeginRead(reader, ref cardType);

					using (var session = new CelikSession(error, cardType))
					{
						return function(session);
					}
				}
			}
			finally
			{
				CelikApi.EndRead();
			}
		}



		private Celik()
		{
			IsDisposed = false;
		}

		#region DISPOSABLE PATTERN

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Celik()
		{
			lock (_lock)
			{	// No dispose while reading data
				Dispose(false);
			}
		}

		private void Dispose(bool disposing)
		{
			try
			{
				if (IsDisposed)
					return;

				if (Interlocked.Decrement(ref _startupSpin) == 0)
					CelikApi.Cleanup();
			}
			catch { /* Silentlty continue */ }
		}

		#endregion
	}
}
