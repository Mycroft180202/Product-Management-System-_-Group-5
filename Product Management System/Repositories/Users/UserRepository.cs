using System.Linq;
using System.Windows.Forms;
using Product_Management_System.Data;
using Product_Management_System.Models;

namespace ProductManagementSystem.Repositories.Users
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ProductManagementDbContext _context;

        public UserRepository()
        {
            // Sử dụng DI hoặc Factory pattern để quản lý đối tượng thực tế
            _context = new ProductManagementDbContext();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username);
        }
    }
}
