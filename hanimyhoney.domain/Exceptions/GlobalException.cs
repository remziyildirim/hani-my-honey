
using hanimyhoney.Domain.Dto;

namespace hanimyhoney.Domain.Exceptions
{
	public class GlobalException : BaseException
	{
		public ExceptionModel Error { get; set; }
		public GlobalException(string message, string title = "Uyarı", ExceptionShowType showType = ExceptionShowType.FullScreen) : base(message)
		{
			Error = new ExceptionModel
			{
				ShowType = showType,
				Title = title,
				Message = message
			};
		}

		public GlobalException(string message, ExceptionModel error) : base(message)
		{
			Error = error;
		}
	}

}