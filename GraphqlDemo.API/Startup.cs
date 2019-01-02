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
                c.RegisterType(type.AsType());
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
            services.AddGraphQL(sp => Schema.Create(c =>
            {
                c.RegisterServiceProvider(sp);
                var assemblies = typeof(IGraphQLBase).Assembly.GetReferencedAssemblies().Select(n => Assembly.Load(n)).Append(typeof(IGraphQLBase).Assembly).ToArray();

                // c.RegisterTypeByInterface<IGraphQLBase>(assemblies);

                // c.RegisterType<UserModel>();

                // c.RegisterType<MessageModel>();

                c.RegisterType<QueryConfig>();
                c.RegisterType<MutationConfig>();

                //c.RegisterTypeByInterface<IQueriable>(assemblies);
                //c.RegisterTypeByInterface<IGraphqlable>(assemblies);
            }));
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
            app.UseGraphQL("/graphql");
            app.UseGraphiQL("/graphql");
            app.UsePlayground("/graphql");
        }
    }
}
