using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace Honoplay.Application
{
    public class AssemblyIdentifier
    {
        public static Assembly Get() =>     
            typeof(AssemblyIdentifier).GetTypeInfo().Assembly;
    }
}
