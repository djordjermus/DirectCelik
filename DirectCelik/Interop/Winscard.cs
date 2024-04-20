using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectCelik.Interop
{
	internal static class Winscard
	{
		[DllImport(WinscardDll, CallingConvention = CallingConvention.Winapi)]
		public static extern uint SCardEstablishContext(int scope, IntPtr reserved1, IntPtr reserved2, IntPtr phContext);

		[DllImport(WinscardDll, CallingConvention = CallingConvention.Winapi)]
		public static extern uint SCardListReadersW(IntPtr hContext, string mszGroups, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder mszReaders, ref int pcchReaders);

		[DllImport(WinscardDll, CallingConvention = CallingConvention.Winapi)]
		public static extern uint SCardReleaseContext(IntPtr phContext);



		#region CONSTANTS

		private const string WinscardDll				= "Winscard.dll";

		public const string SCARD_ALL_READERS			= "SCard$AllReaders\000";
		public const string SCARD_DEFAULT_READERS		= "SCard$DefaultReaders\000";
		public const string SCARD_LOCAL_READERS			= "SCard$LocalReaders\000";     // LEGACY
		public const string SCARD_SYSTEM_READERS		= "SCard$SystemReaders\000";    // LEGACY

		public const int SCARD_SCOPE_USER				= 0;
		public const int SCARD_SCOPE_SYSTEM				= 2;

		public const uint SCARD_S_SUCCESS				= 0x00000000;
		public const uint SCARD_E_NO_READERS_AVAILABLE	= 0x8010002E;
		public const uint SCARD_E_READER_UNAVAILABLE	= 0x80100017;

		#endregion
	}
}
