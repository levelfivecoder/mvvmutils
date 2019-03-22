using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtils
{
    /// <summary>
    /// Extension Utils
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Task extension to add a timeout.
        /// </summary>
        /// <returns>The task with timeout.</returns>
        /// <param name="task">Task.</param>
        /// <param name="timeoutInMilliseconds">Timeout duration in Milliseconds.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async static Task<T> WithTimeout<T>(this Task<T> task, int timeoutInMilliseconds)
        {
            var retTask = await Task.WhenAny(task, Task.Delay(timeoutInMilliseconds))
                .ConfigureAwait(false);

            if (retTask is Task<T>)
                return task.Result;

            return default(T);
        }

        /// <summary>
        /// Task extension to add a timeout.
        /// </summary>
        /// <returns>The task with timeout.</returns>
        /// <param name="task">Task.</param>
        /// <param name="timeout">Timeout Duration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout) =>
            WithTimeout(task, (int)timeout.TotalMilliseconds);

        /// <summary>
        /// Converts date to OADate.
        /// </summary>
        /// <returns>oadate value</returns>
        /// <param name="date">Date</param>
        public static double ToOaDate(DateTime date)
        {
            string dateFormat = "ddd MMM dd yyyy HH:mm:ss";
            string baseDate = "Sat Dec 30 1899 00:00:00";
            DateTime dt = DateTime.ParseExact(baseDate, dateFormat, CultureInfo.InvariantCulture);
            double totalDays = (date - dt).TotalDays;
            totalDays = Math.Floor(totalDays);
            double hours = 0;
            double.TryParse(date.ToString("HH"), out hours);
            double fraction = hours / 24;
            double oadate = totalDays + fraction;
            return oadate;
        }

        /// <summary>
        /// Round the seconds to zero in a date time object
        /// </summary>
        /// <returns>DateTime object with seconds zero</returns>
        /// <param name="date">DateTime object</param>
        public static DateTime RoundOfSeconds(this DateTime date)
        {
            date = date.AddSeconds(-date.Second).AddMilliseconds(-date.Millisecond);
            return date;
        }

        /// <summary>
        /// Resets time part in the DateTime object
        /// </summary>
        /// <returns>DateTime object with time reset</returns>
        /// <param name="date">DateTime object</param>
        public static DateTime RoundOfDay(this DateTime date)
        {
            date = date.AddHours(-date.Hour).AddMinutes(-date.Minute).AddSeconds(-date.Second).AddMilliseconds(-date.Millisecond);
            return date;
        }

        /// <summary>
        /// Resets seconds part
        /// </summary>
        /// <returns>TimeSpan object with time reset</returns>
        /// <param name="date">DateTime object</param>
        public static TimeSpan RoundOfSeconds(this TimeSpan time)
        {
            time = new TimeSpan(time.Hours, time.Minutes, 0);
            return time;
        }

    }
}
