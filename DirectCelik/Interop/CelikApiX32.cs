using System;
using System.Runtime.InteropServices;
using DirectCelik.Interop.Interface;

namespace DirectCelik.Interop
{
	internal unsafe class CelikApiX32 : ICelikApi
	{
		private const string dllName = "CelikApiX32.dll";



		public int SetOption(int nOptionID, IntPtr nOptionValue) =>
			EidSetOption(nOptionID, nOptionValue);

		public int Startup(int nApiVersion) =>
			EidStartup(nApiVersion);
		public int Cleanup() =>
			EidCleanup();

		public int BeginRead(IntPtr szReader, IntPtr pnCardType) =>
			EidBeginRead(szReader, pnCardType);
		public int EndRead() =>
			EidEndRead();

		public int ReadDocumentData(CelikApi.EID_DOCUMENT_DATA* pData) =>
			EidReadDocumentData(pData);
		public int ReadFixedPersonalData(CelikApi.EID_FIXED_PERSONAL_DATA* pData) =>
			EidReadFixedPersonalData(pData);
		public int ReadVariablePersonalData(CelikApi.EID_VARIABLE_PERSONAL_DATA* pData) =>
			EidReadVariablePersonalData(pData);
		public int ReadPortrait(CelikApi.EID_PORTRAIT* pData) =>
			EidReadPortrait(pData);
		public int ReadCertificate(CelikApi.EID_CERTIFICATE* pData, int certificateType) =>
			EidReadCertificate(pData, certificateType);
		public int ChangePassword(IntPtr szOldPassword, IntPtr szNewPassword, int* pnTriesLeft) =>
			EidChangePassword(szOldPassword, szNewPassword, pnTriesLeft);
		public int VerifySignature(uint nSignatureID) =>
			EidVerifySignature(nSignatureID);



		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidSetOption(int nOptionID, IntPtr nOptionValue);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidStartup(int nApiVersion);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidCleanup();


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		public static unsafe extern int EidBeginRead(IntPtr szReader, IntPtr pnCardType);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidEndRead();


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidReadDocumentData(CelikApi.EID_DOCUMENT_DATA* pData);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidReadFixedPersonalData(CelikApi.EID_FIXED_PERSONAL_DATA* pData);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidReadVariablePersonalData(CelikApi.EID_VARIABLE_PERSONAL_DATA* pData);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidReadPortrait(CelikApi.EID_PORTRAIT* pData);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidReadCertificate(CelikApi.EID_CERTIFICATE* pData, int certificateType);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidChangePassword(IntPtr szOldPassword, IntPtr szNewPassword, int* pnTriesLeft);


		[DllImport(dllName, CallingConvention = CallingConvention.Winapi)]
		private static unsafe extern int EidVerifySignature(uint nSignatureID);
	}
}
