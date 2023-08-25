using Lexicom.Concentrate.Supports.Wpf.Extensions;
using Lexicom.Concentrate.Wpf.Amenities.Extensions;
using Lexicom.Concentrate.Wpf.Themes.Extensions;
using Lexicom.Configuration.Settings.For.Wpf.Extensions;
using Lexicom.Example.Cinema.Client.Application.Extensions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Extensions;
using Lexicom.Example.Cinema.Client.Wpf.Views;
using Lexicom.Mvvm.Amenities.Extensions;
using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.For.Wpf.Extensions;
using Lexicom.Supports.Wpf.Extensions;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.Wpf.Extensions;
using Lexicom.Wpf.Amenities.Extensions;
using Lexicom.Wpf.DependencyInjection;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Client.Wpf;
public partial class App : System.Windows.Application
{
    public App()
    {
        WpfApplicationBuilder builder = WpfApplication.CreateBuilder(this);

        builder.Configuration.AddJsonFile("appsettings.json");

        builder.Lexicom(options =>
        {
            options.AddSettings(Wpf.Properties.Settings.Default);
            options.AddAmenities();
            options.AddMvvm(options =>
            {
                options.AddViewModel<MainWindowViewModel>(options =>
                {
                    options.ForWindow<MainWindowView>();
                });

                options.AddViewModel<NavigationDomainsViewModel>();
                options.AddViewModel<NavigationDomainViewModel>(ServiceLifetime.Transient);
                options.AddViewModel<NavigationPageActorViewModel>(ServiceLifetime.Transient);
                options.AddViewModel<NavigationPageDirectorViewModel>(ServiceLifetime.Transient);
                options.AddViewModel<NavigationPageMovieViewModel>(ServiceLifetime.Transient);
                options.AddViewModel<NavigationPageSearchViewModel>(ServiceLifetime.Transient);
                options.AddViewModel<NavigationPagesViewModel<NavigationPageMovieViewModel>>();
                options.AddViewModel<NavigationPagesViewModel<NavigationPageDirectorViewModel>>();
                options.AddViewModel<NavigationPagesViewModel<NavigationPageActorViewModel>>();
                options.AddViewModel<NavigationUserViewModel>();
                options.AddViewModel<NavigationViewModel>();

                options.AddViewModel<PageMovieFormViewModel>();
                options.AddViewModel<PageMovieViewModel>();

                options.AddViewModel<PopupViewModel>();

                options.AddViewModel<PreferencesViewModel>();

                options.AddViewModel<SearchMoviePaginationViewModel>();
                options.AddViewModel<SearchMovieResultViewModel>(ServiceLifetime.Transient);
                options.AddViewModel<SearchMovieViewModel>();

                options.AddViewModel<SignInViewModel>();

                //add mediatR must come after adding all view models
                options.AddMediatR(options =>
                {
                    options.NotificationPublisherType = typeof(TaskWhenAllPublisher);

                    options.RegisterServicesFromClientApplication();
                    options.RegisterServicesFromViewModels();
                });
            });
            options.AddValidation(options =>
            {
                options.AddValidators<AssemblyScanMarker>();
                options.AddClientApplication();
            });
            options.Concentrate(options =>
            {
                options.AddAmenities();
                options.AddTheming();
                options.AddClientAuthentication();
            });
        });

        builder.Services.AddClientApplication();

        WpfApplication app = builder.Build();

        app.StartupWindow<MainWindowView>();
    }
}