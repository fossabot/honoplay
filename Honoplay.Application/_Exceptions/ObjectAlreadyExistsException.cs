﻿using System;

namespace Honoplay.Application.Exceptions
{
    public sealed class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException() : base("Object Already Exists")
        {
        }

        public ObjectAlreadyExistsException(string name, object key) : base($"Entity \"{name}\" ({key}) is existing.")
        {
        }
    }
}