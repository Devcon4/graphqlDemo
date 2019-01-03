using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GraphqlDemo.BLL;
using GraphqlDemo.BLL.Feature.MessageGraph;
using GraphqlDemo.BLL.Feature.UserGraph;
using GraphqlDemo.DAL.Entities;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.AspNetCore.GraphiQL;
using HotChocolate.Configuration;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Hosting;

namespace GraphqlDemo.API
{


    public static class ServiceCollectionExtensions
    {
        public static void RegisterTypeByInterface<T>(this ISchemaConfiguration c, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                var registerTypeInfo = typeof(ICodeFirstConfiguration).GetMethods()
                    .First(m => m.IsGenericMethod && m.Name == "RegisterType");
                registerTypeInfo.MakeGenericMethod(type.AsType()).Invoke(c, new object[]{});
            }
        }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Setup GraphQL.
            services.AddGraphQL(sp => Schema.Create(c =>
            {
                var assemblies = typeof(Startup).Assembly.GetReferencedAssemblies().Select(n => Assembly.Load(n)).Append(typeof(Startup).Assembly);

                c.RegisterServiceProvider(sp);
                 
                c.RegisterTypeByInterface<IModelBase>(assemblies.ToArray());

                c.RegisterQueryType<QueryConfig>();
                c.RegisterMutationType<MutationConfig>();

            }));

            // Add Services here.
            services.AddScoped<IMessageQueries, MessageQueries>();
            services.AddScoped<IUserQueries, UserQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseMvc();
            app.UseWebSockets();
            app.UseGraphQL("/graphql");
            app.UseGraphiQL("/graphql", "/graphiql");
            app.UsePlayground("/graphql", "/playground");
        }
    }
}
