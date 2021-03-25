using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Entity;
using LKXM.FCDTwin.IRepository;
using LKXM.FCDTwin.IService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Service
{
    public class BaseService<T> : IBaseService<T> where T : EntityRoot
    {
        protected readonly IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public bool IsExist(Expression<Func<T, bool>> exp)
            => _baseRepository.IsExist(exp);

        public int GetCount(Expression<Func<T, bool>> exp = null)
            => _baseRepository.GetCount(exp);

        public T FindSingle(Expression<Func<T, bool>> exp = null)
            => _baseRepository.FindSingle(exp);

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
            => _baseRepository.Find(exp);

        public IQueryable<T> FindPage(out int total, int pageIndex = 1, int pageSize = 20
            , Expression<Func<T, bool>> exp = null, OrderByDto[] orderParams = null)
            => _baseRepository.FindPage(out total, pageIndex, pageSize, exp, orderParams);

        public IQueryable<T> FromSql<T>(string sql, params DbParameter[] parames) where T : class
            => _baseRepository.FromSql<T>(sql, parames);

        public T Add(T entity)
            => _baseRepository.Add(entity);
        public async Task<T> AddAsync(T entity)
            => await _baseRepository.AddAsync(entity);

        public int BatchAdd(IEnumerable<T> entities)
            => _baseRepository.BatchAdd(entities);
        public async Task<int> BatchAddAsync(IEnumerable<T> entities)
            => await _baseRepository.BatchAddAsync(entities);

        public int Update(T entity)
            => _baseRepository.Update(entity);
        public async Task<int> UpdateAsync(T entity)
            => await _baseRepository.UpdateAsync(entity);

        public int Update(Expression<Func<T, bool>> exp, Expression<Func<T, T>> entity)
            => _baseRepository.Update(exp, entity);
        public async Task<int> UpdateAsync(Expression<Func<T, bool>> exp, Expression<Func<T, T>> entity)
            => await _baseRepository.UpdateAsync(exp, entity);

        public int Delete(T entity)
            => _baseRepository.Delete(entity);
        public async Task<int> DeleteAsync(T entity)
            => await _baseRepository.DeleteAsync(entity);

        public int Delete(Expression<Func<T, bool>> exp)
            => _baseRepository.Delete(exp);
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> exp)
            => await _baseRepository.DeleteAsync(exp);
    }
}
