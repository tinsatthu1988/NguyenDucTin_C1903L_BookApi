using NguyenDucTin_C1903L_BookApi.DTO;
using NguyenDucTin_C1903L_BookApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Interface
{
    public interface IUserRepository
    {
        void Update(AppUser user);

        void Delete(AppUser user);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUserNameAsync(string UserName);

        Task<MemberDto> GetMemberAsync(string UserName);
    }
}
