using Amazon.DynamoDBv2.DataModel;
using Microsoft.EntityFrameworkCore;

namespace AdminBooksPanel.Repository
{
    public abstract class BaseRepository
    {
        protected readonly IDynamoDBContext _context;

        public BaseRepository(IDynamoDBContext context)
        {
            _context = context;
        }

    }
}
