using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using socialize_api.Data;
using socialize_api.GraphQL;
using socialize_api.GraphQL.Posts;
using socialize_api.GraphQL.Users;
using socialize_api.Services;
using System.Text;

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

            // Add Scoped Service so that we can use it in Configure method.
            services.AddScoped<AppDbContext>();

            services.AddCors();

            // Setup authentication service.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "audience",
                    ValidIssuer = "issuer",
                    RequireSignedTokens = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("$3cr3+SecretKEY3333"))
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });

            services.AddErrorFilter<ErrorFilter>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

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
                .AddInMemorySubscriptions()
                .AddAuthorization();
        }

        /// <summary>
        /// This methods configures the routing and other essentials of the application.
        /// </summary>
        /// <param name="app">The application object</param>
        /// <param name="env">The web host environment object.</param>
        /// <param name="context">The db context.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Run automatic migrations.
            context.Database.Migrate();

            // Add cors.
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

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
