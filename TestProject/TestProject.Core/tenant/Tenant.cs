using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.tenant
{
    public record Tenant(int Id, string Identifier)
    {
        public Dictionary<string, object> Items { get; init; } =
            new Dictionary<string, object>();
    }
}
