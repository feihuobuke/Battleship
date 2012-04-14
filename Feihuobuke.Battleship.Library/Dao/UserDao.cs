using System;

namespace Feihuobuke.Battleship.Library
{
    public class UserDao : IDao
    {
        
		public virtual int Id {get;set;}
			
		public virtual string Name {get;set;}
			
		public virtual string Password {get;set;}
						
		public virtual bool Active {get;set;}
			
    }
}