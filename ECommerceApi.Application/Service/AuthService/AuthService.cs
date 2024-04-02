using ECommerceApi.Application.DTOs.Authentication;
using ECommerceApi.Domain.Entities.Identity;
using ECommerceApi.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceApi.Application.Service.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext db, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId!);
            var role = await _roleManager.RoleExistsAsync(model.Role!);
            if (user is null || role == false)
                return "Invalid User ID or Role";
            if (await _userManager.IsInRoleAsync(user, model.Role!))
                return "User already assigned to this Role";

            var result = await _userManager.AddToRoleAsync(user, model.Role!);
            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        public async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roleClaims = userRoles.Select(x => new Claim("roles", x)).ToList();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("Uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_configuration["JWT:DurationInDays"]!)),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthResponseModel> LoginAsync(LoginDto login)
        {
            var authModel = new AuthResponseModel();
            var user = await _userManager.FindByEmailAsync(login.Email!);
            if (user is null || !await _userManager.CheckPasswordAsync(user, login.Password!))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiredOn = jwtSecurityToken.ValidTo;
            authModel.Roles!.Add(rolesList.ToString()!);
            return authModel;
        }

        public async Task<AuthResponseModel> RegisterAsync(RegisterDto register)
        {
            // Check if Email is Exist or Not
            if (await _userManager.FindByEmailAsync(register.Email!) is not null)
                return new() { Message = "Email is already registered!" };
            // Check if UserName is Exist or Not
            if (await _userManager.FindByNameAsync(register.UserName!) is not null)
                return new() { Message = "Username is already registered!" };
            // Create User
            AppUser user = new()
            {
                UserName = register.UserName,
                Email = register.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, register.Password!);
            // if result not succeeded
            if (!result.Succeeded)
            {
                StringBuilder errors = new();
                foreach (var error in result.Errors)
                    errors.Append($"{error.Description},");

                return new() { Message = errors.ToString() };
            }
            // Add Role to user
            await _userManager.AddToRoleAsync(user, register.Role!);
            // Create JwtToken
            var jwtSecurityToken = await CreateJwtToken(user);

            return new()
            {
                Email = user.Email,
                ExpiredOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new() { register.Role! },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }

        public async Task<ResponseDto> SeedRolesAsync()
        {
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync("ADMIN");
            bool isUserRoleExists = await _roleManager.RoleExistsAsync("USER");

            if (isUserRoleExists && isAdminRoleExists)
            {
                return new()
                {
                    IsSuccess = true,
                    DisplayMessage = "Roles Seeding is Already Done"
                };
            }
            await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
            await _roleManager.CreateAsync(new IdentityRole("USER"));

            return new()
            {
                IsSuccess = true,
                DisplayMessage = "Role Seeding Done Successfully"
            };
        }

        public async Task<AppUser> UpdateUser(AppUser user)
        {
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(string id)
        {
            var result = await _db.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (result != null)
                _db.Users.Remove(result);
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync() => await _db.Users.ToListAsync();

        public async Task<AppUser> GetUserByIdAsync(string id) => (await _db.Users.Where(a => a.Id == id).FirstOrDefaultAsync())!;
    }
}
