using Autofac;
using WebApi.Jwt.Core.Domain.Services;
using WebApi.Jwt.Core.Interfaces.Services;

namespace WebApi.Jwt.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
        }
    }
}