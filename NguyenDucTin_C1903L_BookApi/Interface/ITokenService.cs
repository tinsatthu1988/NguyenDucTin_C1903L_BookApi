using NguyenDucTin_C1903L_BookApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
