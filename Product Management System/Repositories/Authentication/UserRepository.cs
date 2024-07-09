using System.Linq;
using Product_Management_System.Data;
using Product_Management_System.Models;

namespace Product_Management_System.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductManagementDbContext _context;

        public UserRepository(ProductManagementDbContext context)
        {
            _context = context;
        }

        public User? GetUserByUsernameAndPassword(string username, string password)
        {
            return _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive);
        }
    }
}
