using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Feihuobuke.Battleship.Library
{
    public partial class ContentBLL : BllBase<ContentBean, ContentDao>
    {
		public ContentBLL()
            : base(ContentFacade.GetInstance())
        {
        }
        
		internal override void CopyTo(ContentDao dao, ref ContentBean bean)
		{
			
			bean.Id = dao.Id;
				
			bean.CategoryId = dao.CategoryId;
				
			bean.Title = dao.Title;
				
			bean.Summary = dao.Summary;
				
			bean.Detail = dao.Detail;
				
			bean.PublishDate = dao.PublishDate;
								
			bean.Active = dao.Active;
				
		}

		internal override void CopyTo(ContentBean bean, ref ContentDao dao)
		{
			
			dao.Id = bean.Id;
				
			dao.CategoryId = bean.CategoryId;
				
			dao.Title = bean.Title;
				
			dao.Summary = bean.Summary;
				
			dao.Detail = bean.Detail;
				
			dao.PublishDate = bean.PublishDate;
								
			dao.Active = bean.Active;
				
		}
    }
}