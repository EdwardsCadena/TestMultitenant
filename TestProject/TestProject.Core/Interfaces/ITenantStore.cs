using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;

namespace TestProject.Core.Interfaces
{
    public interface ITenantStore<T> where T : Tenant
    {
        Task<T> GetTenantAsync(string identifier);
    
    }
}
