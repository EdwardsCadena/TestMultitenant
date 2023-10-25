using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Infrastructure.Tenant;

namespace TestProject.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static T? GetTenant<T>(this HttpContext context) where T : Tenant
        {
            if (!context.Items.ContainsKey(AppConstants.HttpContextTenantKey))
                return null;

            return context.Items[AppConstants.HttpContextTenantKey] as T;
        }

        public static Tenant? GetTenant(this HttpContext context) => context.GetTenant<Tenant>();

    }
}
