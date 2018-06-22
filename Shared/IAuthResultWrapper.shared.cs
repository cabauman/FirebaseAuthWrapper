using System.Threading.Tasks;

namespace GameCtor.Firebase.AuthWrapper
{
    public interface IAuthResultWrapper
    {
        IUserWrapper User { get; }

        IAdditionalUserInfoWrapper AdditionalUserInfo { get; }
    }
}
