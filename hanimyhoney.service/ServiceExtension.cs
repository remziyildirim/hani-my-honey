using Microsoft.Extensions.DependencyInjection;
using hanimyhoney.Domain.Dto;
using hanimyhoney.Domain.Entity;

public static class ServiceExtension
{
	public static void RegisterService(this IServiceCollection service)
	{
		service.AddTransient<IBaseService, BaseService>();
		// service.AddTransient<IBaseServiceNew<UserDto, User>, UserService>();
	}
}