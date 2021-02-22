using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace socialize_api
{
    /// <summary>
    /// The main Program class of our application.
    /// </summary>
    public class Program
    {
        #region Methods
        /// <summary>
        /// The entry point method.
        /// </summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// This methods adds the services and configure the application by calling Startup class.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>The host builder object.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        #endregion Methods
    }
}
