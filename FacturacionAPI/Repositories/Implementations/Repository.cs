using FacturacionAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FacturacionAPI.Repositories.Implementations
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        #region Properties
        protected DbContext Context { get; set; }
        protected Dictionary<String, String> FriendlyPropertiesNames = new Dictionary<string, string>();
        protected List<String> NotVisibleProperties = new List<String>();

        #endregion

        #region PublicMethods
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            var result = Context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);

            return result;
        }

        public TEntity Find(params object[] primaryKeyValue)
        {
            var result = Context.Set<TEntity>().Find(primaryKeyValue);

            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var result = Context.Set<TEntity>().AsNoTracking();

            return result;
        }

        public IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> predicate, bool asNotracking = false)
        {
            if (asNotracking)
                return Context.Set<TEntity>().Where(predicate).AsNoTracking();
            else
                return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();

            return entity;
        }
        public List<TEntity> UpdateRange(List<TEntity> entities)
        {
            Context.UpdateRange(entities);
            Context.SaveChanges();

            return entities;
        }


        public TEntity Add(TEntity entity)
        {
            Context.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public List<TEntity> AddRange(List<TEntity> entities)
        {
            Context.AddRange(entities);
            Context.SaveChanges();

            return entities;
        }

        public void Remove(TEntity entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public void RemoveRange(List<TEntity> entities)
        {
            Context.RemoveRange(entities);
            Context.SaveChanges();
        }

        public List<TEntity> ExecuteSql<T>(string sql, DbParameter[] parameters) where T : class
        {
            return Context.Set<TEntity>().FromSqlRaw(sql, parameters).ToList();
        }

        public int ExecuteSql(string sql, DbParameter[] parameters)
        {
            return Context.Database.ExecuteSqlRaw(sql, parameters);
        }

        public List<object> ExecuteObjSql(string sql, DbParameter[] parameters)
        {
            return Context.Set<object>().FromSqlRaw(sql, parameters).ToList();
        }

        public IEnumerable<T> ExecuteObjSql<T>(string sql, DbParameter[] parameters) where T : class
        {
            return Context.Set<T>().FromSqlRaw(sql, parameters).ToList();
        }

        /// <summary>
        /// Verificar si existe una entidad partiendo de una condicion. AsNoTracking para que entity framework no le de seguimiento a la entidad cuando se envie a consultar y se pueda agregar mas adelante.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Exists(Func<TEntity, bool> predicate)
        {
            return Context.Set<TEntity>().AsNoTracking().Any(predicate);
        }

        #endregion

        #region PrivateRegion

        private String GetDataType(Type dataType)
        {
            var type = dataType;
            string result = "";
            if (type.IsGenericType && Nullable.GetUnderlyingType(type) != null)
            {
                type = Nullable.GetUnderlyingType(dataType);
            }

            List<Type> numericTypes = new List<Type>() {
                typeof (Int16), typeof (Int32), typeof (Int64), typeof (long), typeof (short), typeof (uint),
                typeof (double), typeof (decimal), typeof (float), typeof (Int32)
            };
            List<Type> dateTypes = new List<Type>() { typeof(DateTime), typeof(TimeSpan) };

            if (numericTypes.Contains(type))
            {
                result = "number";
            }
            else if (dateTypes.Contains(type))
            {
                result = "date";
            }
            else
            {
                result = "string";
            }

            return result;
        }

        public Expression<Func<TSource, object>> GetExpression<TSource>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TSource), "p");
            Expression conversion = Expression.Convert(Expression.Property
            (param, propertyName), typeof(object));   //important to use the Expression.Convert
            return Expression.Lambda<Func<TSource, object>>(conversion, param);
        }

        //makes deleget for specific prop
        public Func<TSource, object> GetFunc<TSource>(string propertyName)
        {
            return GetExpression<TSource>(propertyName).Compile();  //only need compiled expression
        }

        //OrderBy overload
        public IOrderedEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, string propertyName)
        {
            return source.OrderBy(GetFunc<TSource>(propertyName));
        }

        //OrderBy overload
        public IOrderedQueryable<TSource> OrderBy<TSource>(IQueryable<TSource> source, string propertyName)
        {
            return source.OrderBy(GetExpression<TSource>(propertyName));
        }

        public IOrderedEnumerable<TSource> OrderByDescending<TSource>(IEnumerable<TSource> source, string propertyName)
        {
            return source.OrderByDescending(GetFunc<TSource>(propertyName));
        }

        //OrderBy overload
        public IOrderedQueryable<TSource> OrderByDescending<TSource>(IQueryable<TSource> source, string propertyName)
        {
            return source.OrderByDescending(GetExpression<TSource>(propertyName));
        }



        #endregion
    }
}
