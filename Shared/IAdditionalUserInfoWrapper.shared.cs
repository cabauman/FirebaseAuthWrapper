using System.Collections.Generic;

namespace GameCtor.Firebase.AuthWrapper
{
    public interface IAdditionalUserInfoWrapper
    {
        //IDictionary<string, object> Profile { get; }

        string ProviderId { get; }

        string Username { get; }
    }
}
