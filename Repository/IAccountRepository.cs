using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repository
{
    public interface IAccountRepository
    {
        Task Create(User user);
        Task<bool> CheckPassword(User user, string password);
        Task<User> FindByUsername(string Username);
        User Profile(int id);
        void Save();
    }
}
