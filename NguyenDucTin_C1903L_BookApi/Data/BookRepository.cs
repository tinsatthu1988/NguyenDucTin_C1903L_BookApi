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

        public async Task<bool> BookExists(int bookId)
        {
             return await _context.Books.AnyAsync(b => b.Id == bookId);
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }

        public Task<Book> GetBookByIdAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books
                .Include(p => p.Photos)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(string categoryName)
        {
            return await _context.Books
                .Include(p => p.Photos)
                .Include(c => c.Category)
                .Where(c => c.Category.Name.ToLower() == categoryName)
                .ToListAsync();
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }
    }
}
