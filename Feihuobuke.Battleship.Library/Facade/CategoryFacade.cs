namespace Feihuobuke.Battleship.Library
{
    public class CategoryFacade : FacadeBase<CategoryDao>
    {
        private static readonly CategoryFacade Instance = new CategoryFacade();

        private CategoryFacade()
        {
        }

        public static CategoryFacade GetInstance()
        {
            return Instance;
        }
    }
}