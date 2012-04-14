using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Feihuobuke.Battleship.Library
{
    public partial class CategoryBLL : BllBase<CategoryBean, CategoryDao>
    {
		public CategoryBLL()
            : base(CategoryFacade.GetInstance())
        {
        }
        
		internal override void CopyTo(CategoryDao dao, ref CategoryBean bean)
		{
			
			bean.Id = dao.Id;
				
			bean.CategoryId = dao.CategoryId;
				
			bean.Name = dao.Name;
				
			bean.Active = dao.Active;
				
		}

		internal override void CopyTo(CategoryBean bean, ref CategoryDao dao)
		{
			
			dao.Id = bean.Id;
				
			dao.CategoryId = bean.CategoryId;
				
			dao.Name = bean.Name;
				
			dao.Active = bean.Active;
				
		}
    }
}