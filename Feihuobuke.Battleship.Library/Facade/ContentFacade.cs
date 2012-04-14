namespace Feihuobuke.Battleship.Library
{
    public class ContentFacade : FacadeBase<ContentDao>
    {
        private static readonly ContentFacade Instance = new ContentFacade();

        private ContentFacade()
        {
        }

        public static ContentFacade GetInstance()
        {
            return Instance;
        }
    }
}