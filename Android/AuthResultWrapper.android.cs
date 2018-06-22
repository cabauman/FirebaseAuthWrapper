using Firebase.Auth;
using System.Threading.Tasks;

namespace GameCtor.Firebase.AuthWrapper
{
    public class AuthResultWrapper : IAuthResultWrapper
    {
        private IAuthResult _authResult;

        public AuthResultWrapper(IAuthResult authResult)
        {
            _authResult = authResult;
        }

        public IUserWrapper User
        {
            get { return new UserWrapper(_authResult.User); }
        }

        public IAdditionalUserInfoWrapper AdditionalUserInfo
        {
            get { return new AdditionalUserInfoWrapper(_authResult.AdditionalUserInfo); }
        }
    }
}
