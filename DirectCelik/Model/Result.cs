using DirectCelik.Model.Enum;

namespace DirectCelik.Model
{
	/// <summary/>
	public class Result
	{
		/// <summary>
		/// Error code resulting from the celikapi operation.
		/// </summary>
		public ErrorCode Error { get; set; }

		/// <summary>
		/// Celikapi operation result in terms of success or failiure.
		/// </summary>
		public bool Success =>
			Error == ErrorCode.Ok;

		/// <summary>
		/// Returns a successful result.
		/// </summary>
		/// <returns>A successful result</returns>
		public static Result Ok() =>
			new Result { Error = ErrorCode.Ok };

		/// <summary>
		/// Returns a successful result.
		/// </summary>
		/// <typeparam name="T">Associated data type.</typeparam>
		/// <param name="data">Data wrapped in the result.</param>
		/// <returns>A successful result with data.</returns>
		public static Result<T> Ok<T>(T data) =>
			new Result<T> { Error = ErrorCode.Ok, Data = data };

		/// <summary>
		/// Returns a result based on given error code.
		/// </summary>
		/// <param name="error">Error code assigned to the result.</param>
		/// <returns>Result based on the given error code, with data.</returns>
		public static Result From(ErrorCode error) =>
			new Result { Error = error };

		/// <summary>
		/// Returns a result based on given error code.
		/// </summary>
		/// <typeparam name="T">Associated data type.</typeparam>
		/// <param name="error">Error code assigned to the result.</param>
		/// <param name="data">Data wrapped in the result.</param>
		/// <returns>Result based on the given error code, with data.</returns>
		public static Result<T> From<T>(ErrorCode error, T data) =>
			new Result<T> { Error = error, Data = data };
	}

	/// <summary/>
	public class Result<T> : Result
	{
		/// <summary>
		/// Data produced from the celikapi operation.
		/// Alternatively, user generated data.
		/// </summary>
		public T Data { get; set; }
	}
}
