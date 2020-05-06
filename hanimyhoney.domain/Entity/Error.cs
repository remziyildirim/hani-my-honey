using Newtonsoft.Json;

namespace hanimyhoney.Domain.Entity
{
	public class Error : IEntity
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("languageCode")]
		public string LanguageCode { get; set; }
		[JsonProperty("domain")]
		public string Domain { get; set; }
		[JsonProperty("key")]
		public string Key { get; set; }
		[JsonProperty("text")]
		public string Text { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
	}

}