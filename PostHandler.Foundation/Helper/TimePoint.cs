namespace PostHandler.Foundation.Helper
{
    using System;
    using System.Globalization;

    public sealed class TimePoint : IEquatable<TimePoint>
    {
        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        private TimePoint(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public TimePoint AddDays(int days)
        {
            var start = Start.AddDays(days);
            var end = End.AddDays(days);
            return new TimePoint(start, end);
        }

        public static TimePoint Minute(DateTime start, DateTime end)
        {
            start = start.AddMilliseconds(-start.Millisecond);
            end = end.AddMilliseconds(-end.Millisecond).AddMinutes(1).AddMilliseconds(-1);
            return new TimePoint(start, end);
        }

        public static TimePoint SecondFrom(DateTime start)
        {
            start = start.AddMilliseconds(-start.Millisecond);
            return new TimePoint(start, DateTime.MaxValue);
        }

        public static TimePoint SecondUpto(DateTime end)
        {
            end = end.AddMilliseconds(-end.Millisecond).AddSeconds(1).AddMilliseconds(-1);
            return new TimePoint(DateTime.MinValue, end);
        }

        public static TimePoint Second(DateTime start, DateTime end)
        {
            start = start.AddMilliseconds(-start.Millisecond);
            end = end.AddMilliseconds(-end.Millisecond).AddSeconds(1).AddMilliseconds(-1);
            return new TimePoint(start, end);
        }

        public bool IsEmpty()
        {
            return Start > End;
        }

        public bool Includes(DateTime value)
        {
            return Start <= value && value <= End;
        }

        public bool Includes(TimePoint value)
        {
            return Start <= value.Start && value.End <= End;
        }

        public bool Overlaps(DateTime value)
        {
            return Start < value || value > End;
        }

        public bool Overlaps(TimePoint value)
        {
            return Start < value.Start || value.End > End;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(TimePoint))
            {
                return false;
            }
            else
            {
                return Equals((TimePoint)obj);
            }
        }

        public bool Equals(TimePoint other)
        {
            return other.Start == Start && other.End == End;
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} - {1}", Start, End);
        }

    }
}