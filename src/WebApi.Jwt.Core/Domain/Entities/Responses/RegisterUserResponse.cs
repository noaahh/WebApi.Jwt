using System.Collections.Generic;
using WebApi.Jwt.Core.Interfaces;

namespace WebApi.Jwt.Core.Domain.Entities.Responses
{
    public class RegisterUserResponse : ResponseMessage
    {
        public RegisterUserResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(
            success, message)
        {
            Errors = errors;
        }

        public RegisterUserResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }

        public string Id { get; }

        public IEnumerable<string> Errors { get; }
    }
}