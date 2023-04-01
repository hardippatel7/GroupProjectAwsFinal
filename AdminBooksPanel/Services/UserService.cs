using AdminBooksPanel.Models;
using AdminBooksPanel.Repository;
using System.Threading.Tasks;

namespace AdminBooksPanel.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> Create(User user)
        {
            return _userRepository.Create(user);
        }

        public Task<User> FindByUsername(string username)
        {
            return _userRepository.FindByUsername(username);
        }
    }
}
