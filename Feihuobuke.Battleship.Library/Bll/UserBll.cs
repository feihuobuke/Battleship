using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Feihuobuke.Battleship.Library
{
    public partial class UserBLL : BllBase<UserBean, UserDao>
    {
		public UserBLL()
            : base(UserFacade.GetInstance())
        {
        }
        
		internal override void CopyTo(UserDao dao, ref UserBean bean)
		{
			
			bean.Id = dao.Id;
				
			bean.Name = dao.Name;
				
			bean.Password = dao.Password;
								
			bean.Active = dao.Active;
				
		}

		internal override void CopyTo(UserBean bean, ref UserDao dao)
		{
			
			dao.Id = bean.Id;

            dao.Name = bean.Name;
				
			dao.Password = bean.Password;
								
			dao.Active = bean.Active;
				
		}
    }
}