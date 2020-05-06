using hanimyhoney.Domain.Exceptions;

namespace hanimyhoney.Domain.Dto
{
	public class MobileResponseError : MobileResponseBase
	{
		public IExceptionModel Error { get; set; }
		public string Message { get; set; }
		public MobileResponseError() : base(false)
		{
		}

	}

}

