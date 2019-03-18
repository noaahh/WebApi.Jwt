using System.Threading.Tasks;
using WebApi.Jwt.Core.Domain.Entities;
using WebApi.Jwt.Core.Domain.Entities.Responses;

namespace WebApi.Jwt.Core.Interfaces.Gateways.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<CreateUserResponse> Create(string firstName, string lastName, string email, string userName,
            string password);

        Task<User> FindByName(string userName);

        bool CheckPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}