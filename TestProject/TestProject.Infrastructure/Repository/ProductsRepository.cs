using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;
using TestProject.Core.Interfaces;
using TestProject.Infrastructure.Data;

namespace TestProject.Infrastructure.Repository
{
    public class ProductsRepository : IProductRepository


    {
        private readonly TestContext _context;

        public ProductsRepository(TestContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Produc>> GetProducts()
        {
            var posts = await _context.Products.ToListAsync();
            return posts;
        }
        public async Task<Produc> GetProduct(int id)
        {
            var cliente = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return cliente;

        }
        public async Task RegisterProduct(Produc product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateProduct(Produc product)
        {
            var result = await GetProduct(product.Id);
            result.Name = product.Name;
            result.Description = product.Description;
            result.Price = product.Price;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var delete = await GetProduct(id);
            _context.Remove(delete);
            int row = await _context.SaveChangesAsync();
            return row > 0;
        }
    }

}
