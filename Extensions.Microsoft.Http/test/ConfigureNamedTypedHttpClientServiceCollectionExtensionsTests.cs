using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Extensions.Microsoft.Http.Tests;

public class ConfigureNamedTypedHttpClientServiceCollectionExtensionsTests
{
    [Fact]
    public void ConfigureHttpClient()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());

        services.ConfigureHttpClient<TestClient, TestClientOptions>("name");

        using var serviceProvider = services.BuildServiceProvider();

        var client = serviceProvider.GetService<TestClient>();

        client.Should().NotBeNull();
    }

    [Fact]
    public void ConfigureHttpClientWithBaseAddress()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["name:BaseAddress"] = "http://localhost"
            })
            .Build());

        services.ConfigureHttpClient<TestClient, TestClientOptions>("name");

        using var serviceProvider = services.BuildServiceProvider();

        var client = serviceProvider.GetRequiredService<TestClient>();

        client.Client.BaseAddress?.Host.Should().Be("localhost");
    }

    [Fact]
    public void ConfigureHttpClientWithTimeout()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["name:Timeout"] = "00:00:30"
            })
            .Build());

        services.ConfigureHttpClient<TestClient, TestClientOptions>("name");

        using var serviceProvider = services.BuildServiceProvider();

        var client = serviceProvider.GetRequiredService<TestClient>();

        client.Client.Timeout.TotalSeconds.Should().Be(30);
    }

    private class TestClient
    {
        public HttpClient Client { get; set; }

        public TestClient(HttpClient client)
            => Client = client;
    }

    private class TestClientOptions : HttpClientOptions
    {
    }
}
