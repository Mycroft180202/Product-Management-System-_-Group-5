using Product_Management_System.Models;

namespace Product_Management_System.Services
{
    public interface IAuthenticationService
    {
        User? Authenticate(string username, string password);
    }
}
