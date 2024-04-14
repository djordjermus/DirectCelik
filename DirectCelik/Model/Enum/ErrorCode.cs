namespace DirectCelik.Model.Enum
{
	public enum ErrorCode
	{
		Ok                         =  0, 
		GeneralError               = -1, 
		InvalidParameter           = -2, 
		VersionNotSupported        = -3, 
		NotInitialized             = -4, 
		UnableToExecute            = -5, 
		ReaderError                = -6, 
		CardMissing                = -7, 
		CardUnknown                = -8, 
		CardMismatch               = -9, 
		UnableToOpenSession        = -10,
		DataMissing                = -11,
		CardSecformatCheckError    = -12,
		SecformatCheckCertError    = -13,
		InvalidPassword            = -14,
		PinBlocked                 = -15,
	}
}
