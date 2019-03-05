using System;

namespace Honoplay.Application.Exceptions
{
    public class TransactionException : Exception
    {
        public TransactionException() : base("Transaction Exception")
        {

        }
    }
}