namespace Viridisca.Common.Application.Time
{
    public abstract class DateTimeProvider
    {
        private static DateTimeProvider _current = new DefaultDateTimeProvider();

        public static DateTimeProvider Current
        {
            get => _current;
            set => _current = value ?? new DefaultDateTimeProvider();
        }

        public abstract DateTime UtcNow { get; }

        public static void Reset()
        {
            _current = new DefaultDateTimeProvider();
        }

        private class DefaultDateTimeProvider : DateTimeProvider
        {
            public override DateTime UtcNow => DateTime.UtcNow;
        }
    }
} 