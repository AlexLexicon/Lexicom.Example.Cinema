namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Extensions;
public static class TimeSpanExtensions
{
    public static string ToDurationString(this TimeSpan duration)
    {
        int hours = duration.Hours;
        int minutes = duration.Minutes;

        string durationString = $"{hours} ";
        if (hours > 0)
        {
            if (hours == 1)
            {
                durationString += $"hour ";
            }
            else
            {
                durationString += $"hours ";
            }
        }
        else
        {
            durationString = "";
        }

        durationString += $"{minutes} ";

        if (minutes > 0)
        {
            if (minutes == 1)
            {
                durationString += $"minute";
            }
            else
            {
                durationString += $"minutes";
            }
        }

        return durationString;
    }
}
