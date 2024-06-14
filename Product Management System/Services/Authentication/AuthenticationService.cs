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
            // Sử dụng DI hoặc Factory pattern để quản lý đối tượng thực tế
            _userRepository = new UserRepository();
        }

        public bool Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && user.Password == password) // Lưu ý: Không nên so sánh mật khẩu theo cách này trong thực tế.
            {
                return true;
            }
            return false;
        }
    }
}
