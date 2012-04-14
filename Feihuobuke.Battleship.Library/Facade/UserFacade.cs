namespace Feihuobuke.Battleship.Library
{
    public class UserFacade : FacadeBase<UserDao>
    {
        private static readonly UserFacade Instance = new UserFacade();

        private UserFacade()
        {
        }

        public static UserFacade GetInstance()
        {
            return Instance;
        }
    }
}