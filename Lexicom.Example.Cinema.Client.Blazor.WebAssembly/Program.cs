using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.Blazor.WebAssembly.Extensions;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly.ViewModels;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly.ViewModels.Extensions;
using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.For.Blazor.WebAssembly.Extensions;
using Lexicom.Supports.Blazor.WebAssembly.Extensions;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.Blazor.WebAssembly.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Lexicom(options =>
{
    options.AddMvvm(options =>
    {
        options.AddViewModel<CounterViewModel>();
    });
    options.AddValidation(options =>
    {
        options.AddAmenities();
        options.AddValidators<AssemblyScanMarker>();
        options.AddViewModelRuleSets();
    });
    options.AddPrimitives(options =>
    {
        options.AddTimeProvider();
        options.AddGuidProvider();
    });
});

var app = builder.Build();

await app.RunAsync();
