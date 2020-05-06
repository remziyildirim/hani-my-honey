using Newtonsoft.Json;

namespace hanimyhoney.Domain.Entity
{
	public class User : IEntity
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("userName")]
		public string UserName { get; set; }
		[JsonProperty("firstName")]
		public string FirstName { get; set; }
		[JsonProperty("surname")]
		public string Surname { get; set; }
		[JsonProperty("email")]
		public string Email { get; set; }
	}

}
