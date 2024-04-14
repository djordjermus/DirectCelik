namespace DirectCelik.Model
{
	public class Result
	{
		public Enum.ErrorCode Error { get; set; }
		public bool Success => Error == Enum.ErrorCode.Ok;
	}

	public class Result<T> : Result
	{
		public T Data { get; set; }
	}
}
