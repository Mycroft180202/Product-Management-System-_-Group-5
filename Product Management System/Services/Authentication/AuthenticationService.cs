using System.Linq;
using ProductManagementSystem.Repositories.Users;

namespace ProductManagementSystem.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool Authenticate(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService()
        {
            _userRepository = new UserRepository();
        }

        public bool Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && user.Password == password) 
            {
                return true;
            }
            return false;
        }
    }
}
