using System;

namespace Honoplay.Application.Exceptions
{
    public sealed class DataExistingException : Exception
    {
        public DataExistingException(string name, object key) : base($"Entity \"{name}\" ({key}) is existing.")
        {
        }
    }
}