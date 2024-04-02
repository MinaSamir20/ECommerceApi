using ECommerceApi.Application.DTOs.Authentication;
using ECommerceApi.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerceApi.Application.Service.AuthService
{
    public interface IAuthService
    {
        Task<ResponseDto> SeedRolesAsync();
        Task<JwtSecurityToken> CreateJwtToken(AppUser user);
        Task<AuthResponseModel> RegisterAsync(RegisterDto register);
        Task<AuthResponseModel> LoginAsync(LoginDto loginDto);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByIdAsync(string id);
        Task<AppUser> UpdateUser(AppUser user);
        Task DeleteUserAsync(string id);
    }
}
