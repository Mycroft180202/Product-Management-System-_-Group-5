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
            // Lấy người dùng từ repository
            var user = _userRepository.GetUserByUsernameAndPassword(username, password);

            // Nếu tìm thấy người dùng và người dùng đang hoạt động, trả về người dùng, nếu không thì trả về null
            return user;
        }
    }
}
