﻿namespace BookStore.Tests.Helpers
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
                    var connection = new SqlConnection(@"Data Source=DESKTOP-D64SJFJ\SQLEXPRESS;user ID=DESKTOP-D64SJFJ\anduser;Initial Catalog=BookStore;User Id = TestAdmin; Password = Test;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

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
                var dummyHost = builder.Build();

                builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

                var host = builder.Build();
                host.Start();

                return dummyHost;
            }
        }
}
