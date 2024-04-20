using DirectCelik.Interop;
using DirectCelik.Model;
using DirectCelik.Model.Enum;
using System;
using System.Runtime.InteropServices;

namespace DirectCelik
{
	internal class CelikSession : ICelikSession, IDisposable
	{
		public bool Disposed { get; private set; } = false;
		public Result<CardType> SessionBeginResult { get; private set; }

		public CelikSession(ErrorCode eidBeginReadResult, CardType cardType)
		{
			SessionBeginResult = new Result<CardType>() { Data = cardType, Error = eidBeginReadResult };
		}



		public unsafe Result<DocumentData> ReadDocumentData()
		{
			ThrowIfDisposed();
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_DOCUMENT_DATA();
				result = CelikApi.ReadDocumentData(ref data);

				if (result != ErrorCode.Ok)
					return new Result<DocumentData>() { Error = result };

				var output = new DocumentData();
				output.docRegNo				= Utility.Decode(data.docRegNo,				data.docRegNoSize);
				output.documentType			= Utility.Decode(data.documentType,			data.documentTypeSize);
				output.issuingDate			= Utility.Decode(data.issuingDate,			data.issuingDateSize);
				output.expiryDate			= Utility.Decode(data.expiryDate,			data.expiryDateSize);
				output.issuingAuthority		= Utility.Decode(data.issuingAuthority,		data.issuingAuthoritySize);
				output.documentSerialNumber = Utility.Decode(data.documentSerialNumber,	data.documentSerialNumberSize);
				output.chipSerialNumber		= Utility.Decode(data.chipSerialNumber,		data.chipSerialNumberSize);
				output.documentName			= Utility.Decode(data.documentName,			data.documentNameSize);

				return new Result<DocumentData>() { Error = result, Data = output };
			}
			catch
			{
				return new Result<DocumentData>()
				{
					Error = ErrorCode.GeneralError
				};
			}
		}

