﻿using System;

namespace GameCtor.Firebase.AuthWrapper
{
    public class FirebaseAuthException : Exception
    {
        public FirebaseAuthException(string message, FirebaseAuthExceptionType exceptionType)
            : base(message)
        {
            FirebaseAuthExceptionType = exceptionType;
        }

        public FirebaseAuthException(string message, Exception inner, FirebaseAuthExceptionType exceptionType)
            : base(message, inner)
        {
            FirebaseAuthExceptionType = exceptionType;
        }

        public FirebaseAuthExceptionType FirebaseAuthExceptionType { get; }
    }
}
