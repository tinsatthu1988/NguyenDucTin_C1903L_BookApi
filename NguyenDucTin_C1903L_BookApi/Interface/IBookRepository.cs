using NguyenDucTin_C1903L_BookApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Interface
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<Book> GetBookByIsbnAsync(string bookIsbn);
        Task<bool> BookExists(int bookId);
        Task<bool> BookExists(string bookIsbn);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(string categoryName);
    }
}
