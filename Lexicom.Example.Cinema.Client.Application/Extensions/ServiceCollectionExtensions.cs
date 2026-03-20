using Lexicom.Authentication.Http;
using Lexicom.Authentication.Http.Extensions;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Example.Cinema.Client.Application.Options;
using Lexicom.Example.Cinema.Client.Application.Services;
using Lexicom.Example.Cinema.Client.Application.Temp;
using Lexicom.Example.Cinema.Client.Application.Validators;
using Lexicom.Validation.Options.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lexicom.Example.Cinema.Client.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddClientApplication(this IServiceCollection services)
    {
        services.AddSingleton<IDomainsStore, DomainsStore>();

        services.AddAndConfigureHttpClientAnonymous<HttpClientAuthorityAnonymousApiOptions>((sp, c) =>
        {
            var options = sp.GetRequiredService<IOptions<HttpClientAuthorityAnonymousApiOptions>>();

            string? baseAddress = options.Value.BaseAddress;

            if (baseAddress is null)
            {
                throw HttpClientAuthorityAnonymousApiOptionsValidator.ToUnreachableException();
            }

            c.BaseAddress = new Uri(baseAddress);
        });

        services.AddAndConfigureHttpClient<HttpClientAuthorityApiOptions>((sp, c) =>
        {
            var options = sp.GetRequiredService<IOptions<HttpClientAuthorityApiOptions>>();

            string? baseAddress = options.Value.BaseAddress;

            if (baseAddress is null)
            {
                throw HttpClientAuthorityApiOptionsValidator.ToUnreachableException();
            }

            c.BaseAddress = new Uri(baseAddress);
        });

        services.AddAndConfigureHttpClientAnonymous<HttpClientPersonsAnonymousApiOptions>((sp, c) =>
        {
            var options = sp.GetRequiredService<IOptions<HttpClientPersonsAnonymousApiOptions>>();

            string? baseAddress = options.Value.BaseAddress;

            if (baseAddress is null)
            {
                throw HttpClientPersonsAnonymousApiOptionsValidator.ToUnreachableException();
            }

            c.BaseAddress = new Uri(baseAddress);
        });

        services.AddAndConfigureHttpClient<HttpClientPersonsApiOptions>((sp, c) =>
        {
            var options = sp.GetRequiredService<IOptions<HttpClientPersonsApiOptions>>();

            string? baseAddress = options.Value.BaseAddress;

            if (baseAddress is null)
            {
                throw HttpClientPersonsApiOptionsValidator.ToUnreachableException();
            }

            c.BaseAddress = new Uri(baseAddress);
        });

        services.AddAndConfigureHttpClientAnonymous<HttpClientMoviesAnonymousApiOptions>((sp, c) =>
        {
            var options = sp.GetRequiredService<IOptions<HttpClientMoviesAnonymousApiOptions>>();

            string? baseAddress = options.Value.BaseAddress;

            if (baseAddress is null)
            {
                throw HttpClientMoviesAnonymousApiOptionsValidator.ToUnreachableException();
            }

            c.BaseAddress = new Uri(baseAddress);
        });

        services.AddAndConfigureHttpClient<HttpClientMoviesApiOptions>((sp, c) =>
        {
            var options = sp.GetRequiredService<IOptions<HttpClientMoviesApiOptions>>();

            string? baseAddress = options.Value.BaseAddress;

            if (baseAddress is null)
            {
                throw HttpClientMoviesApiOptionsValidator.ToUnreachableException();
            }

            c.BaseAddress = new Uri(baseAddress);
        });

        services.AddSingleton<IHttpClientAccessTokenRefresher, HttpClientAccessTokenRefresher>();
        services.AddSingleton<IHttpClientUnathorizedListener, HttpClientUnathorizedListener>();
    }

    public static IHttpClientBuilder AddAndConfigureHttpClientAnonymous<THttpClientOptions>(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient) where THttpClientOptions : class
    {
        services
            .AddOptions<THttpClientOptions>()
            .BindConfiguration()
            .Validate()
            .ValidateOnStart();

        return services.AddHttpClient(nameof(THttpClientOptions), configureClient);
    }

    public static IHttpClientBuilder AddAndConfigureHttpClient<THttpClientOptions>(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient) where THttpClientOptions : class
    {
        return AddAndConfigureHttpClientAnonymous<THttpClientOptions>(services, configureClient)
            .AddHttpAuthenticationHandlers(options =>
            {
                options.AuthorizeWithAccessToken();
                options.AutomaticallyRefreshAccessToken();
                options.ForwardUnathorizedRequests();
            });
    }
}
