﻿using FastDotnet.Schema.Base;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FastDotnet.Repository
{
    public interface IRepository<TEntity>
    {
        IMongoQueryable<TEntity> QueryData();

        IMongoCollection<TEntity> GetCollection();

        Task<TModel> Get<TModel>(Expression<Func<TEntity, bool>> filter);

        Task<IList<TModel>> List<TModel>(Expression<Func<TEntity, bool>> filter);

        Task<IList<TModel>> List<TModel>();

        Task<IList<TModel>> ListByIds<TModel>(IList<string> ids);

        PageResultModel<TModel> PageData<TModel>(Expression<Func<TEntity, bool>> filter, PageQueryModel pageQuery);

        Task<long> Count(Expression<Func<TEntity, bool>> filter);

        Task<TModel> Save<TModel>(TModel model);

        Task<TModel> Update<TModel>(TModel model);

        Task<long> Delete(Expression<Func<TEntity, bool>> filter);
    }
}