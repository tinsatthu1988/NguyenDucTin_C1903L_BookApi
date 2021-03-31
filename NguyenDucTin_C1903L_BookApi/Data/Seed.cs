﻿using Microsoft.AspNetCore.Identity;
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
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync())
                return;
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole {Name="Member"},
                new AppRole {Name="Admin"},
                new AppRole {Name = "Moderator"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "123Admin!@#");
                await userManager.AddToRoleAsync(user, "Member");
            }
            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "123Admin!@#");
            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.AddToRoleAsync(admin, "Moderator");
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
