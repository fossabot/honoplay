using System;

namespace Honoplay.Common._Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException() : base("Notfound")
        {
        }

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}