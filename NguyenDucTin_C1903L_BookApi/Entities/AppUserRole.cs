using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser Users { get; set; }
        public AppRole Role { get; set; }
    }
}
