using Firebase.Auth;
using System.Threading.Tasks;

namespace GameCtor.Firebase.AuthWrapper
{
    public class AdditionalUserInfoWrapper : IAdditionalUserInfoWrapper
    {
        private AdditionalUserInfo _additionalUserInfo;

        public AdditionalUserInfoWrapper(AdditionalUserInfo additionalUserInfo)
        {
            _additionalUserInfo = additionalUserInfo;
        }
    }
}
