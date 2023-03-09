namespace BookStore.Tests.Helpers
{
    using BookStore.Data;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Data.SqlClient;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using System.Data.Common;

    public class  CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {

            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<BookStoreContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                services.AddSingleton<DbConnection>(container =>
                {
                    //var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;user ID=DESKTOP-D64SJFJ\anduser;Initial Catalog=BookStoreContext-d3b21a9b-f8d3-4177-bae0-7a5f37ff5839;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    var connection = new SqlConnection(@"Data Source=DESKTOP-D64SJFJ\SQLEXPRESS;user ID=DESKTOP-D64SJFJ\anduser;Initial Catalog=BookStore;User Id = TestAdmin; Password = Test;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    //Data Source=DESKTOP-D64SJFJ\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<BookStoreContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlServer(connection);
                });
            });

            builder.UseEnvironment("Development");
            builder.UseUrls("https://localhost:7137");
        }

            protected override IHost CreateHost(IHostBuilder builder)
            {
                // need to create a plain host that we can return.
                var dummyHost = builder.Build();

                // configure and start the actual host.
                builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

                var host = builder.Build();
                host.Start();

                return dummyHost;
            }
        }
}
