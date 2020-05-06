
namespace hanimyhoney.Domain.Exceptions
{
	public class NotFoundException : BaseException
	{
		public NotFoundException(string name, object key) : base($"Entity \"{name}\" \"{key}\" was not found.")
		{
		}
	}

}
