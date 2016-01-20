using System;

namespace Roombait.App
{
    public static class Util
    {
        public static string GetSlug(Models.Residence residence)
        {
            return residence.Name.ToLower().Replace(' ', '-');
        }

        public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            return StartOfWeek(dt, startOfWeek).AddDays(6);
        }
    }
}
