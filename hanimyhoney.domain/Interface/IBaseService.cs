using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using hanimyhoney.Domain.Dto;
using hanimyhoney.Domain.Entity;

public interface IBaseService
{
	T Create<T>(T entity) where T : IEntity;
	T findById<T>(string id) where T: IEntity;
	T Update<T>(T entity) where T : IEntity;
	void Delete<T>(T entity) where T : IEntity;
	void DeleteById<T>(string id) where T : IEntity;
	ICollection<T> find<T>(Expression<Func<T, bool>> filter) where T: IEntity;
	bool Exists<T>(Expression<Func<T, bool>> predicate) where T: IEntity;
}


public interface IBaseServiceNew<TDto, TEntity> where TDto : IDto where TEntity : IEntity
{
	TDto Create(TEntity entity);
	TDto findById(string id);
	TDto Update(TEntity entity);
	void Delete(TEntity entity);
	void DeleteById(string id);
	ICollection<TDto> find(Expression<Func<TEntity, bool>> filter);
	bool Exists(Expression<Func<TEntity, bool>> predicate);
}