using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Infrastructure.Tenant;

namespace TestProject.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static TenantBuilder<T> AddMultiTenancy<T>(this IServiceCollection services) where T : Tenant
    => new(services);

        public static TenantBuilder<Tenant> AddMultiTenancy(this IServiceCollection services)
            => new(services);
    }
}
