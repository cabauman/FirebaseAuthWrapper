using Firebase.Auth;
using System.Threading.Tasks;

namespace GameCtor.Firebase.AuthWrapper
{
    public class AdditionalUserInfoWrapper : IAdditionalUserInfoWrapper
    {
        private IAdditionalUserInfo _additionalUserInfo;

        public AdditionalUserInfoWrapper(IAdditionalUserInfo additionalUserInfo)
        {
            _additionalUserInfo = additionalUserInfo;
        }
    }
}
