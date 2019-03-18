using System.Threading.Tasks;
using WebApi.Jwt.Core.Domain.Entities.Requests;
using WebApi.Jwt.Core.Domain.Entities.Responses;

namespace WebApi.Jwt.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest message);

        Task<ExchangeRefreshTokenResponse> ExchangeRefreshTokenAsync(ExchangeRefreshTokenRequest message);
    }
}