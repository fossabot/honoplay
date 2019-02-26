using System;
using System.Collections.Generic;
using System.Text;

namespace Honoplay.Application.Exceptions
{
    public class InvalidPasswordOrUserNameException : Exception
    {
    }

    public class TooManyAuthenticationFailuresException : Exception
    {
    }
     
}
