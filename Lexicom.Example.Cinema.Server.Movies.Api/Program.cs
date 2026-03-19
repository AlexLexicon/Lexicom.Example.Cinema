using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
using Lexicom.Authorization.AspNetCore.Controllers.Extensions;
using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.AspNetCore.Controllers.Extensions;
using Lexicom.Example.Cinema.Server.Movies.Api;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Extensions;
using Lexicom.Example.Cinema.Server.Movies.Application.Database;
using Lexicom.Example.Cinema.Server.Movies.Application.Extensions;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Logging.AspNetCore.Controllers.Extensions;
using Lexicom.Scalar.Extensions;
using Lexicom.Smtp.AspNetCore.Controllers.Extensions;
using Lexicom.Supports.AspNetCore.Controllers.Extensions;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.AspNetCore.Controllers.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

/*
 * Lexicom.Example.Cinema.Server.Movies.Api
 */

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.SecretsExample.json");

builder.Services.AddControllers();

builder.Lexicom(options =>
{
    options.AddAmenities(options =>
    {
        options.AddErrorResponseActionFilter();
        options.AddExceptionHandlingMiddleware();
#if DEBUG
        options.DebugExceptionHandlingMiddleware(e =>
        {

        });
#endif
        //options.AddInvalidModelStateFactory();
    });
    options.AddAuthentication(options =>
    {
        options.AddAccessTokenAuthentication();
    });
    options.AddAuthorization(options =>
    {
        options.AddPermissions(Policies.Permissions.All);
    });
    options.AddScalar();
    options.AddValidation(options =>
    {
        options.AddAmenities();
        options.AddRequestBodyActionFilter();
        options.AddValidators<AssemblyScanMarker>();
        options.AddMoviesApiRuleSets();
    });
    options.AddLogging();
    options.AddPrimitives(options =>
    {
        options.AddTimeProvider();
        options.AddGuidProvider();
    });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = TestInvalidModelStateResponse.Factory;
});

builder.Services.AddDbContextFactory<MoviesDbContext>(options =>
{
    string? sqliteConnectionString = builder.Configuration.GetConnectionString("moviesdb-sqlite");
    string? sqlConnectionString = builder.Configuration.GetConnectionString("moviesdb-sql");

    options.UseSqlite(sqliteConnectionString);
    //options.UseSqlServer(sqlConnectionString);
});

builder.Services.AddMoviesApplication();

var app = builder.Build();

app.UseLexicomExceptionHandlingMiddleware();
app.UseLexicomLogging();
app.UseLexicomScalar();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public static class TestInvalidModelStateResponse
{
    private const string MODELSTATE_JSON_KEY_PROPERTY = "$.";
    private const string MODELSTATE_INCLUDING = "including the following:";
    private const string MODELSTATE_REQUESTBODY = "requestBody";
    private const string MODELSTATE_REQUIRED_FIELD_END = "field is required.";
    private const string MODELSTATE_NOTVALID_FIELD_END = "is not valid.";
    private const string MODELSTATE_JSONNOTCONVERTED_FIELD_START = "The JSON value could not be converted";
    private const string REQUIRED_MESSAGE = $"The {MODELSTATE_REQUIRED_FIELD_END}";
    private const string NOTVALID_MESSAGE = $"The field {MODELSTATE_NOTVALID_FIELD_END}";
    private const string JSON_MESSAGE = $"The json is malformed or invalid.";

    /// <exception cref="ArgumentNullException"/>
    public static IActionResult Factory(ActionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        //standardize the regular model state validation
        //to use the ErrorResponse object
        //and standardizes the messaging format
        Dictionary<string, ModelErrorCollection> modelStateErrors = context.ModelState.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value!.Errors);

        bool isJsonError = false;
        var errors = new Dictionary<string, IEnumerable<string>>();
        foreach (var modelStateError in modelStateErrors)
        {
            foreach (ModelError error in modelStateError.Value)
            {
                string key = modelStateError.Key;
                string message = error.ErrorMessage;

                if (key is "$")
                {
                    if (message.Contains("Path:") &&
                        message.Contains("LineNumber:") &&
                        message.Contains("BytePositionInLine:"))
                    {
                        isJsonError = true;
                    }
                    else
                    {
                        int includingStartIndex = message.IndexOf(MODELSTATE_INCLUDING);
                        if (includingStartIndex is >= 0 && error.ErrorMessage.Contains("missing required"))
                        {
                            string fieldsString = message[(includingStartIndex + MODELSTATE_INCLUDING.Length)..];
                            string[] fields = fieldsString.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                            if (fields.Length is not 0)
                            {
                                foreach (string field in fields)
                                {
                                    AddMessage(errors, field, REQUIRED_MESSAGE);
                                }
                            }
                        }
                    }
                }
                if (key.StartsWith(MODELSTATE_JSON_KEY_PROPERTY))
                {
                    string cleanKey = key.Replace(MODELSTATE_JSON_KEY_PROPERTY, "");

                    if (message.StartsWith(MODELSTATE_JSONNOTCONVERTED_FIELD_START))
                    {
                        AddMessage(errors, cleanKey, JSON_MESSAGE);
                    }
                    else
                    {
                        AddMessage(errors, cleanKey, "The field is not supported.");
                    }
                }
                else if (key is MODELSTATE_REQUESTBODY)
                {
                    if (isJsonError)
                    {
                        AddMessage(errors, key, JSON_MESSAGE);
                    }
                }
                else
                {
                    if (message.EndsWith(MODELSTATE_REQUIRED_FIELD_END))
                    {
                        AddMessage(errors, key, REQUIRED_MESSAGE);
                    }
                    else if (message.EndsWith(MODELSTATE_NOTVALID_FIELD_END))
                    {
                        AddMessage(errors, key, NOTVALID_MESSAGE);
                    }
                }
            }
        }

        if (errors.Count is 0)
        {
            AddMessage(errors, MODELSTATE_REQUESTBODY, "The json body was invalid.");
        }

        return new BadRequestObjectResult(errors);
    }

    private static void AddMessage(Dictionary<string, IEnumerable<string>> errors, string key, string message)
    {
        if (errors.TryGetValue(key, out IEnumerable<string>? value))
        {
            var messages = (List<string>)value;

            if (!messages.Contains(message))
            {
                messages.Add(message);
            }
        }
        else
        {
            errors.Add(key,
            [
                message,
            ]);
        }
    }
}
