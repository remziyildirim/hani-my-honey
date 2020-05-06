using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Neo4jClient;

public static class RepositoryExtension
{
	public static void RegisterRepository(this IServiceCollection service, string url, string user, string password)
	{
		var client = new GraphClient(new Uri(url), user, password);
		client.ConnectAsync();

		service.AddSingleton<IGraphClient>(client);
		service.AddTransient<IBaseRepository, BaseRepository>();
	}

	public static Dictionary<string, string> constraint = new Dictionary<string, string>{
			// {nameof(User), "id"}
		};
}