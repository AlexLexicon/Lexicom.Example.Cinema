using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Client.Core.Extensions;
public static class MediatRServiceConfigurationExtensions
{
    public static void RegisterServicesFromClientApplication(this MediatRServiceConfiguration mediatRServiceConfiguration)
    {
        mediatRServiceConfiguration.RegisterServicesFromAssemblyContaining<AssemblyScanMarker>();
    }
}
