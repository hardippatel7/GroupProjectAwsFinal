using AdminBooksPanel.Models;
using System.Threading.Tasks;

namespace AdminBooksPanel.Repository
{
    public interface IUserRepository
    {
        Task<User> FindByUsername(string username);

        Task<User> Create(User user);
    }
}
