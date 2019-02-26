using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace Honoplay.Domain.Entities
{
    public class Tenant
    {
        public Tenant()
        {
            //Default values for non nullable refs.
            Name = HostName = "";
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public string HostName { get; set; }

        public byte[]? Logo { get; set; }
    }
}
