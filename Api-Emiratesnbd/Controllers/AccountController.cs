using DAL.Dto;
using DAL.Interfaces;
using DAL.User;
using Infrastrucure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;

namespace Api_Emiratesnbd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<EmiratesUser> _userManager;
        private readonly SignInManager<EmiratesUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<EmiratesUser> userManager, SignInManager<EmiratesUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;

        }

        [HttpPost("login")]
        public async Task<ActionResult<userDto>> login(loginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                var response = new userDto
                {
                    statu = false,
                    MessageEn = "Email is incorrect"

                };

                return response;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);


            if (!result.Succeeded)
            {
                {
                    var response = new userDto
                    {
                        statu = false,
                        MessageEn = "Your password is invalid",

                    };

                    return response;
                }
            }

            return new userDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                statu = true,
                MessageEn = "You have been logged in successfully"
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<userDto>> Regitser(RegisterDto registerDto)
        {

            var exitingUserName = _userManager.Users.FirstOrDefault(u => u.UserName == registerDto.DisplayName);

            if (exitingUserName != null)
            {
                var response = new userDto
                {
                    statu = false,
                    MessageEn = "The name is already in use",

                };

                return response;
            };

            var exitingEmail = _userManager.Users.FirstOrDefault(u => u.Email == registerDto.Email);

            if (exitingEmail != null)
            {
                var response = new userDto
                {
                    statu = false,
                    MessageEn = "Email is already in use",
                };

                return response;
            }


            var user = new EmiratesUser
            {
                Email = registerDto.Email,
                UserName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                FirstName = registerDto.DisplayName,
                LastName = registerDto.DisplayName,

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var response = new userDto
                {
                    statu = false,
                    MessageEn = "Failed to register",
                };

                return response;
            }
            else
            {
                return new userDto
                {
                    DisplayName = user.UserName,
                    Token = _tokenService.CreateToken(user),
                    MessageEn = "A new registration has been added successfully",
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    statu = true

                };
            }
        }
    }
}
