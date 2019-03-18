using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Jwt.Core.Domain.Entities;
using WebApi.Jwt.Core.Domain.Entities.Requests;
using WebApi.Jwt.Core.Domain.Entities.Responses;

namespace WebApi.Jwt.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest message);
        Task<List<User>> GetUsers();
    }
}