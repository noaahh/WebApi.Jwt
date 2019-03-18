using System.Collections.Generic;

namespace WebApi.Jwt.Core.Domain.Entities.Responses
{
    public abstract class BaseGatewayResponse
    {
        protected BaseGatewayResponse(bool success = false, IEnumerable<Error> errors = null)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; }
        public IEnumerable<Error> Errors { get; }
    }
}