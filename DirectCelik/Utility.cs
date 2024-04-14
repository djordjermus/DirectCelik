using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectCelik
{
	internal static class Utility
	{
		private static readonly byte[] emptyByteArray = new byte[0];

		public static unsafe byte[] ReadBytes(byte* data, int count)
		{
			if (data == null || count == 0)
				return emptyByteArray;

			var result = new byte[count];
			Marshal.Copy((IntPtr)data, result, 0, count);
			return result;
		}

		public static unsafe string Decode(byte* data, int count) =>
			Decode(ReadBytes(data, count));

		public static string Decode(byte[] data) =>
			Encoding.UTF8.GetString(data);
	}
}
