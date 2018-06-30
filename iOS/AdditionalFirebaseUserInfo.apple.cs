using Firebase.Auth;
using System.Collections.Generic;

namespace GameCtor.Firebase.AuthWrapper
{
    public class AdditionalFirebaseUserInfo : IAdditionalFirebaseUserInfo
    {
        private AdditionalUserInfo _additionalUserInfo;

        public AdditionalFirebaseUserInfo(AdditionalUserInfo additionalUserInfo)
        {
            _additionalUserInfo = additionalUserInfo;
        }

        //public IDictionary<string, object> Profile => _additionalUserInfo.Profile;

        public string ProviderId => _additionalUserInfo.ProviderId;

        public string Username => _additionalUserInfo.Username;
    }
}
