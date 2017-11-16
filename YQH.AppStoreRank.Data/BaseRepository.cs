using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace YQH.AppStoreRank.Data
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DbContext _dbContext;

        public BaseRepository()
        {
            _dbContext = DbContextFactory.GetCurrentDbContext();
        }

        private string GetErrorMsg()
        {
            var error = _dbContext.GetValidationErrors().First();
            string msg = error.ValidationErrors.First().ErrorMessage;
            string tableName = error.Entry.Entity.GetType().Name;
            return string.Format(Environment.NewLine + "操作表[{0}]出现错误，{1}", tableName, msg);
        }

        #region 增删改的公共方法

        public T Add<T>(T entity) where T : class, new()
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public void Update<T>(T entity) where T : class, new()
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(params object[] keyValues) where T : class, new()
        {
            var entity = _dbContext.Set<T>().Find(keyValues);
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        #endregion


        #region 查询方法

        public bool Exist<T>(Expression<Func<T, bool>> anyLambda) where T : class, new()
        {
            return _dbContext.Set<T>().Any(anyLambda);
        }

        public T Find<T>(params object[] keyValues) where T : class, new()
        {
            return _dbContext.Set<T>().Find(keyValues);
        }


        public int Count<T>(Expression<Func<T, bool>> countLambda) where T : class, new()
        {
            return _dbContext.Set<T>().Count(countLambda);
        }

        public T First<T>(Expression<Func<T, bool>> firstLambda) where T : class, new()
        {
            return _dbContext.Set<T>().FirstOrDefault(firstLambda);
        }


        public IQueryable<T> LoadEntities<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            if (whereLambda == null)
            {
                return _dbContext.Set<T>().AsQueryable();
            }
            return _dbContext.Set<T>().Where(whereLambda).AsQueryable();
        }
        //分页
        public IQueryable<T> LoadPageEntities<T, TKey>(int pageIndex, int pageSize, out int totalCount, out int pageCount,
            Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, TKey>> orderBy) where T : class, new()
        {
            IQueryable<T> temp = _dbContext.Set<T>().Where(whereLambda).AsQueryable();
            totalCount = temp.Count();
            pageSize = Math.Min(pageSize, 30);
            pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

            if (isAsc)
            {
                temp = temp.OrderBy(orderBy)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize).AsQueryable();
            }
            else
            {
                temp = temp.OrderByDescending(orderBy)
                          .Skip(pageSize * (pageIndex - 1))
                          .Take(pageSize).AsQueryable();
            }
            return temp.AsNoTracking();
        }


        public IQueryable<T> LoadPageEntities<T, TKey>(IQueryable<T> query, int pageIndex, int pageSize, out int totalCount, out int pageCount, bool isAsc, Expression<Func<T, TKey>> orderBy) where T : class, new()
        {
            IQueryable<T> temp = query;
            totalCount = temp.Count();
            pageSize = Math.Min(pageSize, 30);
            pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
            if (isAsc)
            {
                temp = temp.OrderBy(orderBy)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize).AsQueryable();
            }
            else
            {
                temp = temp.OrderByDescending(orderBy)
                          .Skip(pageSize * (pageIndex - 1))
                          .Take(pageSize).AsQueryable();
            }
            return temp;
        }
        #endregion

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex + GetErrorMsg());
                throw;
            }
            catch (DbUpdateException ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex.InnerException == null ? ex : ex.InnerException.InnerException);
                throw;
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                throw;
            }
        }

      

       
    }
}
