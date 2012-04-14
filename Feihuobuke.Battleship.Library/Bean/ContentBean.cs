using System;

namespace Feihuobuke.Battleship.Library
{
    public class ContentBean : IBean
    {
        
		public int Id {get;set;}
			
		public int CategoryId {get;set;}
			
		public string Title {get;set;}
			
		public string Summary {get;set;}
			
		public string Detail {get;set;}
			
		public DateTime PublishDate {get;set;}
						
		public bool Active {get;set;}
			
    }
}