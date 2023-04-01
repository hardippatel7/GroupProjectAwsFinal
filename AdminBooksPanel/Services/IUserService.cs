using AdminBooksPanel.Models;
using System.Threading.Tasks;

namespace AdminBooksPanel.Services
{
    public interface IUserService
    {
        Task<User> FindByUsername(string username);

        Task<User> Create(User user);
    }
}
