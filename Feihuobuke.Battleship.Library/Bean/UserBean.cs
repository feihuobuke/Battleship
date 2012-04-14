using System;

namespace Feihuobuke.Battleship.Library
{
    public class UserBean : IBean
    {
        
		public int Id {get;set;}
			
		public string Name {get;set;}
			
		public string Password {get;set;}
						
		public bool Active {get;set;}
			
    }
}