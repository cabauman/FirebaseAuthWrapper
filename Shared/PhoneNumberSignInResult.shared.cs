namespace GameCtor.Firebase.AuthWrapper
{
    public class PhoneNumberSignInResult
    {
        public IFirebaseAuthResult AuthResult { get; set; }

        public string VerificationId { get; set; }
    }
}
