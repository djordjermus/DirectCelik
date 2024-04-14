using System;

namespace DirectCelik.Interop.Interface
{
	internal unsafe interface ICelikApi
	{
		int SetOption(int nOptionID, IntPtr nOptionValue);
		int Startup(int nApiVersion);
		int Cleanup();
		int BeginRead(IntPtr szReader, IntPtr pnCardType);
		int EndRead();
		int ReadDocumentData(CelikApi.EID_DOCUMENT_DATA* pData);
		int ReadFixedPersonalData(CelikApi.EID_FIXED_PERSONAL_DATA* pData);
		int ReadVariablePersonalData(CelikApi.EID_VARIABLE_PERSONAL_DATA* pData);
		int ReadPortrait(CelikApi.EID_PORTRAIT* pData);
		int ReadCertificate(CelikApi.EID_CERTIFICATE* pData, int certificateType);
		int ChangePassword(IntPtr szOldPassword, IntPtr szNewPassword, int* pnTriesLeft);
		int VerifySignature(uint nSignatureID);
	}
}
