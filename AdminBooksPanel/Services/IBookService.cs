using AdminBooksPanel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminBooksPanel.Services
{
    public interface IBookService
    {
        Task<Books> Save(Books book);
        Task<Books> GetById(string bookId);
        Task<IEnumerable<Books>> GetAll();
        Task Delete(string bookId);
    }
}
