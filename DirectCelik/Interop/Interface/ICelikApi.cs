using DirectCelik.Model.Enum;
using System;

namespace DirectCelik.Interop.Interface
{
	internal unsafe interface ICelikApi
	{
		ErrorCode SetOption(int nOptionID, IntPtr nOptionValue);
		ErrorCode Startup(int nApiVersion);
		ErrorCode Cleanup();
		ErrorCode BeginRead(string szReader, ref CardType pnCardType);
		ErrorCode EndRead();
		ErrorCode ReadDocumentData(ref CelikApi.EID_DOCUMENT_DATA pData);
		ErrorCode ReadFixedPersonalData(ref CelikApi.EID_FIXED_PERSONAL_DATA pData);
		ErrorCode ReadVariablePersonalData(ref CelikApi.EID_VARIABLE_PERSONAL_DATA pData);
		ErrorCode ReadPortrait(ref CelikApi.EID_PORTRAIT pData);
		ErrorCode ReadCertificate(ref CelikApi.EID_CERTIFICATE pData, int certificateType);
		ErrorCode ChangePassword(IntPtr szOldPassword, IntPtr szNewPassword, ref int pnTriesLeft);
		ErrorCode VerifySignature(uint nSignatureID);
	}
}
