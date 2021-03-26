using Microsoft.EntityFrameworkCore;
using NguyenDucTin_C1903L_BookApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.Username = user.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123Admin!@#"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedBooks(DataContext context)
        {
            if (await context.Books.AnyAsync())
                return;
            var bookData = await System.IO.File.ReadAllTextAsync("Data/BookSeedData.json");

            var books = JsonSerializer.Deserialize<List<Book>>(bookData);


            foreach (var book in books)
            {
                context.Books.Add(book);
            }

            await context.SaveChangesAsync();
        }
    }
}
