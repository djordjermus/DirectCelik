using DirectCelik.Interop;
using System.Text;
using System;

namespace DirectCelik
{
	/// <summary>
	/// Utility static class for enumerating connected card readers
	/// </summary>
	public static class CardReaders
	{
		/// <summary>
		/// Method for enumerating connected card readers
		/// </summary>
		/// <returns>List of names of connected card readers</returns>
		public static unsafe string[] Readers()
		{
			try
			{
				IntPtr hCtx = IntPtr.Zero;
				try
				{
					var result = Winscard.SCardEstablishContext(Winscard.SCARD_SCOPE_SYSTEM, IntPtr.Zero, IntPtr.Zero, (IntPtr)(&hCtx));
					if (result != Winscard.SCARD_S_SUCCESS)
						return null;

					int length = 0;
					result = Winscard.SCardListReadersW(hCtx, null, null, ref length);
					if (result == Winscard.SCARD_E_NO_READERS_AVAILABLE)
						return new string[0];

					StringBuilder output = new StringBuilder(length + 1);
					result = Winscard.SCardListReadersW(hCtx, null, output, ref length);
					return output.ToString().Trim('\0').Split('\0');
				}
				finally
				{
					if (hCtx != IntPtr.Zero)
						Winscard.SCardReleaseContext(hCtx);
				}
			}
			catch
			{
				return null;
			}
		}
	}
}
