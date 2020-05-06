namespace hanimyhoney.Domain.Exceptions
{
	public interface IExceptionModel
	{
		ExceptionShowType ShowType { get; set; }
		string Title { get; set; }
		string Message { get; set; }
	}

	public class ExceptionModel : IExceptionModel
	{
		public ExceptionShowType ShowType { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
	}

	public enum ExceptionShowType
	{
		FullScreen = 0,
		Popup = 1
	}
}