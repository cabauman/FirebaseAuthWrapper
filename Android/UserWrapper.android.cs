using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;

namespace GameCtor.Firebase.AuthWrapper
{
    public class UserWrapper : IUserWrapper
    {
        private FirebaseUser _user;

        public UserWrapper(FirebaseUser user)
        {
            _user = user;
        }

        /// <summary>
        /// Attaches the given phone credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="verificationId">The verification ID obtained by calling VerifyPhoneNumber.</param>
        /// <param name="verificationCode">The 6 digit SMS-code sent to the user.</param>
        /// <returns>Updated current account</returns>
        public async Task<IAuthResultWrapper> LinkWithPhoneNumberAsync(string verificationId, string verificationCode)
        {
            AuthCredential credential = PhoneAuthProvider.GetCredential(verificationId, verificationCode);
            return await LinkWithCredentialAsync(credential);
        }

        /// <summary>
        /// Attaches the given Google credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="idToken"></param>
        /// <param name="accessToken"></param>
        /// <returns>Updated current account</returns>
        public async Task<IAuthResultWrapper> LinkWithGoogleAsync(string idToken, string accessToken)
        {
            AuthCredential credential = GoogleAuthProvider.GetCredential(idToken, accessToken);
            return await LinkWithCredentialAsync(credential);
        }

        /// <summary>
        /// Attaches the given Facebook credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="accessToken">The Access Token from Facebook.</param>
        /// <returns>Updated current account</returns>
        public async Task<IAuthResultWrapper> LinkWithFacebookAsync(string accessToken)
        {
            AuthCredential credential = FacebookAuthProvider.GetCredential(accessToken);
            return await LinkWithCredentialAsync(credential);
        }

        /// <summary>
        /// Attaches the given Twitter credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="token">The Twitter OAuth token.</param>
        /// <param name="secret">The Twitter OAuth secret.</param>
        /// <returns>Updated current account</returns>
        public async Task<IAuthResultWrapper> LinkWithTwitterAsync(string token, string secret)
        {
            AuthCredential credential = TwitterAuthProvider.GetCredential(token, secret);
            return await LinkWithCredentialAsync(credential);
        }

        /// <summary>
        /// Attaches the given Github credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="token">The GitHub OAuth access token.</param>
        /// <returns>Updated current account</returns>
        public async Task<IAuthResultWrapper> LinkWithGithubAsync(string token)
        {
            AuthCredential credential = GithubAuthProvider.GetCredential(token);
            return await LinkWithCredentialAsync(credential);
        }

        /// <summary>
        /// Attaches the given email credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="email">The user’s email address.</param>
        /// <param name="password">The user’s password.</param>
        /// <returns>Updated current account</returns>
        public async Task<IAuthResultWrapper> LinkWithEmailAsync(string email, string password)
        {
            AuthCredential credential = EmailAuthProvider.GetCredential(email, password);
            return await LinkWithCredentialAsync(credential);
        }

        /// <summary>
        /// Detaches credentials from a given provider type from this user. This prevents the user from signing in to this account in the future with credentials from such provider.
        /// </summary>
        /// <param name="providerId">A unique identifier of the type of provider to be unlinked.</param>
        /// <returns>Task of IUserWrapper</returns>
        public async Task<IUserWrapper> UnlinkAsync(string providerId)
        {
            try
            {
                IAuthResult authResult = await _user.UnlinkAsync(providerId);
                return new UserWrapper(authResult.User);
            }
            catch(FirebaseException ex)
            {
                throw GetFirebaseAuthException(ex);
            }
        }

        /// <summary>
        /// Fetches a Firebase Auth ID Token for the user.
        /// </summary>
        /// <param name="forceRefresh">Force refreshes the token. Should only be set to true if the token is invalidated out of band.</param>
        /// <returns>Task with the ID Token</returns>
        public async Task<string> GetIdTokenAsync(bool forceRefresh)
        {
            try
            {
                GetTokenResult tokenResult = await _user.GetIdTokenAsync(forceRefresh);
                return tokenResult.Token;
            }
            catch(FirebaseException ex)
            {
                throw GetFirebaseAuthException(ex);
            }
        }

        private async Task<IAuthResultWrapper> LinkWithCredentialAsync(AuthCredential credential)
        {
            try
            {
                IAuthResult authResult = await _user.LinkWithCredentialAsync(credential);
                return new AuthResultWrapper(authResult);
            }
            catch(FirebaseException ex)
            {
                throw GetFirebaseAuthException(ex);
            }
        }

        private FirebaseAuthException GetFirebaseAuthException(Exception ex)
        {
            FirebaseAuthImplementation.FirebaseExceptionTypeToEnumDict.TryGetValue(ex.GetType(), out FirebaseAuthExceptionType exceptionType);
            throw new FirebaseAuthException(ex.Message, ex, exceptionType);
        }
    }
}
