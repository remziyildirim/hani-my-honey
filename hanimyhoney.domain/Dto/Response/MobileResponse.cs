namespace hanimyhoney.Domain.Dto
{
	public class MobileResponse<TData> : MobileResponseBase
	{
		public TData Data { get; set; }
		protected MobileResponse() : base(true)
		{
		}

		protected MobileResponse(TData data) : this()
		{
			Data = data;
		}
	}

}

