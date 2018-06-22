namespace GameCtor.Firebase.AuthWrapper
{
    public class PhoneNumberSignInResult
    {
        public IAuthResultWrapper AuthResult { get; set; }

        public string VerificationId { get; set; }
    }
}
