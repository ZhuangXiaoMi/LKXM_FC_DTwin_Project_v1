using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.IRepository
{
    public interface IBaseRepository<T> where T : EntityRoot
    {
        #region 同步
        T Add(T entity);

        int BatchAdd(IEnumerable<T> entities);

        int Update(T entity);

        /// <summary>
        /// 更新部分字段，如：Update(p => p.id = 1, p => new Entity { property = value });
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="entity"></param>
        int Update(Expression<Func<T, bool>> exp, Expression<Func<T, T>> entity);

        int Delete(T entity);

        int Delete(Expression<Func<T, bool>> exp);


        bool IsExist(Expression<Func<T, bool>> exp);

        int GetCount(Expression<Func<T, bool>> exp = null);

        T FindSingle(Expression<Func<T, bool>> exp = null);

        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

        IQueryable<T> FindPage(out int total, int pageIndex = 1, int pageSize = 20
            , Expression<Func<T, bool>> exp = null, OrderByDto[] orderParams = null);

        IQueryable<TEntity> FromSql<TEntity>(string sql, params DbParameter[] parames) where TEntity : class;
        #endregion 同步

        #region 异步
        Task<T> AddAsync(T entity);

        Task<int> BatchAddAsync(IEnumerable<T> entities);

        Task<int> UpdateAsync(T entity);

        Task<int> UpdateAsync(Expression<Func<T, bool>> exp, Expression<Func<T, T>> entity);

        Task<int> DeleteAsync(T entity);

        Task<int> DeleteAsync(Expression<Func<T, bool>> exp);
        #endregion 异步
    }
}
