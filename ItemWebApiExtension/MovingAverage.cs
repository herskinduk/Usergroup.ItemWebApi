using System;

namespace ItemWebApiExtension
{
    public class MovingAverage : IThrottleStrategy
    {
        private int _count;
        private DateTime _lastRequest;
        private readonly MovingAverageConfig _movingAverageConfig;

        public MovingAverage(DateTime requestTime, MovingAverageConfig movingAverageConfig)
        {
            _count = 1;
            _lastRequest = requestTime;
            _movingAverageConfig = movingAverageConfig;
        }

        public bool IsAllowed(DateTime requestTime)
        {
            var timespan = requestTime.Subtract(_lastRequest);

            if (timespan.TotalMilliseconds > 0)
            {
                _movingAverageConfig.ActivityInterval += (timespan.TotalMilliseconds - _movingAverageConfig.ActivityInterval) / ++_count;
            }

            _lastRequest = requestTime;

            return (_movingAverageConfig.ActivityInterval < _movingAverageConfig.MinimumRequestInterval);
        }
    }
}
