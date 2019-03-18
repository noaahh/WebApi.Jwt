using System;
using System.Linq.Expressions;
using WebApi.Jwt.Core.Domain.Entities;
using WebApi.Jwt.Core.Shared;

namespace WebApi.Jwt.Core.Specifications
{
    public sealed class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(Expression<Func<User, bool>> criteria) : base(criteria)
        {
            AddInclude(u => u.RefreshTokens);
        }

        public UserSpecification(string username) : base(u => u.Username == username)
        {
            AddInclude(u => u.RefreshTokens);
        }
    }
}