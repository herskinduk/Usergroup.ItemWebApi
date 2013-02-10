using System;

namespace ItemWebApiExtension
{
    public class HardCodedThrottleStrategyFactory : IThrottleStrategyFactory
    {
        public IThrottleStrategy CreateInstance(DateTime timestamp)
        {
            var strategyToCreate = GetStrategyToCreate();

            if (strategyToCreate == typeof(MovingAverage))
            {
                return new MovingAverage(timestamp, CreateMovingAverageConfig());
            }
            else
            {
                throw new InvalidOperationException(string.Format("Invalid strategy ({0})", strategyToCreate.FullName));
            }
        }

        private static MovingAverageConfig CreateMovingAverageConfig()
        {
            // TODO read from config / SC item etc
            const int activityInterval = 7500; // TODO 10000;
            const int minimumRequestInterval = 750; // TODO  1000;
            return new MovingAverageConfig(activityInterval, minimumRequestInterval);
        }

        private static Type GetStrategyToCreate()
        {
            // TODO read configuration to determine what type of strategy to employ
            return typeof(MovingAverage);
        }
    }
}
