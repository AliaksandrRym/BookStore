namespace BookStore.Tests.Helpers
{
    using Microsoft.AspNetCore.Mvc.Testing;
    public class CustomWebAplicationFactory : WebApplicationFactory<Program>
    {

            public CustomWebAplicationFactory()
            {
            }

            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
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
