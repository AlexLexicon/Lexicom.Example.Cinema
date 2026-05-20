using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Extensions;

public static class FileName
{
    public static void AddClientWpfViewModels(this IMvvmServiceBuilder builder)
    {
        builder.Services
            .AssemblyScan<AssemblyScanMarker>()
            .For<ObservableObject>()
            .Register(t =>
            {
                builder.AddViewModel(t);
            });

        builder.Services
            .AssemblyScan<AssemblyScanMarker>()
            .For<DisposableObservableObject>()
            .Register(t =>
            {
                builder.AddViewModel(t);
            });
    }
}
