using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        List<Category> GetAllWithProducts();
        Category GetById(int id);
        Category GetByIdWithProducts(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(Category category);
        bool IsExisted(int id);
        void Save();
    }
}
