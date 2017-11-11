using System;

namespace YQH.AppStoreRank.Common
{
    public class Timestamp
    {
        public static long GetTimestamp(DateTime time)
        {
            TimeSpan ts = time - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        public static DateTime GetDateTime(long timestamp)
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time = time.AddSeconds(timestamp);
            return time;
        }

        public static long GetUTCTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}
