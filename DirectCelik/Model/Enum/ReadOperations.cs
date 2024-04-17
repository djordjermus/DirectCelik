using System;

namespace DirectCelik.Model.Enum
{
	[Flags]
	public enum ReadOperations
	{
		None				 = 0x00,
		DocumentData		 = 0x01,
		FixedPersonalData	 = 0x02,
		VariablePersonalData = 0x04,
		Portrait			 = 0x08,
		All					 = 0x0F,
	}
}
