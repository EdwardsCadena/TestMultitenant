using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;
using TestProject.Infrastructure.Extensions;
using TestProject.Infrastructure.Tenant;

namespace TestProject.Infrastructure.Data
{
    public class MultiTenantDbContext : DbContext
    {
        private readonly int _tenantId;

        public MultiTenantDbContext(
            DbContextOptions<MultiTenantDbContext> options,
            IHttpContextAccessor contextAccessor) : base(options)
        {
            var currentTenant = contextAccessor.HttpContext?.GetTenant();
            _tenantId = currentTenant?.Id ?? 0;

            this.Filter<Tenant>(f => f.Where(q => q.TenantId == _tenantId));
        }
    }
}
