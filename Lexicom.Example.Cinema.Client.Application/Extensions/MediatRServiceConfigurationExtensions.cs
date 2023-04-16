using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Client.Application.Extensions;
public static class MediatRServiceConfigurationExtensions
{
    public static void RegisterServicesFromClientApplication(this MediatRServiceConfiguration mediatRServiceConfiguration)
    {
        mediatRServiceConfiguration.RegisterServicesFromAssemblyContaining<AssemblyScanMarker>();
    }
}
