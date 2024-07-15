using Product_Management_System.Models;

namespace Product_Management_System.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User GetUserByUsername(string username);
        void AddUser(User newUser);
        User GetUserByUsernameAndPassword(string username, string password);
    }
}
