using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using hanimyhoney.Domain.Entity;

public interface IBaseRepository
{
	// void CreateUniqueConstraintIfNeeded<T>(string propertyName) where T : IEntity;
	T Create<T>(T entity) where T : IEntity;
	T FindById<T>(string id) where T : IEntity;
	T Update<T>(T entity) where T : IEntity;
	void Delete<T>(T entity) where T : IEntity;
	void DeleteById<T>(string id) where T : IEntity;
	ICollection<T> Find<T>(Expression<Func<T, bool>> filter) where T : IEntity;
	bool Exists<T>(Expression<Func<T, bool>> predicate) where T : IEntity;
}