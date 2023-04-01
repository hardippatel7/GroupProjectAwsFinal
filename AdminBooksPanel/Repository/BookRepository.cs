using AdminBooksPanel.Models;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminBooksPanel.Repository
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(IDynamoDBContext context) : base(context)
        {
        }

        public async Task<Books> Save(Books book)
        {
            await _context.SaveAsync(book);
            return book;
        }

        public async Task<IEnumerable<Books>> GetAll()
        {
            return await _context.ScanAsync<Books>(default).GetRemainingAsync();
        }

        public async Task<Books> GetById(string bookId)
        {
            return await _context.LoadAsync<Books>(bookId);
        }

        public async void Delete(Books book)
        {
            await _context.DeleteAsync(book);
        }
    }
}
