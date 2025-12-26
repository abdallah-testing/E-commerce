using E_CommerceSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_CommerceSystem.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Context context;

        public AccountRepository(Context context)
        {
            this.context = context;
        }
        public async Task Create(User user)
        {
            await context.Users.AddAsync(user);
            Save();
        }
        public async Task<User> FindByUsername(string Username)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Username == Username);
        }
        public async Task<bool> CheckPassword(User user, string password)
        {
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            //bool isValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return BCrypt.Net.BCrypt.Verify(password, user.Password);

        }

        public User Profile(int id)
        {
            return context.Users.SingleOrDefault(u => u.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
