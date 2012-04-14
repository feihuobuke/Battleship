using System;

namespace Feihuobuke.Battleship.Library
{
    public class IamgeDao : IDao
    {
        
		public virtual int Id {get;set;}
			
		public virtual string Title {get;set;}

        public virtual string Summary { get; set; }

		public virtual byte[] Detail {get;set;}

        public virtual bool Active { get; set; }
    }
}