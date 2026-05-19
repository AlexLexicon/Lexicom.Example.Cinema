namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

public interface IDateTimeService
{
    Task<string> GetLocalDateTimeStringFromUtcAsync(DateTimeOffset? dateTimeOffsetUtc);
}
public class DateTimeService : IDateTimeService
{
    public Task<string> GetLocalDateTimeStringFromUtcAsync(DateTimeOffset? dateTimeOffsetUtc)
    {
        string localDateTimeString = dateTimeOffsetUtc?.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss tt") ?? "null";

        return Task.FromResult(localDateTimeString);
    }
}
