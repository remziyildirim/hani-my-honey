namespace hanimyhoney.Domain.Dto
{
	public class UserDto : IDto
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string Surname { get; set; }
	}
}