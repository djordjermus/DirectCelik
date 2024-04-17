using DirectCelik.Interop;
using DirectCelik.Model;
using DirectCelik.Model.Enum;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
namespace DirectCelik
{
	public sealed class Celik : IDisposable
	{
		private static object _lock = new object();
		private static int _startupSpin = 0;
		public bool IsStartupExecuted => _startupSpin > 0;
		public bool IsDisposed { get; private set; }



		public static Celik Create()
		{
			try
			{
				if (Interlocked.Increment(ref _startupSpin) == 1)
					CelikApi.Startup(4);
				
				return new Celik();
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Reads data from the default card reader.
		/// </summary>
		/// <param name="readOperations">Read operations to perform.</param>
		/// <returns>Data obtained from the card</returns>
		public unsafe ReadResultBatch Read(ReadOperations readOperations = ReadOperations.All) =>
			Read(null, readOperations);

		/// <summary>
		/// Reads data from the specified card reader.
		/// </summary>
		/// <param name="reader">Target reader.</param>
		/// <param name="readOperations">Read operations to perform.</param>
		/// <returns>Data obtained from the card</returns>
		public unsafe ReadResultBatch Read(string reader, ReadOperations readOperations = ReadOperations.All)
		{
			lock (_lock)
			{
				ThrowIfDisposed();
				try
				{
					int cardType = 0;
					var readerBytes = reader is null ?
						null : Encoding.ASCII.GetBytes(reader);

#pragma warning disable CS8500
					var error = CelikApi.BeginRead((IntPtr)(&readerBytes), (IntPtr)(&cardType));
#pragma warning restore CS8500
					var result = new ReadResultBatch();
					result.CardType = new Result<CardType>() { Data = (CardType)cardType, Error = error };

					if ((readOperations & ReadOperations.DocumentData) == ReadOperations.DocumentData)
						result.DocumentData = ReadDocumentData();
					if ((readOperations & ReadOperations.FixedPersonalData) == ReadOperations.FixedPersonalData)
						result.FixedPersonalData = ReadFixedPersonalData();
					if ((readOperations & ReadOperations.VariablePersonalData) == ReadOperations.VariablePersonalData)
						result.VariablePersonalData = ReadVariablePersonalData();
					if ((readOperations & ReadOperations.Portrait) == ReadOperations.Portrait)
						result.Portrait = ReadPortrait();

					return result;
				}
				finally
				{
					CelikApi.EndRead();
				}
			}
		}



		private static unsafe Result<DocumentData> ReadDocumentData()
		{
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_DOCUMENT_DATA();
				result = CelikApi.ReadDocumentData(&data);

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

		private static unsafe Result<FixedPersonalData> ReadFixedPersonalData()
		{
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_FIXED_PERSONAL_DATA();
				result = CelikApi.ReadFixedPersonalData(&data);
				
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

		private static unsafe Result<VariablePersonalData> ReadVariablePersonalData()
		{
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_VARIABLE_PERSONAL_DATA();
				result = CelikApi.ReadVariablePersonalData(&data);
				
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

		private static unsafe Result<byte[]> ReadPortrait()
		{
			try
			{
				ErrorCode result;
				var data = new CelikApi.EID_PORTRAIT();
				result = CelikApi.ReadPortrait(&data);
				
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



		private Celik()
		{
			IsDisposed = false;
		}

		#region DISPOSABLE PATTERN

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Celik()
		{
			lock (_lock)
			{	// No dispose while reading data
				Dispose(false);
			}
		}

		private void Dispose(bool disposing)
		{
			try
			{
				if (IsDisposed)
					return;

				if (Interlocked.Decrement(ref _startupSpin) == 0)
					CelikApi.Cleanup();
			}
			catch { /* Silentlty continue */ }
		}

		private void ThrowIfDisposed()
		{
			if (IsDisposed)
				throw new ObjectDisposedException(typeof(Celik).FullName);
		}

		#endregion
	}
}
