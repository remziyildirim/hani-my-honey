using Newtonsoft.Json;

namespace hanimyhoney.Domain.Entity
{
	public interface IEntity
	{
		[JsonProperty("id")]
		string Id { get; set; }

		// string GetLabel() {
		// 	return this.GetType().Name;
		// }
	}

}