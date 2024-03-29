﻿using API.Extensions;
using AutoMapper;
//using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using YourStoreApi.Context;
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
        private AppIdentityContext _appidentitycontext;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                ITokenService tokenService, IMapper mapper, AppIdentityContext appidentitycontext)
            {
                _mapper = mapper;
                _tokenService = tokenService;
                _signInManager = signInManager;
                _userManager = userManager;
        }

            [HttpGet]
            [Authorize]
            public async Task<ActionResult<UserDto>> GetCurrentUser()
            {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

                return new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    UserName = user.UserName,
                    Role=user.Role
                };
            }

            [HttpGet("emailexists")]
            public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
            {
                return await _userManager.FindByEmailAsync(email) != null;
            }

            // [Authorize]
            // [HttpGet("address")]
            // public async Task<ActionResult<AddressDto>> GetUserAddress()
            // {
            // var email = User.FindFirstValue(ClaimTypes.Email);
            // var user = await _userManager.FindByEmailAsync(email);

            // return _mapper.Map<AddressDto>(user.Address);
            // }

            // [Authorize]
            // [HttpPut("address")]
            // public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
            // {
                
            //     var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            // user.Address = _mapper.Map<Address>(address);

            //     var result = await _userManager.UpdateAsync(user);

            //     if (result.Succeeded) return Ok(_mapper.Map<AddressDto>(user.Address));

            //     return BadRequest("Problem updating the user");
            // }
         [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            return _mapper.Map<AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            user.Address = _mapper.Map<Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<AddressDto>(user.Address));

            return BadRequest("Problem updating the user");
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser(UserDto userDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(User);
            user.Email=userDto.Email;
            user.UserName=userDto.UserName;
            // user.Address = _mapper.Map<Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) {
                return userDto;
            }
            
            return BadRequest("Problem updating the user");
        }

            [HttpPost("login")]
            public async Task<ActionResult<UserDto>> Login(object obj)
            {
            var login = obj.ToString();
            var loginDto = (JsonSerializer.Deserialize<LoginDto>(login));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user == null) return Unauthorized(new ApiResponse(401));

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                UserName=user.UserName,
                Token = _tokenService.CreateToken(user),
               // DisplayName = user.DisplayName
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
                    //DisplayName = registerDto.UserName,
                    Email = registerDto.Email,
                    UserName = registerDto.UserName,

                    
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded) return BadRequest(new ApiResponse(400));

                return new UserDto
                {
                    //DisplayName = user.DisplayName,
                    UserName=user.UserName,
                    Token = _tokenService.CreateToken(user),
                    Email = user.Email
                };
            }
        }

    }
