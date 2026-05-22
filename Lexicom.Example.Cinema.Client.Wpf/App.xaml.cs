using Lexicom.Concentrate.Client.Authentication.For.Wpf.Extensions;
using Lexicom.Concentrate.Supports.Wpf.Extensions;
using Lexicom.Concentrate.Wpf.Amenities.Extensions;
using Lexicom.Concentrate.Wpf.Themes.Extensions;
using Lexicom.Configuration.Settings.For.Wpf.Extensions;
using Lexicom.Example.Cinema.Client.Application.Extensions;
using Lexicom.Example.Cinema.Client.Application.Temp;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Extensions;
using Lexicom.Example.Cinema.Client.Wpf.Views;
using Lexicom.Example.Cinema.Shared.Extensions;
using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.For.Wpf.Extensions;
using Lexicom.Supports.Wpf.Extensions;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.Wpf.Extensions;
using Lexicom.Wpf.Amenities.Extensions;
using Lexicom.Wpf.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicom.Example.Cinema.Client.Wpf;

public partial class App : System.Windows.Application
{
    public App()
    {
        WpfApplicationBuilder builder = WpfApplication.CreateBuilder(this);

        builder.Configuration.AddJsonFile("appsettings.json");

        builder.Lexicom(l =>
        {
            l.Concentrate(lc =>
            {
                lc.AddAmenities();
                lc.AddTheming();
                lc.AddClientAuthentication();
            });

            l.AddSettings(Wpf.Properties.Settings.Default);
            l.AddAmenities();
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<MainWindowViewModel>(vm =>
                {
                    vm.ForWindow<MainWindowView>();
                });

                mvvm.AddViewModel<NavigationDomainsViewModel>();
                mvvm.AddViewModel<NavigationDomainViewModel>(ServiceLifetime.Transient);
                mvvm.AddViewModel<NavigationPageActorViewModel>(ServiceLifetime.Transient);
                mvvm.AddViewModel<NavigationPageDirectorViewModel>(ServiceLifetime.Transient);
                mvvm.AddViewModel<NavigationPageMovieViewModel>(ServiceLifetime.Transient);
                mvvm.AddViewModel<NavigationPageSearchViewModel>(ServiceLifetime.Transient);
                mvvm.AddViewModel<NavigationPagesViewModel<NavigationPageMovieViewModel>>();
                mvvm.AddViewModel<NavigationPagesViewModel<NavigationPageDirectorViewModel>>();
                mvvm.AddViewModel<NavigationPagesViewModel<NavigationPageActorViewModel>>();
                mvvm.AddViewModel<NavigationUserViewModel>();
                mvvm.AddViewModel<NavigationViewModel>();

                mvvm.AddViewModel<PageMovieFormViewModel>();
                mvvm.AddViewModel<PageMovieViewModel>();

                mvvm.AddViewModel<PopupViewModel>();

                mvvm.AddViewModel<PreferencesDialogViewModel>();

                mvvm.AddViewModel<SearchMoviePaginationViewModel>();
                mvvm.AddViewModel<SearchMovieResultViewModel>(ServiceLifetime.Transient);
                mvvm.AddViewModel<SearchMovieViewModel>();

                mvvm.AddViewModel<SignInViewModel>();
            });
            l.AddValidation(v =>
            {
                v.AddValidators<AssemblyScanMarker>();
                v.AddClientApplication();
            });
        });

        builder.Services.AddShared();
        builder.Services.AddClientApplication();

        WpfApp = builder.Build();

        WpfApp.Services.GetRequiredService
    }

    private WpfApplication? WpfApp { get; }

    protected override async void OnStartup(StartupEventArgs e)
    {
        if (WpfApp is not null)
        {
            var domainStore = WpfApp.Services.GetRequiredService<IDomainsStore>();

            await domainStore.LoadAsync();

            WpfApp.StartupWindow<MainWindowView>();
        }

        base.OnStartup(e);
    }
}