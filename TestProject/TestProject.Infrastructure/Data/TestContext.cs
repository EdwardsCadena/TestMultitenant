using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;

namespace TestProject.Infrastructure.Data
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
        : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Produc> Products{ get; set; }

        

    }
}
