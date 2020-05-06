namespace hanimyhoney.Domain.Dto
{
	public abstract class MobileResponseBase
	{
		public bool Success { get; set; }
		public MobileResponseBase(bool success)
		{
			Success = success;
		}
	}
}