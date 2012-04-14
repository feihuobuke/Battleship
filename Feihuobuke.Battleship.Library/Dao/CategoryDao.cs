using System;

namespace Feihuobuke.Battleship.Library
{
    public class CategoryDao : IDao
    {
        
		public virtual int Id {get;set;}
			
		public virtual int CategoryId {get;set;}
			
		public virtual string Name {get;set;}
			
		public virtual bool Active {get;set;}
			
    }
}