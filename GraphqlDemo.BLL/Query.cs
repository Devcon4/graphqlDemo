using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GraphqlDemo.BLL.Feature.Message;
using GraphqlDemo.BLL.Feature.User;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphqlDemo.BLL
{
    public class Query
    {
    }

    public static class IObjectTypeDescriptorExtensions
    {
        public static void IncludeQueriesByInterface<T>(this IObjectTypeDescriptor c, params Assembly[] assemblies)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                var methodInfo = typeof(IObjectTypeDescriptor).GetMethod("Include");
                methodInfo.MakeGenericMethod(type.AsType()).Invoke(c, new object[] { });

            }
        }

        public static void IncludeTypeByInterface<T, U>(this IObjectTypeDescriptor c, params Assembly[] assemblies)
        {
            var typesFromAssemblies =
                assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));

            var fieldInfo = typeof(IObjectTypeDescriptor).GetMethods().FirstOrDefault(m => m.IsGenericMethod && m.Name == "Field");

            foreach (var type in typesFromAssemblies)
            {
                var mutMethods = type.DeclaredMethods.Where(m => m.GetParameters().Any(p => p.GetType() == typeof(U))).ToList();

                if (mutMethods.Any())
                {
                    foreach (var method in mutMethods)
                    {
                        fieldInfo.MakeGenericMethod(type.AsType()).Invoke(c, new object[] { method.Invoke(c, new object[] {default})});
                    }
                }
            }
        }
    }

    public class QueryConfig : ObjectType<Query> {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor) {

            descriptor.IncludeTypeByInterface<IResolvable, ObjectType>(typeof(IResolvable).Assembly);
        }
    }
}
