using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Feihuobuke.Battleship.Library
{
    public partial class IamgeBLL : BllBase<IamgeBean, IamgeDao>
    {
		public IamgeBLL()
            : base(IamgeFacade.GetInstance())
        {
        }
        
		internal override void CopyTo(IamgeDao dao, ref IamgeBean bean)
		{
			
			bean.Id = dao.Id;
				
			bean.Title = dao.Title;
				
			bean.Detail = dao.Detail;
				
		}

		internal override void CopyTo(IamgeBean bean, ref IamgeDao dao)
		{
			
			dao.Id = bean.Id;
				
			dao.Title = bean.Title;
				
			dao.Detail = bean.Detail;
				
		}
    }
}