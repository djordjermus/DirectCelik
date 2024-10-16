namespace DirectCelik.Model.Enum
{
	/// <summary>
	/// Error Code returned by an celikapi operation
	/// </summary>
	public enum ErrorCode
	{
		/// <summary />
		Ok                         =  0, 

		/// <summary />
		GeneralError               = -1, 

		/// <summary />
		InvalidParameter           = -2, 

		/// <summary />
		VersionNotSupported        = -3, 

		/// <summary />
		NotInitialized             = -4, 

		/// <summary />
		UnableToExecute            = -5, 

		/// <summary />
		ReaderError                = -6, 

		/// <summary />
		CardMissing                = -7, 

		/// <summary />
		CardUnknown                = -8, 

		/// <summary />
		CardMismatch               = -9, 

		/// <summary />
		UnableToOpenSession        = -10,

		/// <summary />
		DataMissing                = -11,

		/// <summary />
		CardSecformatCheckError    = -12,

		/// <summary />
		SecformatCheckCertError    = -13,

		/// <summary />
		InvalidPassword            = -14,

		/// <summary />
		PinBlocked                 = -15,
	}
}
