using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        List<Product> GetAllWithCategory();
        Product GetById(int id);
        Product GetByIdWithCategory(int id);
        void Create(Product product);
        void Update(Product product);
        bool IsExisted(int id);
        void Delete(Product product);
        public List<Product> GetProductsByCategory(int CategoryId);
        public Product GetProductByName(string name);
        void Save();
    }
}
