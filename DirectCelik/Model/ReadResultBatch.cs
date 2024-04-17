using DirectCelik.Model.Enum;

namespace DirectCelik.Model
{
	public class ReadResultBatch
	{
		public Result<CardType> CardType { get; set; }
		public Result<DocumentData> DocumentData { get; set; }
		public Result<FixedPersonalData> FixedPersonalData { get; set; }
		public Result<VariablePersonalData> VariablePersonalData { get; set; }
		public Result<byte[]> Portrait { get; set; }
	}
}
