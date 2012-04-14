using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate;
using NHibernate.Linq;
using log4net;

namespace Feihuobuke.Battleship.Library
{
    public abstract class FacadeBase<T>
        where T : class, IDao, new()
    {
        #region Logging Definition

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        /// <summary>
        /// Exposes the ISession used within the DAO.
        /// </summary>
        private ISession NHibernateSession
        {
            get { return NHibernateSessionManager.Instance.GetSession(); }
        }

        #region -- Private functions --

        private void Transact(Action action)
        {
            Transact(() =>
                         {
                             action.Invoke();
                             return false;
                         }
                );
        }

        protected TResult Transact<TResult>(Func<TResult> func)
        {
            if (!NHibernateSession.Transaction.IsActive)
            {
                //Wrap in transaction

                Log.Debug("Begin Transaction");

                TResult result;
                using (ITransaction tx = NHibernateSession.BeginTransaction())
                {
                    result = func.Invoke();
                    Log.Debug("Commit");
                    try
                    {
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        tx.Rollback();
                        throw;
                    }

                }

                Log.Debug("End Transaction");
                return result;
            }
            //Don't wrap;
            return func.Invoke();
        }

        #endregion

        /// <summary>
        /// Retrieves the entity with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the entity or null if it doesn't exist</returns>
        public T Get(int id)
        {
            try
            {
                Log.Debug(id);
                return NHibernateSession.Get<T>(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

            //return Transact(() => NHibernateSession.Get<T>(id));
        }

        /// <summary>
        /// Saves or updates the given NEW entity
        /// Sometimes you don't know if the entity is a duplicate, this method does an upsert for you.
        /// </summary>
        /// <param name="entity"></param>
        public T Insert(T entity)
        {
            Log.Debug(entity);
            Transact(() =>
                         {
                             NHibernateSession.SaveOrUpdate(typeof(T).FullName, entity);
                             return entity;
                         });
            return entity;
        }

        /// <summary>
        /// Here you select an object from NHibernateSession, and you made changes to the object. 
        /// Now you merge it back to the cached instancs then commit to db.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            Log.Debug(entity);
            Transact(() => NHibernateSession.Merge(typeof(T).FullName, entity));
        }

        /// <summary>
        /// Deletes the given entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            Log.Debug(entity);
            //NHibernateSession.Delete(entity);
            Transact(() => NHibernateSession.Delete(entity));
        }

        /// <summary>
        /// Returns true if at least one entity exists that matches the given exprs
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<T, bool>> expr)
        {
            Log.Debug(expr);
            return GetQuery(expr).Any();
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expr)
        {
            try
            {
                IQueryable<T> query = NHibernateSession.Query<T>();
                if (expr != null)
                {
                    query = query.Where(expr);
                }
                return query;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        /// <summary>
        /// Retrieves the entity with the given expr
        /// </summary>
        /// <param name="expr"></param>
        /// <returns>the entity or null if it doesn't exist</returns>
        public T Single(Expression<Func<T, bool>> expr)
        {
            try
            {
                Log.Debug(expr);
                return GetQuery(expr).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
    }
}