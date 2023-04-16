using Lexicom.ConsoleApp.DependencyInjection;
using Microsoft.Extensions.Configuration;

/*
 * Lexicom.Example.Cinema.Server.Movies.ConsoleApp
 */

ConsoleApplicationBuilder builder = ConsoleApplication.CreateBuilder();

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("appsettings.Example.json");
