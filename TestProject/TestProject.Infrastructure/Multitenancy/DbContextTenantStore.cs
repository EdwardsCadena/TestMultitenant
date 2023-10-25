using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Interfaces;
using TestProject.Infrastructure.Data;

namespace TestProject.Infrastructure.Multitenancy
{
    internal class DbContextTenantStore : ITenantStore<Tenant>
    {
        private readonly TestContext _context;
        private readonly IMemoryCache _cache;

        public DbContextTenantStore(TestContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Tenant> GetTenantAsync(string identifier)
        {
            var cacheKey = $"Cache_{identifier}";
            var tenant = _cache.Get<Tenant>(cacheKey);

            if (tenant is null)
            {
                var entity = await _context.Tenants
                    .FirstOrDefaultAsync(q => q.Identifier == identifier)
                        ?? throw new ArgumentNullException($"identifier no es un tenant válido");

                tenant = new Tenant(entity.TenantId, entity.Identifier);

                tenant.Items["Name"] = entity.Name;

                _cache.Set(cacheKey, tenant);
            }

            return tenant;
        }
    }
}