		public unsafe Result<FixedPersonalData> ReadFixedPersonalData()
		{
			ThrowIfDisposed();
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_FIXED_PERSONAL_DATA();
				result = CelikApi.ReadFixedPersonalData(ref data);
				
				if (result != ErrorCode.Ok)
					return new Result<FixedPersonalData>() { Error = result };

				var output = new FixedPersonalData();
				output.personalNumber		= Utility.Decode(data.personalNumber,		data.personalNumberSize);
				output.surname				= Utility.Decode(data.surname,				data.surnameSize);
				output.givenName			= Utility.Decode(data.givenName,			data.givenNameSize);
				output.parentGivenName		= Utility.Decode(data.parentGivenName,		data.parentGivenNameSize);
				output.sex					= Utility.Decode(data.sex,					data.sexSize);
				output.placeOfBirth			= Utility.Decode(data.placeOfBirth,			data.placeOfBirthSize);
				output.stateOfBirth			= Utility.Decode(data.stateOfBirth,			data.stateOfBirthSize);
				output.dateOfBirth			= Utility.Decode(data.dateOfBirth,			data.dateOfBirthSize);
				output.communityOfBirth		= Utility.Decode(data.communityOfBirth,		data.communityOfBirthSize);
				output.statusOfForeigner	= Utility.Decode(data.statusOfForeigner,	data.statusOfForeignerSize);
				output.nationalityFull		= Utility.Decode(data.nationalityFull,		data.nationalityFullSize);
				output.purposeOfStay		= Utility.Decode(data.purposeOfStay,		data.purposeOfStaySize);
				output.eNote				= Utility.Decode(data.eNote,				data.eNoteSize);

				return new Result<FixedPersonalData>() { Error = result, Data = output };
				
			}
			catch
			{
				return new Result<FixedPersonalData>()
				{
					Error = ErrorCode.GeneralError
				};
			}
		}

		public unsafe Result<VariablePersonalData> ReadVariableParsonalData()
		{
			ThrowIfDisposed();
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_VARIABLE_PERSONAL_DATA();
				result = CelikApi.ReadVariablePersonalData(ref data);
				
				if (result != ErrorCode.Ok)
					return new Result<VariablePersonalData>() { Error = result };

				var output = new VariablePersonalData();
				output.state			= Utility.Decode(data.state,			data.stateSize);
				output.community		= Utility.Decode(data.community,		data.communitySize);
				output.place			= Utility.Decode(data.place,			data.placeSize);
				output.street			= Utility.Decode(data.street,			data.streetSize);
				output.houseNumber		= Utility.Decode(data.houseNumber,		data.houseNumberSize);
				output.houseLetter		= Utility.Decode(data.houseLetter,		data.houseLetterSize);
				output.entrance			= Utility.Decode(data.entrance,			data.entranceSize);
				output.floor			= Utility.Decode(data.floor,			data.floorSize);
				output.apartmentNumber	= Utility.Decode(data.apartmentNumber,	data.apartmentNumberSize);
				output.addressDate		= Utility.Decode(data.addressDate,		data.addressDateSize);
				output.addressLabel		= Utility.Decode(data.addressLabel,		data.addressLabelSize);

				return new Result<VariablePersonalData>() { Error = result, Data = output };
			}
			catch
			{
				return new Result<VariablePersonalData>()
				{
					Error = ErrorCode.GeneralError
				};
			}
		}

		public unsafe Result<byte[]> ReadPortrait()
		{
			ThrowIfDisposed();
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_PORTRAIT();
				result = CelikApi.ReadPortrait(ref data);

				if (result != ErrorCode.Ok)
					return new Result<byte[]>() { Error = result };

				var output = new byte[data.portraitSize];
				Marshal.Copy((IntPtr)data.portrait, output, 0, data.portraitSize);

				return new Result<byte[]>() { Error = result, Data = output };
			}
			catch
			{
				return new Result<byte[]>()
				{
					Error = ErrorCode.GeneralError
				};
			}
		}



		public Result VerifyDocumentData()
		{
			ThrowIfDisposed();
			return new Result { Error = CelikApi.VerifySignature(CelikApi.EID_SIG_CARD) };

		}

		public Result VerifyFixedPersonalData()
		{
			ThrowIfDisposed();
			return new Result { Error = CelikApi.VerifySignature(CelikApi.EID_SIG_FIXED) };
		}

		public Result VerifyVariablePersonalData()
		{
			ThrowIfDisposed();
			return new Result { Error = CelikApi.VerifySignature(CelikApi.EID_SIG_VARIABLE) };
		}

		public Result VerifyPortrait()
		{
			ThrowIfDisposed();
			return new Result { Error = CelikApi.VerifySignature(CelikApi.EID_SIG_PORTRAIT) };
		}



		public unsafe Result<byte[]> ReadMoiIntermediateCACertificate() =>
			ReadCertificate(CelikApi.EID_Cert_MoiIntermediateCA);

		public unsafe Result<byte[]> ReadAuthentificationCertificate() =>
			ReadCertificate(CelikApi.EID_Cert_User1);

		public unsafe Result<byte[]> ReadSignatureCertificate() =>
			ReadCertificate(CelikApi.EID_Cert_User2);



		private unsafe Result<byte[]> ReadCertificate(int certificateId)
		{
			ThrowIfDisposed();
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_CERTIFICATE();
				result = CelikApi.ReadCertificate(ref data, certificateId);

				if (result != ErrorCode.Ok)
					return new Result<byte[]>() { Error = result };

				var output = new byte[data.certificateSize];
				Marshal.Copy((IntPtr)data.certificate, output, 0, data.certificateSize);

				return new Result<byte[]>() { Error = result, Data = output };
			}
			catch
			{
				return new Result<byte[]>()
				{
					Error = ErrorCode.GeneralError
				};
			}
		}

		private void ThrowIfDisposed()
		{
			if (Disposed)
				throw new ObjectDisposedException(typeof(ICelikSession).FullName);
		}

		public void Dispose() => Disposed = true;
	}
}
