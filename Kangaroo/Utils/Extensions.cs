using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Infrastructure.CommandProcessor;
using SimpleInjector;

namespace Kangaroo.Utils
{
    public static class Extensions
    {
        public static void RegisterLazy<T>(this Container container) where T : class
        {
            Func<T> factory = () => container.GetInstance<T>();

            container.Register<Lazy<T>>(() => new Lazy<T>(factory));
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static bool None<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }
    }
}