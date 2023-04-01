using AdminBooksPanel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminBooksPanel.Repository
{
    public interface IBookRepository
    {
        Task<Books> GetById(string bookId);

        Task<IEnumerable<Books>> GetAll();

        Task<Books> Save(Books book);

        void Delete(Books book);


    }
}
