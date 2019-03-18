using System.Threading.Tasks;
using WebApi.Jwt.Core.Domain.Entities;

namespace WebApi.Jwt.Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName);
    }
}