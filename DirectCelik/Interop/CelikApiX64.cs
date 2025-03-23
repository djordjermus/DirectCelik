using System;
using System.Runtime.InteropServices;
using DirectCelik.Interop.Interface;
using DirectCelik.Model.Enum;

namespace DirectCelik.Interop
{
	internal unsafe class CelikApiX64 : ICelikApi
	{
		private const string dllName = "CelikApiX64.dll";



		public ErrorCode SetOption(int nOptionID, IntPtr nOptionValue) =>
			EidSetOption(nOptionID, nOptionValue);

		public ErrorCode Startup(int nApiVersion) =>
			EidStartup(nApiVersion);
		public ErrorCode Cleanup() =>
			EidCleanup();

		public ErrorCode BeginRead(string szReader, ref CardType pnCardType) =>
			EidBeginRead(szReader, ref pnCardType);
		public ErrorCode EndRead() =>
			EidEndRead();

		public ErrorCode ReadDocumentData(ref CelikApi.EID_DOCUMENT_DATA pData) =>
			EidReadDocumentData(ref pData);
		public ErrorCode ReadFixedPersonalData(ref CelikApi.EID_FIXED_PERSONAL_DATA pData) =>
			EidReadFixedPersonalData(ref pData);
		public ErrorCode ReadVariablePersonalData(ref CelikApi.EID_VARIABLE_PERSONAL_DATA pData) =>
			EidReadVariablePersonalData(ref pData);
		public ErrorCode ReadPortrait(ref CelikApi.EID_PORTRAIT pData) =>
			EidReadPortrait(ref pData);
		public ErrorCode ReadCertificate(ref CelikApi.EID_CERTIFICATE pData, int certificateType) =>
			EidReadCertificate(ref pData, certificateType);
		public ErrorCode ChangePassword(IntPtr szOldPassword, IntPtr szNewPassword, ref int pnTriesLeft) =>
			EidChangePassword(szOldPassword, szNewPassword, ref pnTriesLeft);
		public ErrorCode VerifySignature(uint nSignatureID) =>
			EidVerifySignature(nSignatureID);



		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidSetOption(int nOptionID, IntPtr nOptionValue);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidStartup(int nApiVersion);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidCleanup();


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		public static unsafe extern ErrorCode EidBeginRead([MarshalAs(UnmanagedType.LPStr)]string szReader, ref CardType pnCardType);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidEndRead();


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidReadDocumentData(ref CelikApi.EID_DOCUMENT_DATA pData);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidReadFixedPersonalData(ref CelikApi.EID_FIXED_PERSONAL_DATA pData);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidReadVariablePersonalData(ref CelikApi.EID_VARIABLE_PERSONAL_DATA pData);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidReadPortrait(ref CelikApi.EID_PORTRAIT pData);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidReadCertificate(ref CelikApi.EID_CERTIFICATE pData, int certificateType);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidChangePassword(IntPtr szOldPassword, IntPtr szNewPassword, ref int pnTriesLeft);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern ErrorCode EidVerifySignature(uint nSignatureID);
	}
}
