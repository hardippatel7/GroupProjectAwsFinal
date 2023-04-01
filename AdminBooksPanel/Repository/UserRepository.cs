using AdminBooksPanel.Models;
using Amazon.DynamoDBv2.DataModel;
using System.Threading.Tasks;

namespace AdminBooksPanel.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDynamoDBContext context) : base(context)
        {
        }

        public async Task<User> Create(User user)
        {
            await _context.SaveAsync(user);
            return user;
        }

        public async Task<User> FindByUsername(string userName)
        {
            return await _context.LoadAsync<User>(userName);
        }
    }
}
