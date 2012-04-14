namespace Feihuobuke.Battleship.Library
{
    public class IamgeFacade : FacadeBase<IamgeDao>
    {
        private static readonly IamgeFacade Instance = new IamgeFacade();

        private IamgeFacade()
        {
        }

        public static IamgeFacade GetInstance()
        {
            return Instance;
        }
    }
}