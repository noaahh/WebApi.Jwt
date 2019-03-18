namespace WebApi.Jwt.Core.Domain.Entities.Requests
{
    public class LoginRequest
    {
        public LoginRequest(string userName, string password, string remoteIpAddress)
        {
            UserName = userName;
            Password = password;
            RemoteIpAddress = remoteIpAddress;
        }

        public string UserName { get; }

        public string Password { get; }

        public string RemoteIpAddress { get; }
    }
}