using DirectCelik.Model;
using DirectCelik.Model.Enum;

namespace DirectCelik
{
	/// <summary>
	/// Provides interface for celikapi operations related to reading data off of an ID card.
	/// </summary>
	public interface ICelikSession
	{
		/// <summary>
		/// Result of the EidBeginRead call.
		/// </summary>
		Result<CardType> SessionBeginResult { get; }



		/// <summary>
		/// Reads document data.
		/// </summary>
		/// <returns>Document data wrapped in a result.</returns>
		Result<DocumentData> ReadDocumentData();

		/// <summary>
		/// Reads fixed personal data.
		/// </summary>
		/// <returns>Fixed personal data wrapped in a result.</returns>
		Result<FixedPersonalData> ReadFixedPersonalData();

		/// <summary>
		/// Reads variable personal data.
		/// </summary>
		/// <returns>Variable personal data wrapped in a result.</returns>
		Result<VariablePersonalData> ReadVariablePersonalData();

		/// <summary>
		/// Reads portrait data.
		/// </summary>
		/// <returns>Portrait data wrapped in a result.</returns>
		Result<byte[]> ReadPortrait();



		/// <summary>
		/// Verifies document data.
		/// </summary>
		/// <returns>Verification result.</returns>
		Result VerifyDocumentData();

		/// <summary>
		/// Verifies fixed personal data.
		/// </summary>
		/// <returns>Verification result.</returns>
		Result VerifyFixedPersonalData();

		/// <summary>
		/// Verifies variable personal data.
		/// </summary>
		/// <returns>Verification result.</returns>
		Result VerifyVariablePersonalData();

		/// <summary>
		/// ID2008/APOLLO CARDS ONLY
		/// Verifies portrait data.
		/// </summary>
		/// <returns>Verification result.</returns>
		Result VerifyPortrait();

		/// <summary>
		/// ID2008/APOLLO CARDS ONLY
		/// Reads the signing certificate for the two other certificates.
		/// </summary>
		/// <returns>x.509 certificate wrapped in a result.</returns>
		Result<byte[]> ReadMoiIntermediateCACertificate();

		/// <summary>
		/// ID2008/APOLLO CARDS ONLY
		/// Reads the owner authentification certificate.
		/// </summary>
		/// <returns>x.509 certificate wrapped in a result.</returns>
		Result<byte[]> ReadAuthentificationCertificate();



		/// <summary>
		/// ID2008/APOLLO CARDS ONLY
		/// Reads the owner signing certificate.
		/// </summary>
		/// <returns>x.509 certificate wrapped in a result.</returns>
		Result<byte[]> ReadSignatureCertificate();
	}
}
