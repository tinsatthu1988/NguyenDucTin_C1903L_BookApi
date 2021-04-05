using Microsoft.EntityFrameworkCore;
using NguyenDucTin_C1903L_BookApi.Entities;
using NguyenDucTin_C1903L_BookApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
        }

        public Task<bool> BookExists(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BookExists(string bookIsbn)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookByIdAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookByIsbnAsync(string bookIsbn)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books
                .Include(p => p.Photos)
                .Include(b => b.Category)
                .ToListAsync();
        }

        public Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
