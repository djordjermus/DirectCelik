using DirectCelik.Interop;
using DirectCelik.Model.Enum;
using System;
using System.Text;
namespace DirectCelik
{
	public sealed class CelikLifetime : IDisposable
	{
		private static uint _startupSpin = 0;
		public bool IsStartupExecuted => _startupSpin > 0;
		public bool IsDisposed { get; private set; }

		public static CelikLifetime Create()
		{
			try
			{
				if (_startupSpin == 0)
				{
					var result = CelikApi.Startup(4);
					if (result == ErrorCode.Ok)
						_startupSpin++;
					else
						return null;
				}
				else
				{
					_startupSpin++;
				}

				return new CelikLifetime();
			}
			catch
			{
				return null;
			}
		}

		public unsafe ICelikContext BeginTransaction(string reader = null)
		{
			if (CelikContext.InSession)
				throw new Exception("Cannot begin multiple transactions at one time!");

			int cardType;
			var readerBytes = reader is null ?
				null : Encoding.ASCII.GetBytes(reader);
			var result = CelikApi.BeginRead((IntPtr)(&readerBytes), (IntPtr)(&cardType));

			if (result != ErrorCode.Ok)
				throw new Exception("Failed to execute EIDBeginRead(LPCSTR)");

			return new CelikContext((CardType)cardType);
		}

		private CelikLifetime()
		{
			IsDisposed = false;
		}

		#region DISPOSABLE PATTERN

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		~CelikLifetime()
		{
			Dispose(false);
		}
		private void Dispose(bool disposing)
		{
			try
			{
				if (IsDisposed)
					return;

				if (_startupSpin == 1)
				{
					var result = CelikApi.Cleanup();
					if (result == ErrorCode.Ok)
					{
						_startupSpin--;
					}
				}
				else
				{
					_startupSpin--;
				}
			}
			catch { /* Silentlty continue */ }
		}

		#endregion
	}
}
