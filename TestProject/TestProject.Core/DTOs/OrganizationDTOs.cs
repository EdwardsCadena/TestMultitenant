using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Entities
{
    public class OrganizationDTOs

    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
