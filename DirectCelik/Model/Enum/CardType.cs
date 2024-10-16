namespace DirectCelik.Model.Enum
{
	/// <summary>
	/// Enumeration of different types of Electronic ID cards
	/// </summary>
	public enum CardType : int
	{
		/// <summary>
		/// Invalid card type / not an EID card.
		/// </summary>
		Invalid	= 0,

		/// <summary />
		ID2008	= 1,

		/// <summary />
		ID2014	= 2,

		/// <summary>
		/// Foreigner ID card
		/// </summary>
		IF2020	= 3,

		/// <summary>
		/// Residental permin
		/// </summary>
		RP2024	= 4,
	}
}
