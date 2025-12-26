using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace E_CommerceSystem.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context context;

        public ProductRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Product item)
        {
            context.Products.Add(item);
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public List<Product> GetAllWithCategory()
        {
            return context.Products.Include(p => p.Category).ToList();
        }

        public Product GetById(int id)
        {
            return context.Products.SingleOrDefault(p => p.Id == id);
        }
        public Product GetByIdWithCategory(int id)
        {
            return context.Products.Include(p => p.Category).SingleOrDefault(p => p.Id == id);
        }
        public bool IsExisted(int id)
        {
            return context.Products.Any(p => p.Id == id);
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
        }
        public List<Product> GetProductsByCategory(int CategoryId)
        {
            return context.Products.Where(p => p.CategoryId == CategoryId).ToList();
        }
        public Product GetProductByName(string name)
        {
            return context.Products.SingleOrDefault(p => p.Name == name);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
