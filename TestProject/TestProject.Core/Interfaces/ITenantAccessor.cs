using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;

namespace TestProject.Core.Interfaces
{
    public interface ITenantAccessor<T> where T : Tenant
    {
        public T? Tenant { get; init; }
    }

}
