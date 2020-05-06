using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using hanimyhoney.Domain.Entity;
using Neo4jClient;
using Neo4jClient.Cypher;

public class BaseRepository : IBaseRepository
{
	private readonly IGraphClient _graphClient;
	public BaseRepository(IGraphClient graphClient)
	{
		_graphClient = graphClient;
	}

	public void CreateUniqueConstraintIfNeeded<T>(string propertyName) where T : IEntity
	{
		// var identity = String.Format("(n:{0})", typeof(T).Name);
		// var camelCasePropertyName = $"{propertyName.Substring(0, 1).ToLowerInvariant()}{(propertyName.Length > 1 ? propertyName.Substring(1, propertyName.Length - 1) : string.Empty)}";
		// var property = String.Format("n.{0}", camelCasePropertyName);

		var identity = String.Format("(n:{0})", typeof(T).Name);
		var property = String.Format("n.{0}", propertyName);
		_graphClient.Cypher.CreateUniqueConstraint(identity, property);
	}

	public T Create<T>(T entity) where T : IEntity
	{
		if (String.IsNullOrEmpty(entity.Id))
		{
			entity.Id = Guid.NewGuid().ToString();
		}

		var query = _graphClient.Cypher
		.Create(String.Format("(n:{0} {{entity}})", typeof(T).Name))
		.WithParam("entity", entity)
		.Return(n => n.As<T>());

		QueryDebugText(MethodBase.GetCurrentMethod().Name, query);

		return query.ResultsAsync.Result.FirstOrDefault();
	}

	public T Update<T>(T entity) where T : IEntity
	{
		if (String.IsNullOrEmpty(entity.Id))
		{
			throw new ArgumentException("Id is required for update");
		}

		var query = _graphClient.Cypher
		.Match(String.Format("(n:{0})", typeof(T).Name))
		.Where("n.id = {id}")
		.WithParam("id", entity.Id)
		.Set("n += {entity}")
		.WithParam("entity", entity)
		.Return(n => n.As<T>());

		// Not working
		// var query = _graphClient.Cypher
		// 	.Match(String.Format("(n:{0})", typeof(T).Name))
		// 	.Where<T>(n => n.Id == entity.Id)
		// 	.Set("n += {entity}")
		// 	.WithParam("entity", entity)
		// 	.Return(n => n.As<T>());

		QueryDebugText(MethodBase.GetCurrentMethod().Name, query);

		return query.ResultsAsync.Result.FirstOrDefault();
	}

	public T FindById<T>(string id) where T : IEntity
	{
		var query = _graphClient.Cypher
				.Match(String.Format("(n:{0})", typeof(T).Name))
				.Where((T n) => n.Id == id)
				.Return(n => n.As<T>());

		QueryDebugText(MethodBase.GetCurrentMethod().Name, query);

		return query.ResultsAsync.Result.FirstOrDefault();
	}

	public void Delete<T>(T entity) where T : IEntity
	{
		var query = _graphClient.Cypher
				.Match(String.Format("(n:{0})", typeof(T).Name))
				.Where((T n) => n.Id == entity.Id)
				.DetachDelete("n");

		QueryDebugText(MethodBase.GetCurrentMethod().Name, query);

		query.ExecuteWithoutResultsAsync();
	}

	public void DeleteById<T>(string id) where T : IEntity
	{
		Delete(FindById<T>(id));
	}

	public ICollection<T> Find<T>(Expression<Func<T, bool>> filter) where T : IEntity
	{
		ICypherFluentQuery queryWithoutReturn = _graphClient.Cypher
				.Match(String.Format("(n:{0})", typeof(T).Name));

		if (filter != null)
		{
			queryWithoutReturn = queryWithoutReturn.Where(filter);
		}

		var query = queryWithoutReturn.Return(n => n.As<T>());

		QueryDebugText(MethodBase.GetCurrentMethod().Name, query);

		return query.ResultsAsync.Result.ToList();
	}

	public bool Exists<T>(Expression<Func<T, bool>> predicate) where T : IEntity
	{
		var query = _graphClient.Cypher
				.Match(String.Format("(n:{0})", typeof(T).Name))
				.Where(predicate)
				.Return(n => n.As<T>());

		QueryDebugText(MethodBase.GetCurrentMethod().Name, query);

		return query.ResultsAsync.Result.Any();
	}

	private void QueryDebugText(string functionName, ICypherFluentQuery query)
	{
		// var removable = _graphClient.Cypher
    // .Match("(t:Node {Mapped: true})")
    // .With("t.name AS t, collect(t) AS nodes")
    // .Return(() => Return.As<User>("nodes[0]"));

		Console.WriteLine("{0} Query: {1}", functionName, query.Query.QueryText);
		Console.WriteLine("{0} Debug: {1}", functionName, query.Query.DebugQueryText);
		Console.WriteLine("adasdsd", functionName, functionName, functionName, functionName, functionName);
	}
}