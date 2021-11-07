using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FacturacionAPI.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(params object[] primaryKeyValue);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> predicate, bool asNotracking = false);
        TEntity Update(TEntity entity);
        TEntity Add(TEntity entity);
        List<TEntity> AddRange(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
        List<TEntity> ExecuteSql<T>(string sql, DbParameter[] parameters) where T : class;
        int ExecuteSql(string sql, DbParameter[] parameters);
        List<object> ExecuteObjSql(string sql, DbParameter[] parameters);
        IEnumerable<T> ExecuteObjSql<T>(string sql, DbParameter[] parameters) where T : class;
        bool Exists(Func<TEntity, bool> predicate);

    }
}
