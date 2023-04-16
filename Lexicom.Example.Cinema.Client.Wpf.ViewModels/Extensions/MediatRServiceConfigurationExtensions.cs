using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Extensions;
public static class MediatRServiceConfigurationExtensions
{
    public static void RegisterServicesFromViewModels(this MediatRServiceConfiguration mediatRServiceConfiguration)
    {
        mediatRServiceConfiguration.RegisterServicesFromAssemblyContaining<AssemblyScanMarker>();
    }
}
