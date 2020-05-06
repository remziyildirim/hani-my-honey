using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using hanimyhoney.Domain.Dto;
using hanimyhoney.Domain.Entity;

public class BaseService: IBaseService
{
	private readonly IBaseRepository _repository;

	public BaseService(IBaseRepository repository)
	{
		_repository = repository;
	}
	
	public T Create<T>(T entity) where T : IEntity
	{
		return _repository.Create(entity);
	}

	public T findById<T>(string id) where T: IEntity
	{
		return _repository.FindById<T>(id);
	}

	public T Update<T>(T entity) where T : IEntity
	{
		return _repository.Update(entity);
	}

	public void Delete<T>(T entity) where T : IEntity
	{
		_repository.Delete(entity);
	}
	public void DeleteById<T>(string id) where T : IEntity
	{
		_repository.DeleteById<T>(id);
	}

	public ICollection<T> find<T>(Expression<Func<T, bool>> filter) where T: IEntity
	{
		return _repository.Find(filter);
	}

	public bool Exists<T>(Expression<Func<T, bool>> predicate) where T: IEntity
	{
		return _repository.Exists(predicate);
	}
}


public class BaseServiceNew<TDto, TEntity> : IBaseServiceNew<TDto, TEntity> where TDto : IDto where TEntity : IEntity
{
	private readonly IBaseRepository _repository;
	private readonly IMapper _mapper;

	public BaseServiceNew(IBaseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public TDto Create(TEntity entity)
	{
		return _mapper.Map<TDto>(_repository.Create(entity));
	}

	public void Delete(TEntity entity)
	{
		_repository.Delete(entity);
	}

	public void DeleteById(string id)
	{
		_repository.DeleteById<TEntity>(id);
	}

	public bool Exists(Expression<Func<TEntity, bool>> predicate)
	{
		return _repository.Exists(predicate);
	}

	public ICollection<TDto> find(Expression<Func<TEntity, bool>> filter)
	{
		return _mapper.Map<ICollection<TDto>>(_repository.Find(filter));
	}

	public TDto findById(string id)
	{
		return _mapper.Map<TDto>(_repository.FindById<TEntity>(id));
	}

	public TDto Update(TEntity entity)
	{
		return _mapper.Map<TDto>(_repository.Update(entity));
	}
}

public class UserService : BaseServiceNew<UserDto, User>
{
	public UserService(IBaseRepository repository, IMapper mapper) : base(repository, mapper)
	{
	}
}