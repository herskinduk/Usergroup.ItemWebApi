using System;

namespace ItemWebApiExtension
{
    public class SimpleInterval : IThrottleStrategy
    {
        private DateTime _lastRequest;
        private readonly SimpleIntervalConfig _config;

        public SimpleInterval(SimpleIntervalConfig config)
        {
            _config = config;            
        }

        public bool IsAllowed(DateTime requestTime)
        {
            var timespan = requestTime.Subtract(_lastRequest);
            _lastRequest = requestTime;
            return (timespan.TotalMilliseconds > _config.MinimumRequestInterval);
        }
    }
}
