using DirectCelik.Interop;
using System;
using System.Threading;

namespace DirectCelik
{
	public sealed class Celik : IDisposable
	{
		private static readonly object _threadLock = new object();
		private static int _activeLifetimes = 0;

        public static bool IsStartupExecuted => _activeLifetimes > 0;
        public static int ActiveLifetimeCount => _activeLifetimes;

		public bool Disposed { get; private set; } = false;

        public static Celik Create() => new Celik();

        private Celik()
		{
            if (Interlocked.Increment(ref _activeLifetimes) == 1)
                CelikApi.Startup(4);
        }

		public void Execute(string reader, Action<ICelikSession> action)
		{
            lock (_threadLock)
            {
			    using (var session = new CelikSession(reader))
			    {
			    	action(session);
			    }
            }
		}

        public T Execute<T>(string reader, Func<ICelikSession, T> action)
        {
            lock (_threadLock)
            {
                using (var session = new CelikSession(reader))
                {
                    return action(session);
                }
            }
        }

        #region IDisposable

        public void Dispose()
        {
			lock (_threadLock)
			{
				GC.SuppressFinalize(this);
				Dispose(true);
			}
        }

		~Celik()
		{
			Dispose(false);
		}

        private void Dispose(bool disposing)
        {
			Disposed = true;
            if (Interlocked.Decrement(ref _activeLifetimes) == 0)
                CelikApi.Cleanup();
        }

        #endregion
    }
}
