﻿using System;
using System.Threading;
using DirectCelik.Interop;
using DirectCelik.Interop.Interface;
using DirectCelik.Model;
using DirectCelik.Model.Enum;

namespace DirectCelik
{
	public sealed class Celik : IDisposable
	{
        private static ICelikApi _api = CelikApi.GetApiImplementation();
		private static readonly object _threadLock = new object();
		private static int _activeLifetimes = 0;

        private bool _disposed = false;

        private Celik()
		{
            lock (_threadLock)
            {
                if (Interlocked.Increment(ref _activeLifetimes) == 1)
                    _api.Startup(4);
            }
        }



        #region PUBLIC INTERFACE

        /// <summary>
        /// Returns true if any non disposed Celik instances exist. false otherwise.
        /// </summary>
        public static bool IsStartupExecuted => _activeLifetimes > 0;

        /// <summary>
        /// Returns the number of Celik instances currently not disposed.
        /// </summary>
        public static int ActiveLifetimeCount => _activeLifetimes;

        /// <summary>
        /// Returns true if the current instance of the Celik class is disposed.
        /// </summary>
        public bool Disposed => _disposed;

        /// <summary>
        /// Creates an instance of Celik class.
        /// Celik class manages the lifetime of the unmanaged celikapi library.
        /// When the first Celik class is created, it initializes the celikapi library.
        /// When the last one is disposed, it cleans up after the celikapi library.
        /// Multiple Celik instances may exist at any time.
        /// </summary>
        public static Celik Create() => new Celik();

        /// <summary>
        /// Conducts reading data from the ID, with an internally created and disposed instance of Celik class.
        /// If there are no other undisposed instances of Celik, the method will initialize and cleanup the library,
        /// Thus you should consider having a global instance created at the start of your program, and disposed at its end.
        /// </summary>
        /// <param name="reader">Name of the target card reader.</param>
        /// <param name="action">Action that will use ICelikSession instance to read data off of the card.</param>
        /// <returns>Result of end read operation.</returns>
        public static Result ExecuteStatic(string reader, Action<ICelikSession> action)
        {
            using (var celik = Create())
                return celik.Execute(reader, action);
        }

        /// <summary>
        /// Conducts reading data from the ID, with an internally created and disposed instance of Celik class.
        /// If there are no other undisposed instances of Celik, the method will initialize and cleanup the library,
        /// Thus you should consider having a global instance created at the start of your program, and disposed at its end.
        /// </summary>
        /// <param name="reader">Name of the target card reader.</param>
        /// <param name="function">Function that will use ICelikSession instance to read data off of the card.</param>
        /// <returns>Result of end read operation, with data returned from user function.</returns>
        public static Result<T> ExecuteStatic<T>(string reader, Func<ICelikSession, T> function)
        {
            using (var celik = Create())
                return celik.Execute(reader, function);
        }

        /// <summary>
        /// Conducts reading data from the ID.
        /// </summary>
        /// <param name="reader">Name of the target card reader.</param>
        /// <param name="action">Action that will use ICelikSession instance to read data off of the card.</param>
        /// <returns>Result of end read operation.</returns>
        public Result Execute(string reader, Action<ICelikSession> action)
		{
            CelikSession session = default;
            lock (_threadLock)
            {
			    using (session = new CelikSession(reader))
			    {
			    	action(session);
			    }
            }
            return new Result { Error = session?.SessionEndErrorCode ?? ErrorCode.UnableToOpenSession };
		}

        /// <summary>
        /// Conducts reading data from the ID.
        /// </summary>
        /// <param name="reader">Name of the target card reader.</param>
        /// <param name="function">Function that will use ICelikSession instance to read data off of the card.</param>
        /// <returns>Result of end read operation, with data returned from user function.</returns>
        public Result<T> Execute<T>(string reader, Func<ICelikSession, T> function)
        {
            CelikSession session = default;
            T result = default;
            lock (_threadLock)
            {
                using (session = new CelikSession(reader))
                {
                    result = function(session);
                }
            }

            return new Result<T>()
            {
                Error = session.SessionEndErrorCode,
                Data = result
            };
        }

        #endregion



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
            try
            {
                Dispose(false);
            }
            catch { }
        }
		

        private void Dispose(bool disposing)
        {
			_disposed = true;
            if (Interlocked.Decrement(ref _activeLifetimes) == 0)
                _api.Cleanup();
        }

        #endregion
    }
}
