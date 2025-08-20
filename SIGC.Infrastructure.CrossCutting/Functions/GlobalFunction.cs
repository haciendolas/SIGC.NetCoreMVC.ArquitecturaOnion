namespace SIGC.Infrastructure.CrossCutting.Functions
{
    public static class GlobalFunction
    {
        public static DateTime ChangeToUtcDate(string timeZoneCode, DateTime currentDate)
        {
            var currentTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneCode);
            var utcDate = TimeZoneInfo.ConvertTime(DateTime.SpecifyKind(currentDate, DateTimeKind.Unspecified), currentTimeZone);

            return utcDate;
        }
    }
}