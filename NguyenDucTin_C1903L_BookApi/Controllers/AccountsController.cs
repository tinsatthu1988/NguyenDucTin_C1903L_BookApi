using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDucTin_C1903L_BookApi.Data;
using NguyenDucTin_C1903L_BookApi.DTO;
using NguyenDucTin_C1903L_BookApi.Entities;
using NguyenDucTin_C1903L_BookApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(IUserRepository userRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.UserName)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
            if (user == null) return Unauthorized("Invalid Username");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }

        [Authorize(Policy = "MemberAdminRole")]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var UserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetUserByUserNameAsync(UserName);

            _mapper.Map(memberUpdateDto, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }

        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }



        
    }
}
