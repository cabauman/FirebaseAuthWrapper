using System.Threading.Tasks;

namespace GameCtor.Firebase.AuthWrapper
{
    public interface IFirebaseAuthResult
    {
        IFirebaseUser User { get; }

        IAdditionalFirebaseUserInfo AdditionalUserInfo { get; }
    }
}
