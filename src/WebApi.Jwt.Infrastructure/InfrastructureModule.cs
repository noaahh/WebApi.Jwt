using Autofac;
using WebApi.Jwt.Core.Interfaces.Gateways.Repositories;
using WebApi.Jwt.Core.Interfaces.Services;
using WebApi.Jwt.Infrastructure.Auth;
using WebApi.Jwt.Infrastructure.Data.Repositories;
using WebApi.Jwt.Infrastructure.Interfaces;

namespace WebApi.Jwt.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance()
                .FindConstructorsWith(new InternalConstructorFinder());
            builder.RegisterType<JwtTokenHandler>().As<IJwtTokenHandler>().SingleInstance()
                .FindConstructorsWith(new InternalConstructorFinder());
            builder.RegisterType<TokenFactory>().As<ITokenFactory>().SingleInstance();
            builder.RegisterType<JwtTokenValidator>().As<IJwtTokenValidator>().SingleInstance()
                .FindConstructorsWith(new InternalConstructorFinder());
        }
    }
}