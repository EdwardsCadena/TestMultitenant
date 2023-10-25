using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Entities
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public string? Name { get; set; }
        public string? Identifier { get; set; }
    }
}
