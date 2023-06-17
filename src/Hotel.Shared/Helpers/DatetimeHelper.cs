namespace Hotel.Shared.Helpers;

public static class DatetimeHelper
{
    public static DateTime ToVietnameseDatetime(this DateTime date)
    {
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        DateTime currentTimeInVietnam = TimeZoneInfo.ConvertTime(date, vietnamTimeZone);
        return currentTimeInVietnam;
    }
}
