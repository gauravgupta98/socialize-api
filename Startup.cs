using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using socialize_api.Data;
using socialize_api.GraphQL;
using socialize_api.GraphQL.Posts;
using socialize_api.GraphQL.Users;

namespace socialize_api
{
    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        #region Members
        private readonly IConfiguration Configuration;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// This method configures the services used in the application.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // AddPooledDbContextFactory is added for multithreading which can run multiple queries in parallel.
            // Earlier this was AddDbContext where it used to fail multiple queries.
            services
                .AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString")));

            // AddProjections is used to get any child data in a query. Like inside a User if we want to get all the Posts he posted.
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<UserType>()
                .AddType<PostType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();
        }

        /// <summary>
        /// This methods configures the routing and other essentials of the application.
        /// </summary>
        /// <param name="app">The application object</param>
        /// <param name="env">The web host environment object.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            // This /graphql-voyager endpoint will create documentation for our GraphQL API.
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });
        }
        #endregion Methods
    }
}
