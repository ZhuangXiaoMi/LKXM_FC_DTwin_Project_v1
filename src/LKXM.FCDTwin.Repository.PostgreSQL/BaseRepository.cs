using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Entity;
using LKXM.FCDTwin.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace LKXM.FCDTwin.Repository.PostgreSQL
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityRoot
    {
        private readonly ApiDbContext _dbContext;
        public readonly IQueryable<T> _dbSet;

        public BaseRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #region 同步
        private int Save()
        {
            return _dbContext.SaveChanges();//数据回调，增加/修改可取数据使用
        }

        private IQueryable<T> Filter(Expression<Func<T, bool>> exp = null)
        {
            var dbSet = _dbContext.Set<T>().AsNoTracking().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }

        public T Add(T entity)
        {
            var result = _dbContext.Set<T>().Add(entity);
            Save();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return result.Entity;
        }

        public int BatchAdd(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            return Save();
        }

        public int Update(T entity)
        {
            var model = _dbContext.Entry(entity);
            model.State = EntityState.Modified;
            //_dbContext.Set<T>().Attach(entity);

            //如果数据没有发生变化
            if (!_dbContext.ChangeTracker.HasChanges())
            {
                return 0;
            }
            var count = Save();
            model.State = EntityState.Detached;
            return count;
        }

        public int Update(Expression<Func<T, bool>> exp, Expression<Func<T, T>> entity)
        {
            _dbContext.Set<T>().Where(exp).Update(entity);
            return Save();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);//entity删除前数据库不存在会报错
            return Save();
        }

        public int Delete(Expression<Func<T, bool>> exp)
        {
            IEnumerable<T> entities = Filter(exp);
            if (entities.Count() > 0)
            {
                _dbContext.Set<T>().RemoveRange(entities);
                return Save();
            }
            else
            {
                return 0;
            }
        }


        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            return _dbContext.Set<T>().Any(exp);
        }

        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp).Count();
        }

        public T FindSingle(Expression<Func<T, bool>> exp = null)
        {
            return _dbContext.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }

        public IQueryable<T> FindPage(out int total, int pageIndex = 1, int pageSize = 20
            , Expression<Func<T, bool>> exp = null, OrderByDto[] orderParams = null)
        {
            var list = Filter(exp);
            if (orderParams != null && orderParams.Length > 0)
            {
                var queryExp = list.Expression;
                var parameter = Expression.Parameter(typeof(T), "p");
                //var parameter = Expression.Parameter(list.ElementType);
                var methodAsc = "OrderBy";
                var methodDesc = "OrderByDescending";
                foreach (var param in orderParams)
                {
                    //根据属性名获取属性
                    var property = typeof(T).GetProperty(param.property_name);
                    //var property = Expression.Property(parameter, param.property_name);
                    if (property == null) continue;

                    //创建一个访问属性的表达式
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    var methodName = param.method == OrderByEnum.ASC ? methodAsc : methodDesc;
                    queryExp = Expression.Call(typeof(Queryable), methodName
                        , new Type[] { typeof(T), property.PropertyType }
                        , queryExp, Expression.Quote(orderByExp));

                    methodAsc = "ThenBy";
                    methodDesc = "ThenByDescending";
                }
                list = list.Provider.CreateQuery<T>(queryExp);
            }

            total = list.Count();
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params DbParameter[] parames) where TEntity : class
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(sql, parames);
        }
        #endregion 同步

        #region 异步
        private async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();//数据回调，增加/修改可取数据使用
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);
            await SaveAsync();
            return result.Entity;
        }

        public async Task<int> BatchAddAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return await SaveAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            await _dbContext.Set<T>().SingleUpdateAsync(entity);
            return await SaveAsync();
        }

        public async Task<int> UpdateAsync(Expression<Func<T, bool>> exp, Expression<Func<T, T>> entity)
        {
            await _dbContext.Set<T>().Where(exp).UpdateAsync(entity);
            return await SaveAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await SaveAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> exp)
        {
            IEnumerable<T> entities = Filter(exp);
            if (entities.Count() > 0)
            {
                _dbContext.Set<T>().RemoveRange(entities);
                return await SaveAsync();
            }
            else
            {
                return 0;
            }
        }
        #endregion 异步
    }
}
