using System.Threading.Tasks;
using WebApi.Jwt.Core.Domain.Entities;
using WebApi.Jwt.Core.Domain.Entities.Responses;
using WebApi.Jwt.Core.Interfaces.Gateways.Repositories;
using WebApi.Jwt.Core.Specifications;
using WebApi.Jwt.Infrastructure.Shared;

namespace WebApi.Jwt.Infrastructure.Data.Repositories
{
    internal sealed class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(
            appDbContext)
        {
        }

        public async Task<CreateUserResponse> Create(string firstName, string lastName, string email, string userName,
            string password)
        {
            var user = new User(firstName, lastName, userName, email);

            PasswordHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            AppDbContext.Users.Add(user);
            await AppDbContext.SaveChangesAsync();

            return new CreateUserResponse(user.Id.ToString(), true);
        }

        public async Task<User> FindByName(string userName)
        {
            return await GetSingleBySpec(new UserSpecification(userName));
        }

        public bool CheckPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            return PasswordHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        }
    }
}