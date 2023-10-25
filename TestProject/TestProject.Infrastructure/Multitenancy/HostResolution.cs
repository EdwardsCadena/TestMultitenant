using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Interfaces;

namespace TestProject.Infrastructure.Multitenancy
{
    public class HostResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly HttpContext? _httpContext;

        public HostResolutionStrategy(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
        }

        public async Task<string> GetTenantIdentifierAsync()
        {
            if (_httpContext is null)
            {
                return string.Empty;
            }

            return await Task.FromResult(_httpContext.Request.Host.Host);
        }
    }
}
