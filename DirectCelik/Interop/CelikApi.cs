using DirectCelik.Interop.Interface;
using System.Runtime.InteropServices;
namespace DirectCelik.Interop
{
	internal static class CelikApi
	{
		private static readonly ICelikApi _api = InstantiateCelikApi();

		private static ICelikApi InstantiateCelikApi()
		{
			try
			{
				var result = new CelikApiDefault();
				result.Cleanup();
				return result;
			}
			catch { }
			try
			{
				var result = new CelikApiX64();
				result.Cleanup();
				return result;
			}
			catch { }
			try
			{
				var result = new CelikApiX32();
				result.Cleanup();
				return result;
			}
			catch { }
			throw new System.Exception("No appropriate CelikApi.dll. both CelikApiX32.dll and CelikApiX64.dll should be located in the project output!");
		}



		public static ICelikApi GetApiImplementation() => _api;

		#region STRUCTS

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public unsafe struct EID_DOCUMENT_DATA
		{
			public fixed byte docRegNo[EID_MAX_DocRegNo];
			public int docRegNoSize;
			public fixed byte documentType[EID_MAX_DocumentType];
			public int documentTypeSize;
			public fixed byte issuingDate[EID_MAX_IssuingDate];
			public int issuingDateSize;
			public fixed byte expiryDate[EID_MAX_ExpiryDate];
			public int expiryDateSize;
			public fixed byte issuingAuthority[EID_MAX_IssuingAuthority];
			public int issuingAuthoritySize;
			public fixed byte documentSerialNumber[EID_MAX_DocumentSerialNumber];
			public int documentSerialNumberSize;
			public fixed byte chipSerialNumber[EID_MAX_ChipSerialNumber];
			public int chipSerialNumberSize;
			public fixed byte documentName[EID_MAX_DocumentName];
			public int documentNameSize;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public unsafe struct EID_FIXED_PERSONAL_DATA
		{
			public fixed byte personalNumber[EID_MAX_PersonalNumber];
			public int personalNumberSize;
			public fixed byte surname[EID_MAX_Surname];
			public int surnameSize;
			public fixed byte givenName[EID_MAX_GivenName];
			public int givenNameSize;
			public fixed byte parentGivenName[EID_MAX_ParentGivenName];
			public int parentGivenNameSize;
			public fixed byte sex[EID_MAX_Sex];
			public int sexSize;
			public fixed byte placeOfBirth[EID_MAX_PlaceOfBirth];
			public int placeOfBirthSize;
			public fixed byte stateOfBirth[EID_MAX_StateOfBirth];
			public int stateOfBirthSize;
			public fixed byte dateOfBirth[EID_MAX_DateOfBirth];
			public int dateOfBirthSize;
			public fixed byte communityOfBirth[EID_MAX_CommunityOfBirth];
			public int communityOfBirthSize;
			public fixed byte statusOfForeigner[EID_MAX_StatusOfForeigner];
			public int statusOfForeignerSize;
			public fixed byte nationalityFull[EID_MAX_NationalityFull];
			public int nationalityFullSize;
			public fixed byte purposeOfStay[EID_MAX_PurposeOfStay];
			public int purposeOfStaySize;
			public fixed byte eNote[EID_MAX_ENote];
			public int eNoteSize;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public unsafe struct EID_VARIABLE_PERSONAL_DATA
		{
			public fixed byte state[EID_MAX_State];
			public int stateSize;
			public fixed byte community[EID_MAX_Community];
			public int communitySize;
			public fixed byte place[EID_MAX_Place];
			public int placeSize;
			public fixed byte street[EID_MAX_Street];
			public int streetSize;
			public fixed byte houseNumber[EID_MAX_HouseNumber];
			public int houseNumberSize;
			public fixed byte houseLetter[EID_MAX_HouseLetter];
			public int houseLetterSize;
			public fixed byte entrance[EID_MAX_Entrance];
			public int entranceSize;
			public fixed byte floor[EID_MAX_Floor];
			public int floorSize;
			public fixed byte apartmentNumber[EID_MAX_ApartmentNumber];
			public int apartmentNumberSize;
			public fixed byte addressDate[EID_MAX_AddressDate];
			public int addressDateSize;
			public fixed byte addressLabel[EID_MAX_AddressLabel];
			public int addressLabelSize;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public unsafe struct EID_PORTRAIT
		{
			public fixed byte portrait[EID_MAX_Portrait];
			public int portraitSize;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public unsafe struct EID_CERTIFICATE
		{
			public fixed byte certificate[EID_MAX_Certificate];
			public int certificateSize;
		}

		#endregion

		#region CONSTANTS

		public const int EID_MAX_DocRegNo = 9;
		public const int EID_MAX_DocumentType = 2;
		public const int EID_MAX_IssuingDate = 10;
		public const int EID_MAX_ExpiryDate = 10;
		public const int EID_MAX_IssuingAuthority = 100;
		public const int EID_MAX_DocumentSerialNumber = 10;
		public const int EID_MAX_ChipSerialNumber = 14;
		public const int EID_MAX_DocumentName = 100;

		public const int EID_MAX_PersonalNumber = 13;
		public const int EID_MAX_Surname = 200;
		public const int EID_MAX_GivenName = 200;
		public const int EID_MAX_ParentGivenName = 200;
		public const int EID_MAX_Sex = 2;
		public const int EID_MAX_PlaceOfBirth = 200;
		public const int EID_MAX_StateOfBirth = 200;
		public const int EID_MAX_DateOfBirth = 10;
		public const int EID_MAX_CommunityOfBirth = 200;
		public const int EID_MAX_StatusOfForeigner = 200;
		public const int EID_MAX_NationalityFull = 200;
		public const int EID_MAX_PurposeOfStay = 200;
		public const int EID_MAX_ENote = 200;

		public const int EID_MAX_State = 100;
		public const int EID_MAX_Community = 200;
		public const int EID_MAX_Place = 200;
		public const int EID_MAX_Street = 200;
		public const int EID_MAX_HouseNumber = 20;
		public const int EID_MAX_HouseLetter = 8;
		public const int EID_MAX_Entrance = 10;
		public const int EID_MAX_Floor = 6;
		public const int EID_MAX_ApartmentNumber = 12;
		public const int EID_MAX_AddressDate = 10;
		public const int EID_MAX_AddressLabel = 60;

		public const int EID_MAX_Portrait = 7700;

		public const int EID_MAX_Certificate = 2048;

		public const int EID_CARD_ID2008	= 1;
		public const int EID_CARD_ID2014	= 2;
		public const int EID_CARD_IF2020	= 3;
		public const int EID_CARD_RP2024	= 4;
		

		public const int EID_O_KEEP_CARD_CLOSED = 1;
		

		public const int EID_Cert_MoiIntermediateCA	= 1;
		public const int EID_Cert_User1				= 2;
		public const int EID_Cert_User2				= 3;
		public const int EID_Cert_SIG_FIXED			= 4;
		public const int EID_Cert_SIG_VARIABLE		= 5;


		public const int EID_SIG_CARD		= 1;
		public const int EID_SIG_FIXED		= 2;
		public const int EID_SIG_VARIABLE	= 3;
		public const int EID_SIG_PORTRAIT	= 4;
		
		public const int EID_OK                            =  0; 
		public const int EID_E_GENERAL_ERROR               = -1; 
		public const int EID_E_INVALID_PARAMETER           = -2; 
		public const int EID_E_VERSION_NOT_SUPPORTED       = -3; 
		public const int EID_E_NOT_INITIALIZED             = -4; 
		public const int EID_E_UNABLE_TO_EXECUTE           = -5; 
		public const int EID_E_READER_ERROR                = -6; 
		public const int EID_E_CARD_MISSING                = -7; 
		public const int EID_E_CARD_UNKNOWN                = -8; 
		public const int EID_E_CARD_MISMATCH               = -9; 
		public const int EID_E_UNABLE_TO_OPEN_SESSION      = -10;
		public const int EID_E_DATA_MISSING                = -11;
		public const int EID_E_CARD_SECFORMAT_CHECK_ERROR  = -12;
		public const int EID_E_SECFORMAT_CHECK_CERT_ERROR  = -13;
		public const int EID_E_INVALID_PASSWORD            = -14;
		public const int EID_E_PIN_BLOCKED                 = -15;
		

		#endregion
	}
}
