using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Infrastructure.Tenant;

namespace TestProject.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultiTenancy<T>(this IApplicationBuilder builder) where T : Tenant
        => builder.UseMiddleware<TenantMiddleware<T>>();

        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder)
            => builder.UseMiddleware<TenantMiddleware<Tenant>>();
    }
}
