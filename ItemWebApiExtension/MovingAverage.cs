using System;

namespace ItemWebApiExtension
{
    public class MovingAverage : IThrottleStrategy
    {
        private int _count;
        private DateTime _lastRequest;
        private readonly MovingAverageConfig _config;

        public MovingAverage(MovingAverageConfig config)
        {
            _count = 1;
            _config = config;
        }

        public bool IsAllowed(DateTime requestTime)
        {
            bool isAllowed;
            if (_lastRequest == DateTime.MinValue)
            {
                isAllowed = true;
            }
            else
            {
                var timespan = requestTime.Subtract(_lastRequest);

                if (timespan.TotalMilliseconds > 0)
                {
                    _config.ActivityInterval += (timespan.TotalMilliseconds - _config.ActivityInterval) / ++_count;
                }

                isAllowed = (_config.ActivityInterval > _config.MinimumRequestInterval);
            }

            _lastRequest = requestTime;

            return isAllowed;
        }
    }
}
