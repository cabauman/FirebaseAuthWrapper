using System;

namespace GameCtor.Firebase.AuthWrapper
{
    public class FirebaseAuthException : Exception
    {
        public FirebaseAuthException()
        {
        }

        public FirebaseAuthException(string message)
            : base(message)
        {
        }

        public FirebaseAuthException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public FirebaseAuthException(string message, FirebaseAuthExceptionType exceptionType)
            : base(message)
        {
            ExceptionType = exceptionType;
        }

        public FirebaseAuthException(string message, Exception inner, FirebaseAuthExceptionType exceptionType)
            : base(message, inner)
        {
            ExceptionType = exceptionType;
        }

        FirebaseAuthExceptionType ExceptionType { get; }
    }
}
