using Product_Management_System.Models;

namespace Product_Management_System.Repositories
{
    public interface IUserRepository
    {
        User? GetUserByUsernameAndPassword(string username, string password);
    }
}
