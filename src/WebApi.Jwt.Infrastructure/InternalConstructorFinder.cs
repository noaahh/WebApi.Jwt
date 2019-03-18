using System;
using System.Linq;
using System.Reflection;
using Autofac.Core.Activators.Reflection;

namespace WebApi.Jwt.Infrastructure
{
    public class InternalConstructorFinder : IConstructorFinder
    {
        public ConstructorInfo[] FindConstructors(Type t)
        {
            return t.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsPrivate && !c.IsPublic).ToArray();
        }
    }
}