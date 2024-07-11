using Product_Management_System.Models;
using Product_Management_System.Repositories;

namespace Product_Management_System.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsernameAndPassword(username, password);
            return user;
        }
    }
}
