using System;

namespace Honoplay.Application.Exceptions
{
    public class InvalidPasswordOrUserNameException : Exception
    {
    }

    public class TooManyAuthenticationFailuresException : Exception
    {
    }
}