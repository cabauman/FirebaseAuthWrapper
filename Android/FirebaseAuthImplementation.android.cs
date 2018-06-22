﻿using Android.App;
using Firebase;
using Firebase.Auth;
using Java.Util.Concurrent;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCtor.Firebase.AuthWrapper
{
    /// <summary>
    /// Main implementation for IFirebaseAuth
    /// </summary>
    public partial class FirebaseAuthImplementation : IFirebaseAuth
    {
        internal static IDictionary<Type, FirebaseAuthExceptionType> FirebaseExceptionTypeToEnumDict { get; } = new Dictionary<Type, FirebaseAuthExceptionType>
        {
            { typeof(FirebaseAuthActionCodeException), FirebaseAuthExceptionType.FirebaseAuthActionCode },
            { typeof(FirebaseAuthRecentLoginRequiredException), FirebaseAuthExceptionType.FirebaseAuthRecentLoginRequired },
            { typeof(FirebaseAuthWeakPasswordException), FirebaseAuthExceptionType.FirebaseAuthWeakPassword },
            { typeof(FirebaseAuthInvalidCredentialsException), FirebaseAuthExceptionType.FirebaseAuthInvalidCredentials },
            { typeof(FirebaseAuthInvalidUserException), FirebaseAuthExceptionType.FirebaseAuthInvalidUser },
            { typeof(FirebaseAuthUserCollisionException), FirebaseAuthExceptionType.FirebaseAuthUserCollision },
            { typeof(FirebaseAuthEmailException), FirebaseAuthExceptionType.FirebaseAuthEmail },
            { typeof(FirebaseApiNotAvailableException), FirebaseAuthExceptionType.FirebaseApiNotAvailable },
            { typeof(FirebaseTooManyRequestsException), FirebaseAuthExceptionType.FirebaseTooManyRequests },
        };

        private FirebaseAuth _auth;

        public FirebaseAuthImplementation()
        {
            _auth = FirebaseAuth.Instance;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public IUserWrapper CurrentUser
        {
            get { return new UserWrapper(_auth.CurrentUser); }
        }

        /// <summary>
        /// Signs in the user anonymously without requiring any credential.
        /// </summary>
        /// <returns>Task of IUserWrapper with the result of the operation</returns>
        public async Task<IUserWrapper> SignInAnonymouslyAsync()
        {
            try
            {
                IAuthResult authResult = await _auth.SignInAnonymouslyAsync();
                return new UserWrapper(authResult.User);
            }
            catch(Exception ex)
            {
                throw GetFirebaseAuthException(ex);
            }
        }

        /// <summary>
        /// Attempts to sign in to Firebase with the given phone number, or provides a verification ID if unable.
        /// </summary>
        /// <param name="phoneNumber">The phone number for the account the user is signing up for or signing into. Make sure to pass in a phone number with country code prefixed with plus sign ('+').</param>
        /// <returns>PhoneNumberSignInResult containing either a verification ID or an IAuthResultWrapper</returns>
        public async Task<PhoneNumberSignInResult> SignInWithPhoneNumberAsync(string phoneNumber)
        {
            var completionHandler = new PhoneNumberVerificationCallbackWrapper();
            PhoneAuthProvider.Instance.VerifyPhoneNumber(phoneNumber, 60, TimeUnit.Seconds, CrossCurrentActivity.Current.Activity, completionHandler);

            PhoneNumberVerificationResult result = null;
            try
            {
                result = await completionHandler.Verify();
            }
            catch(Exception ex)
            {
                throw GetFirebaseAuthException(ex);
            }

            if(result.AuthCredential != null)
            {
                IAuthResult authResult = null;
                try
                {
                    authResult = await _auth.SignInWithCredentialAsync(result.AuthCredential);
                }
                catch(Exception ex)
                {
                    throw GetFirebaseAuthException(ex);
                }

                IAuthResultWrapper authResultWrapper = new AuthResultWrapper(authResult);
                return new PhoneNumberSignInResult()
                {
                    AuthResult = authResultWrapper
                };
            }
            else
            {
                return new PhoneNumberSignInResult()
                {
                    VerificationId = result.VerificationId
                };
            }
        }

        /// <summary>
        /// Signs in to Firebase with the given phone number credentials.
        /// </summary>
        /// <param name="verificationId"></param>
        /// <param name="verificationCode">The 6 digit SMS-code sent to the user.</param>
        /// <returns>User account</returns>
        public async Task<IAuthResultWrapper> SignInWithPhoneNumberAsync(string verificationId, string verificationCode)
        {
            AuthCredential credential = PhoneAuthProvider.GetCredential(verificationId, verificationCode);
            return await SignInAsync(credential);
        }

        /// <summary>
        /// Signs in to Firebase with the given Google credentials.
        /// </summary>
        /// <param name="idToken">The ID Token from Google.</param>
        /// <param name="accessToken">The Access Token from Google.</param>
        /// <returns>User account</returns>
        public async Task<IAuthResultWrapper> SignInWithGoogleAsync(string idToken, string accessToken)
        {
            AuthCredential credential = GoogleAuthProvider.GetCredential(idToken, accessToken);
            return await SignInAsync(credential);
        }

        /// <summary>
        /// Signs in to Firebase with the given Facebook credentials.
        /// </summary>
        /// <param name="accessToken">The Access Token from Facebook.</param>
        /// <returns>User account</returns>
        public async Task<IAuthResultWrapper> SignInWithFacebookAsync(string accessToken)
        {
            AuthCredential credential = FacebookAuthProvider.GetCredential(accessToken);
            return await SignInAsync(credential);
        }

        /// <summary>
        /// Signs in to Firebase with the given Twitter credentials.
        /// </summary>
        /// <param name="token">The Twitter OAuth token.</param>
        /// <param name="secret">The Twitter OAuth secret.</param>
        /// <returns>User account</returns>
        public async Task<IAuthResultWrapper> SignInWithTwitterAsync(string token, string secret)
        {
            AuthCredential credential = TwitterAuthProvider.GetCredential(token, secret);
            return await SignInAsync(credential);
        }

        /// <summary>
        /// Signs in to Firebase with the given  Github credentials.
        /// </summary>
        /// <param name="token">The GitHub OAuth access token.</param>
        /// <returns>User account</returns>
        public async Task<IAuthResultWrapper> SignInWithGithubAsync(string token)
        {
            AuthCredential credential = GithubAuthProvider.GetCredential(token);
            return await SignInAsync(credential);
        }

        /// <summary>
        /// Signs the user in via email.
        /// </summary>
        /// <param name="email">The user’s email address.</param>
        /// <param name="password">The user’s password.</param>
        /// <returns>User account</returns>
        public async Task<IAuthResultWrapper> SignInWithEmailAsync(string email, string password)
        {
            try
            {
                IAuthResult authResult = await _auth.SignInWithEmailAndPasswordAsync(email, password);
                return new AuthResultWrapper(authResult);
            }
            catch(Exception ex)
            {
                throw GetFirebaseAuthException(ex);
            }
        }

        /// <summary>
        /// Signs out the current user.
        /// </summary>
        public void SignOut()
        {
            _auth.SignOut();
        }

        private async Task<IAuthResultWrapper> SignInAsync(AuthCredential credential)
        {
            try
            {
                IAuthResult authResult = await _auth.SignInWithCredentialAsync(credential);
                return new AuthResultWrapper(authResult);
            }
            catch(FirebaseException ex)
            {
                throw GetFirebaseAuthException(ex);
            }
        }

        private FirebaseAuthException GetFirebaseAuthException(Exception ex)
        {
            FirebaseExceptionTypeToEnumDict.TryGetValue(ex.GetType(), out FirebaseAuthExceptionType exceptionType);
            throw new FirebaseAuthException(ex.Message, ex, exceptionType);
        }
    }
}