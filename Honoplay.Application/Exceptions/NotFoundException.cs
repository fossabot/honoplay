using System;
using System.Text;

namespace Honoplay.Application.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
