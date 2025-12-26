using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context context;

        public CategoryRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Category item)
        {
            context.Categories.Add(item);
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public List<Category> GetAllWithProducts()
        {
            return context.Categories.Include(c => c.Products).ToList();
        }

        public Category GetById(int id)
        {
            return context.Categories.SingleOrDefault(c => c.Id == id);
        }
        public Category GetByIdWithProducts(int id)
        {
            return context.Categories.Include(c => c.Products).SingleOrDefault(c => c.Id == id);
        }
        public bool IsExisted(int id)
        {
            return context.Categories.Any(c => c.Id == id);
        }

        public void Update(Category category)
        {
            context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            context.Categories.Remove(category);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
