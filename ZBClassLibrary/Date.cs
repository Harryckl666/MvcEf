using System;
using System.Collections.Generic;

using System.Web;

namespace ZbClassLibrary
{
    public class Date
    {
        /// <summary>
        /// 获取当前timestamp 时间
        /// </summary>
        public static long GetCuurentUTC()
        {
             return DateTimeToUTC(System.DateTime.Now);
        }

        /// <summary>
        /// UTC时间转WINDOWS时间
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static System.DateTime UTCToDateTime(double l)
        {
            System.DateTime dtZone = new System.DateTime(1970, 1, 1, 0, 0, 0);
            
            dtZone = dtZone.AddSeconds(l);

            return dtZone.ToLocalTime();

        }

        /// <summary>
        /// WINDOWS时间转UTC时间
        /// </summary>
        /// <param name="vDate"></param>
        /// <returns></returns>
        public static long DateTimeToUTC(System.DateTime vDate)
        {
            vDate = vDate.ToUniversalTime();

            System.DateTime dtZone = new System.DateTime(1970, 1, 1, 0, 0, 0);

            return (long)vDate.Subtract(dtZone).TotalSeconds;

        }

        /// <summary>
        /// 返回相差的秒数
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan ts = System.DateTime.Now - System.DateTime.Parse(Time).AddSeconds(Sec);
            if (ts.TotalSeconds > int.MaxValue)
                return int.MaxValue;

            else if (ts.TotalSeconds < int.MinValue)
                return int.MinValue;

            return (int)ts.TotalSeconds;
        }

        /// <summary>
        /// 返回相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (string.IsNullOrEmpty(time))
                return 1;

            TimeSpan ts = System.DateTime.Now - System.DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
                return int.MaxValue;
            else if (ts.TotalMinutes < int.MinValue)
                return int.MinValue;

            return (int)ts.TotalMinutes;
        }

        /// <summary>
        /// 返回相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (string.IsNullOrEmpty(time))
                return 1;

            TimeSpan ts = System.DateTime.Now - System.DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
                return int.MaxValue;
            else if (ts.TotalHours < int.MinValue)
                return int.MinValue;

            return (int)ts.TotalHours;
        }
    }
}
