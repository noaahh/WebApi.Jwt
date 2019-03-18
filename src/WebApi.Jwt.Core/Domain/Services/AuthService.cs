using System.Linq;
using System.Threading.Tasks;
using WebApi.Jwt.Core.Domain.Entities;
using WebApi.Jwt.Core.Domain.Entities.Requests;
using WebApi.Jwt.Core.Domain.Entities.Responses;
using WebApi.Jwt.Core.Interfaces.Gateways.Repositories;
using WebApi.Jwt.Core.Interfaces.Services;
using WebApi.Jwt.Core.Specifications;

namespace WebApi.Jwt.Core.Domain.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly ITokenFactory _tokenFactory;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository, IJwtFactory jwtFactory, ITokenFactory tokenFactory,
            IJwtTokenValidator jwtTokenValidator)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _tokenFactory = tokenFactory;
            _jwtTokenValidator = jwtTokenValidator;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest message)
        {
            if (string.IsNullOrEmpty(message.UserName) || string.IsNullOrEmpty(message.Password))
                return new LoginResponse(new[] {new Error("login_failure", "Invalid username or password.")});

            // ensure we have a user with the given user name
            var user = await _userRepository.FindByName(message.UserName);
            if (user == null)
                return new LoginResponse(new[] {new Error("login_failure", "Invalid username or password.")});

            // validate password
            if (!_userRepository.CheckPassword(message.Password, user.PasswordHash, user.PasswordSalt))
                return new LoginResponse(new[] {new Error("login_failure", "Invalid username or password.")});

            // generate refresh token
            var refreshToken = _tokenFactory.GenerateToken();
            user.AddRefreshToken(refreshToken, user.Id, message.RemoteIpAddress);
            await _userRepository.Update(user);

            // generate access token
            return new LoginResponse(await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.Username),
                refreshToken, true);
        }

        public async Task<ExchangeRefreshTokenResponse> ExchangeRefreshTokenAsync(ExchangeRefreshTokenRequest message)
        {
            var cp = _jwtTokenValidator.GetPrincipalFromToken(message.AccessToken, message.SigningKey);

            // invalid token/signing key was passed and we can't extract user claims
            if (cp == null) return new ExchangeRefreshTokenResponse(false, "Invalid token.");

            var id = cp.Claims.First(c => c.Type == "id");
            var user = await _userRepository.GetSingleBySpec(new UserSpecification(id.Value));

            if (!user.HasValidRefreshToken(message.RefreshToken))
                return new ExchangeRefreshTokenResponse(false, "Invalid token.");

            var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.Username);
            var refreshToken = _tokenFactory.GenerateToken();
            user.RemoveRefreshToken(message.RefreshToken); // delete the token we've exchanged
            user.AddRefreshToken(refreshToken, user.Id, ""); // add the new one
            await _userRepository.Update(user);
            return new ExchangeRefreshTokenResponse(jwtToken, refreshToken, true);
        }
    }
}