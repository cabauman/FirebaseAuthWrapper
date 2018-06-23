using Firebase.Auth;
using System.Collections.Generic;

namespace GameCtor.Firebase.AuthWrapper
{
    public class AdditionalUserInfoWrapper : IAdditionalUserInfoWrapper
    {
        private AdditionalUserInfo _additionalUserInfo;

        public AdditionalUserInfoWrapper(AdditionalUserInfo additionalUserInfo)
        {
            _additionalUserInfo = additionalUserInfo;
        }

        //public IDictionary<string, object> Profile => _additionalUserInfo.Profile;

        public string ProviderId => _additionalUserInfo.ProviderId;

        public string Username => _additionalUserInfo.Username;
    }
}
