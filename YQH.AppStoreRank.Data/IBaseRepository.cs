using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace YQH.AppStoreRank.Data
{
    public interface IBaseRepository
    {
        T Add<T>(T entity) where T : class, new();

        void Delete<T>(params object[] keyValues) where T : class, new();

        void Update<T>(T entity) where T : class, new();

        bool Exist<T>(Expression<Func<T, bool>> anyLambda) where T : class, new();

        T Find<T>(params object[] keyValues) where T : class, new();

        int Count<T>(Expression<Func<T, bool>> countLambda) where T : class, new();

        T First<T>(Expression<Func<T, bool>> firstLambda) where T : class, new();

        IQueryable<T> LoadEntities<T>(Expression<Func<T, bool>>whereLambda = null) where T : class, new();

        IQueryable<T> LoadPageEntities<T, TKey>(int pageIndex, int pageSize,
                                          out int totalCount, out int pageCount,
                                          Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, TKey>> orderBy) where T : class, new();

        IQueryable<T> LoadPageEntities<T, TKey>(IQueryable<T> query, int pageIndex, int pageSize,
                                        out int totalCount, out int pageCount, bool isAsc, Expression<Func<T, TKey>> orderBy) where T : class, new();
    
        void SaveChanges();

    }
}
