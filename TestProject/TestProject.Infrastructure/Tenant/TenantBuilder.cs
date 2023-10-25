using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Interfaces;

namespace TestProject.Infrastructure.Tenant
{
    public class TenantBuilder<T> where T : Tenant
    {
        private readonly IServiceCollection _services;

        public TenantBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public TenantBuilder<T> WithResolutionStrategy<V>(ServiceLifetime lifetime = ServiceLifetime.Transient)
            where V : class, ITenantResolutionStrategy
        {
            _services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantResolutionStrategy), typeof(V), lifetime));
            return this;
        }

        public TenantBuilder<T> WithStore<V>(ServiceLifetime lifetime = ServiceLifetime.Transient)
            where V : class, ITenantStore<T>
        {
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantStore<T>), typeof(V), lifetime));
            return this;
        }
    
    }
}
