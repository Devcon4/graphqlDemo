using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using GraphqlDemo.BLL.Feature.MessageGraph;
using GraphqlDemo.BLL.Feature.UserGraph;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphqlDemo.BLL
{
    public class Query
    {
    }
    public enum IncludeType
    {
        Query,
        Mutation
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

        public static Expression<Func<U, object>> exp<U> (MethodInfo method, List<object> pars) where U: IResolvable {
            return (m) => method.Invoke(m, pars.ToArray());
        }

        public static void IncludeTypeByInterface<T>(this IObjectTypeDescriptor c, IncludeType includeType, params Assembly[] assemblies)
        {
            var typesFromAssemblies =
                assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));

            var fieldInfo = typeof(IObjectTypeDescriptor).GetMethods().FirstOrDefault(m => m.IsGenericMethod && m.Name == "Field");

            foreach (var type in typesFromAssemblies)
            {
                var methods = new List<MethodInfo> {};
                
                if(includeType == IncludeType.Mutation) {
                    methods.AddRange(type.DeclaredMethods.Where(m => m.GetParameters().Any(p => p.GetType() == typeof(InputObjectType))));
                } else if (includeType == IncludeType.Query) {
                    methods.AddRange(type.DeclaredMethods.Where(m => m.GetParameters().All(p => p.GetType() != typeof(InputObjectType))));
                }

                if (methods.Any())
                {
                    foreach (MethodInfo method in methods)
                    {
                        var pars = new List<object>() {};
                        foreach (var p in method.GetParameters())
                        {
                            pars.Add(default);
                        }

                        var expInfo = typeof(IObjectTypeDescriptorExtensions).GetMethods().FirstOrDefault(m => m.IsGenericMethod && m.Name == "exp");

                        fieldInfo.MakeGenericMethod(type.AsType()).Invoke(c, new object[] { expInfo.MakeGenericMethod(type.AsType()).Invoke(null, new object[]{ method, pars }) });
                    }
                }


            }
        }
    }

    public class QueryConfig : ObjectType<Query> {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor) {

            descriptor.IncludeTypeByInterface<IResolvable>(IncludeType.Query, typeof(IResolvable).Assembly);
        }
    }
}
