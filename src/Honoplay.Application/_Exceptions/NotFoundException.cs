﻿using System;

namespace Honoplay.Application.Exceptions
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