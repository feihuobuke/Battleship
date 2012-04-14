using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI.WebControls;
using Feihuobuke.Battleship.Library.Util;
using log4net;

namespace Feihuobuke.Battleship.Library
{
    [DataObject]
    public abstract class BllBase<TBean, TDao>
        where TBean : class, IBean, new()
        where TDao : class, IDao, new()
    {
        #region Logging Definition

        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        protected FacadeBase<TDao> Facade;

        protected BllBase(FacadeBase<TDao> facade)
        {
            Facade = facade;
        }

        #region IBLL<TBean> Members

        /// <summary>
        /// Insert bean to DB with aspUserId
        /// used for insert in page' request, other bll callings
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public virtual void Insert(TBean bean)
        {
            var json = bean.ToJson();
            log.DebugFormat("[Insert Start]bean:{0}", json);
            try
            {
                var dao = new TDao();
                CopyTo(bean, ref dao);
                Facade.Insert(dao);
                log.DebugFormat("[Insert Success]bean:{0}", json);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("[Insert Error]bean:{0};ex:{1}", json, ex.Message);
            }
            log.DebugFormat("[Insert End]bean:{0}", json);
        }

        /// <summary>
        /// Update bean in db
        /// used for update in page' request, other bll callings
        /// </summary>
        /// <param name="bean"></param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public virtual void Update(TBean bean)
        {
            var json = bean.ToJson();
            log.DebugFormat("[Update Start]bean:{0}", json);

            try
            {
                var dao = Facade.Get(bean.Id);
                CopyTo(bean, ref dao);
                Facade.Update(dao);
                log.DebugFormat("[Update Success]bean:{0}", json);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("[Update Error]bean:{0};ex:{1}", json, ex.Message);
            }
            log.DebugFormat("[Update End]bean:{0}", json);
        }

        /// <summary>
        /// delete bean in db
        ///  used for update bean in page' request or other bll callings
        /// </summary>
        /// <param name="aspUserId"></param>
        /// <param name="bean"></param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public virtual void Delete(TBean bean)
        {
            var json = bean.ToJson();
            log.DebugFormat("[Delete Start]bean:{0}", json);

            try
            {
                var dao = Facade.Get(bean.Id);
                CopyTo(bean, ref dao);
                Facade.Delete(dao);
                log.DebugFormat("[Delete Success]bean:{0}", json);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("[Delete Error]bean:{0};ex:{1}", json, ex.Message);
            }
            log.DebugFormat("[Delete End]bean:{0}", json);
        }

        /// <summary>
        /// delete bean by id or id list
        /// used in UI pages, blls,where get an id list to delete beans
        /// </summary>
        /// <param name="aspUserId"></param>
        /// <param name="idList"></param>
        public virtual void DeleteById(params int[] idList)
        {
            var json = idList.ToJson();
            log.DebugFormat("[DeleteById Start]idList:{0}", json);

            foreach (var id in idList)
            {
                log.DebugFormat("[DeleteById]id:{0}", id);
                Delete(new TBean { Id = id });
                log.DebugFormat("[DeleteById Success]id:{0}", id);
            }
            log.DebugFormat("[DeleteById End]idList:{0}", json);
        }

        /// <summary>
        /// get bean from db
        /// used in/for get bean details in UI,BLL or other place need to get details of the bean
        /// </summary>
        /// <param name="aspUserId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public virtual TBean SelectById(int id)
        {
            log.DebugFormat("[SelectById Start]id:{0}", id);
            TBean bean = null;
            try
            {
                var dao = Facade.Get(id);
                if (dao == null)
                {
                    bean = null;
                    log.DebugFormat("[SelectById Success]id:{0};dao:null", id);
                }
                else
                {
                    bean = new TBean();
                    CopyTo(dao, ref bean);
                    var json = dao.ToJson();
                    log.DebugFormat("[SelectById Success]id:{0}:dao:{1}", id, json);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("[SelectById Error]id:{0};ex:{1}", id, ex.Message);
            }
            log.DebugFormat("[SelectById End]id:{0}", id);
            return bean;
        }

        [DataObjectMethod(DataObjectMethodType.Fill, true)]
        public virtual List<TBean> SelectAll()
        {
            return SelectAll(new Pagination()
                                 {
                                     StartRowIndex = 0,
                                     MaximumRows = 50
                                 });
        }

        /// <summary>
        /// Search by SearchBean
        /// </summary>
        /// <param name="aspUserId"></param>
        /// <param name="searchBean"></param>
        /// <param name="sort"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public virtual List<TBean> SelectAll(Pagination pagination)
        {
            var json = pagination.ToJson();
            log.DebugFormat("[SelectAll Start]pagination:{0}", json);
            var query = (IQueryable<TDao>)GetQuery().OrderByDescending(t => t.Id);
            if (pagination != null)
            {
                query = query.Skip(pagination.StartRowIndex).Take(pagination.MaximumRows);
            }
            List<TDao> data = query.ToList();
            var tmp = new List<TBean>();
            foreach (TDao dao in data)
            {
                var bean = new TBean();
                CopyTo(dao, ref bean);
                tmp.Add(bean);
                var jsonDao = dao.ToJson();
                log.DebugFormat("[SelectAll Convert]dao:{0}", jsonDao);
            }
            log.DebugFormat("[SelectAll End]pagination:{0}", json);
            return tmp;
        }

        /// <summary>
        /// get count of objects
        /// </summary>
        /// <returns></returns>
        public virtual int GetCount()
        {
            return GetQuery().Count();
        }

        #endregion

        protected IQueryable<TDao> GetQuery()
        {
            return Facade.GetQuery(null);
        }

        internal abstract void CopyTo(TDao dao, ref TBean bean);

        internal abstract void CopyTo(TBean bean, ref TDao dao);
    }
}