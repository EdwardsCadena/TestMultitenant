using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;

namespace TestProject.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Produc>> GetProducts();
        Task RegisterProduct(Produc product);
        Task<Produc> GetProduct(int id);
        Task<bool> UpdateProduct(Produc product);
        Task<bool> DeleteProduct(int id);
    }
}
