using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;

namespace TestTask.Services.Interfaces.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext Context;
        public UserService (ApplicationDbContext context)
        {
            Context = context;
        }

        public Task<User> GetUser()
        {
            User result = Context.Users.Include(u => u.Orders).First();
            foreach (var user in Context.Users.Include(u => u.Orders))
            {
                result = user.Orders.Count > result.Orders.Count ? user : result;
            }
            return Task.FromResult(result);
        }

        public Task<List<User>> GetUsers()
        {
            var result = Context.Users.Include(u => u.Orders).Where(u => u.Status == UserStatus.Inactive).ToList();
            return Task.FromResult(result);
        }
    }
}
