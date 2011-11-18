namespace TestDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Helper class to generate Random Data
    /// </summary>
    internal static class Rnd
    {
        private static readonly Random rnd = new Random((int)DateTime.UtcNow.Ticks);

        public static int Integer()
        {
            return rnd.Next(1000);
        }

        public static int Integer(int max)
        {
            return rnd.Next(max);
        }

        public static int Integer(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public static bool Bool()
        {
            return rnd.Next(2) == 1;
        }

        public static uint UInt()
        {
            return (uint)rnd.Next();
        }

        public static long Long()
        {
            return rnd.Next();
        }

        public static ulong ULong()
        {
            return (ulong)rnd.Next();
        }

        public static byte Byte()
        {
            return (byte)rnd.Next();
        }

        public static sbyte SByte()
        {
            return (sbyte)rnd.Next();
        }

        public static ushort UShort()
        {
            return (ushort)rnd.Next();
        }

        public static float Float()
        {
            return (float)(rnd.NextDouble() * Integer());
        }

        public static double Double()
        {
            return (double)(rnd.NextDouble() * Integer());
        }

        public static decimal Decimal()
        {
            return (decimal)Double();
        }
        public static short Short()
        {
            return (short)rnd.Next();
        }

        public static char Char()
        {
            return (char)Byte();
        }

        public static string String(string hint = "")
        {
            string format = hint;
            if (string.IsNullOrWhiteSpace(format))
            {
                format = "string";
            }

            if (format.Contains("{0}"))
            {
                return string.Format(format, Integer());
            }
            else
            {
                return string.Format("{0} {1}", format, Integer());
            }
        }

        public static Uri Uri(string hint = "")
        {
            return new Uri(string.Format("http://{0}/{1}", string.IsNullOrWhiteSpace(hint) ? "string" : hint, Integer()));
        }

        public static DateTime Date()
        {
            return DateTime.UtcNow.AddMilliseconds(Integer() - Integer());
        }

        public static object RandomEnumValue(Type type)
        {
            var vals = Enum.GetValues(type);
            return vals.GetValue(rnd.Next(0, vals.Length));
        }

        public static T RandomFromCollection<T>(IEnumerable<T> candidates)
        {
            return candidates.ElementAt(Integer(candidates.Count()));
        }


    }
}
