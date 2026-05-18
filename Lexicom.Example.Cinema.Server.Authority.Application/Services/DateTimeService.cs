namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;

public interface IDateTimeService
{
    Task<string> GetLocalDateTimeStringFromUtcAsync(DateTimeOffset? dateTimeOffsetUtc);
}
public class DateTimeService : IDateTimeService
{
    public Task<string> GetLocalDateTimeStringFromUtcAsync(DateTimeOffset? dateTimeOffsetUtc)
    {
        string localDateTimeString = dateTimeOffsetUtc?.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss tt") ?? "null";

        return Task.FromResult(localDateTimeString);
    }
}
