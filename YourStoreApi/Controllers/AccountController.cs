﻿using AutoMapper;
//using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YourStoreApi.Errors;
using YourStoreApi.Models.Dto;
using YourStoreApi.Services;


namespace YourStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly ITokenService _tokenService;
            private readonly IMapper _mapper;
        protected ClaimsIdentity claimsIdentity { get; set; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                ITokenService tokenService, IMapper mapper)
            {
                _mapper = mapper;
                _tokenService = tokenService;
                _signInManager = signInManager;
                _userManager = userManager;
        }

            [Authorize]
            [HttpGet]
            public async Task<ActionResult<UserDto>> GetCurrentUser()
            {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var email=User.FindFirstValue(ClaimTypes.Email);

                return new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    DisplayName = user.UserName
                };
            }

            [HttpGet("emailexists")]
            public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
            {
                return await _userManager.FindByEmailAsync(email) != null;
            }

            [Authorize]
            [HttpGet("address")]
            public async Task<ActionResult<AddressDto>> GetUserAddress()
            {
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            return _mapper.Map<AddressDto>(user.Address);
            }

            [Authorize]
            [HttpPut("address")]
            public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
            {
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            user.Address = _mapper.Map<Address>(address);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) return Ok(_mapper.Map<AddressDto>(user.Address));

                return BadRequest("Problem updating the user");
            }


            [HttpPost("login")]
            public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user == null) return Unauthorized(new ApiResponse(401));

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.UserName
                };
            }

            [HttpPost("register")]
            public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
            {
                if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
                {
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
                }

                var user = new AppUser
                {
                    NormalizedUserName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    UserName = registerDto.DisplayName,
                    
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded) return BadRequest(new ApiResponse(400));

                return new UserDto
                {
                    DisplayName = user.UserName,
                    Token = _tokenService.CreateToken(user),
                    Email = user.Email
                };
            }
        }

    }