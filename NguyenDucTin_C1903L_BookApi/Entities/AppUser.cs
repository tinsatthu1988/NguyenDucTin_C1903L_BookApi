using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Entities
{
    public class AppUser : IdentityUser<int>
    {
        //public int Id { get; set; }
        //public string UserName { get; set; }
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
        //public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
