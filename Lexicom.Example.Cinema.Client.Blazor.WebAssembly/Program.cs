using Lexicom.DependencyInjection.Amenities;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.Blazor.WebAssembly.Extensions;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly.Options;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly.ViewModels;
using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.For.Blazor.WebAssembly.Extensions;
using Lexicom.Supports.Blazor.WebAssembly.Extensions;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.For.Blazor.WebAssembly.Extensions;
using Lexicom.Validation.Options.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddInMemoryCollection(new ConfigurationInMemorySource
{
    new MyOptions
    {
        CounterTitle = "Test from config"
        //CounterTitle = null
    }
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddOptions<MyOptions>()
    .BindConfiguration();
    //.Validate()
    //.ValidateOnStart();

builder.Lexicom(options =>
{
    options.AddMvvm(options =>
    {
        options.AddViewModel<CounterViewModel>();
    });
    options.AddValidation(options =>
    {
        options.AddAmenities();
        //options.AddValidators<AssemblyScanMarker>();
    });
    options.AddPrimitives(options =>
    {
        options.AddTimeProvider();
        options.AddGuidProvider();
    });
});

var app = builder.Build();

await app.RunAsync();
