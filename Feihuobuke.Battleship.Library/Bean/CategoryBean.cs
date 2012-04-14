using System;

namespace Feihuobuke.Battleship.Library
{
    public class CategoryBean : IBean
    {
        
		public int Id {get;set;}
			
		public int CategoryId {get;set;}
			
		public string Name {get;set;}
			
		public bool Active {get;set;}
			
    }
}