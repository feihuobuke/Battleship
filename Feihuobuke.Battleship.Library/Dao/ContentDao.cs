using System;

namespace Feihuobuke.Battleship.Library
{
    public class ContentDao : IDao
    {
        
		public virtual int Id {get;set;}
			
		public virtual int CategoryId {get;set;}
			
		public virtual string Title {get;set;}
			
		public virtual string Summary {get;set;}
			
		public virtual string Detail {get;set;}
			
		public virtual DateTime PublishDate {get;set;}
						
		public virtual bool Active {get;set;}
			
    }
}