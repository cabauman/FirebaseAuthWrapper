﻿using System.Threading.Tasks;

namespace GameCtor.Firebase.AuthWrapper
{
    public interface IUserWrapper
    {
        /// <summary>
        /// Attaches the given phone credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="verificationId">The verification ID obtained by calling VerifyPhoneNumber.</param>
        /// <param name="verificationCode">The 6 digit SMS-code sent to the user.</param>
        /// <returns>Updated current account</returns>
        Task<IAuthResultWrapper> LinkWithPhoneNumberAsync(string verificationId, string verificationCode);

        /// <summary>
        /// Attaches the given Google credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="idToken">The ID Token from Google.</param>
        /// <param name="accessToken">The Access Token from Google.</param>
        /// <returns>Updated current account</returns>
        Task<IAuthResultWrapper> LinkWithGoogleAsync(string idToken, string accessToken);

        /// <summary>
        /// Attaches the given Facebook credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="accessToken">The Access Token from Facebook.</param>
        /// <returns>Updated current account</returns>
        Task<IAuthResultWrapper> LinkWithFacebookAsync(string accessToken);

        /// <summary>
        /// Attaches the given Twitter credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="token">The Twitter OAuth token.</param>
        /// <param name="secret">The Twitter OAuth secret.</param>
        /// <returns>Updated current account</returns>
        Task<IAuthResultWrapper> LinkWithTwitterAsync(string token, string secret);

        /// <summary>
        /// Attaches the given Github credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="token">The GitHub OAuth access token.</param>
        /// <returns>Updated current account</returns>
        Task<IAuthResultWrapper> LinkWithGithubAsync(string token);

        /// <summary>
        /// Attaches the given email credentials to the user. This allows the user to sign in to this account in the future with credentials for such provider.
        /// </summary>
        /// <param name="email">The user’s email address.</param>
        /// <param name="password">The user’s password.</param>
        /// <returns>Updated current account</returns>
        Task<IAuthResultWrapper> LinkWithEmailAsync(string email, string password);

        /// <summary>
        /// Detaches credentials from a given provider type from this user. This prevents the user from signing in to this account in the future with credentials from such provider.
        /// </summary>
        /// <param name="providerId">A unique identifier of the type of provider to be unlinked.</param>
        /// <returns>Task of IUserWrapper</returns>
        Task<IUserWrapper> UnlinkAsync(string providerId);

        /// <summary>
        /// Fetches a Firebase Auth ID Token for the user.
        /// </summary>
        /// <param name="forceRefresh">Force refreshes the token. Should only be set to true if the token is invalidated out of band.</param>
        /// <returns>Task with the ID Token</returns>
        Task<string> GetIdTokenAsync(bool forceRefresh);
    }
}
