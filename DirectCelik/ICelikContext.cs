using DirectCelik.Model;
using DirectCelik.Model.Enum;
using System;

namespace DirectCelik
{
	public interface ICelikContext : IDisposable
	{
		CardType CardType { get; }

		Result<DocumentData> ReadDocumentData();
		Result<FixedPersonalData> ReadFixedPersonalData();
		Result<VariablePersonalData> ReadVariablePersonalData();
		Result<byte[]> ReadPortraitData();
	}
}
