using AdminBooksPanel.Models;
using AdminBooksPanel.Repository;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminBooksPanel.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Books> Save(Books book)
        {
            return await _bookRepository.Save(book);
        }

        public async Task<Books> GetById(string bookId)
        {
            return await _bookRepository.GetById(bookId);
        }


        public async Task<IEnumerable<Books>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task Delete(string bookId)
        {
            Books book = await _bookRepository.GetById(bookId);
            _bookRepository.Delete(book);
        }

    }
}
