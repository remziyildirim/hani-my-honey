using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using hanimyhoney.Domain.Dto;
using hanimyhoney.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hanimyhoney.api.Controllers
{
	[ApiController]
	// [Route("[controller]")]
	public class GraphController : ControllerBase
	{
		private readonly ILogger<GraphController> _logger;
		private readonly IBaseService _service;
		// private readonly IBaseServiceNew<UserDto, User> _serviceNew;

		public GraphController(ILogger<GraphController> logger, IBaseService service) // IBaseServiceNew<UserDto, User> serviceNew
		{
			_logger = logger;
			_service = service;
			// _serviceNew = serviceNew;
		}

		[Route("graph/about")]
		[HttpGet]
		public string About()
		{
			return "Aloo, hani my honey. ";
		}

		[Route("graph/createUser")]
		[HttpPost]
		public User Create(User user)
		{
			return _service.Create(user);
		}

		[Route("graph/getUser")]
		[HttpGet]
		public User GetUser(string id)
		{
			return _service.findById<User>(id);
		}

		[Route("graph/updateUser")]
		[HttpPost]
		public User Update(User user)
		{
			return _service.Update(user);
		}

		[Route("graph/deleteUser")]
		[HttpPost]
		public void Delete(User user)
		{
			_service.Delete(user);
		}

		[Route("graph/deleteUserById")]
		[HttpPost]
		public void DeleteById(string id)
		{
			_service.DeleteById<User>(id);
		}

		[Route("graph/getUsers")]
		[HttpGet]
		public ICollection<User> GetUsers(string firstName)
		{
			Expression<Func<User, bool>> filter = null;
			if (firstName != null)
			{
				filter = n => n.FirstName == firstName;
			}
			return _service.find(filter);
		}

		// [Route("graph/getUserNew")]
		// [HttpGet]
		// public UserDto GetUserNew(string id)
		// {
		// 	return _serviceNew.findById(id);
		// }
	}
}
