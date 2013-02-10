using System;
using System.Collections.Generic;

namespace ItemWebApiExtension
{
    public class HardCodedThrottleStrategyFactory : IThrottleStrategyFactory
    {
        private readonly Type _defaultThrottleStrategy = typeof(MovingAverage);

        private static readonly Dictionary<Type, Func<IThrottleStrategy>> StrategyBuilders;

        static HardCodedThrottleStrategyFactory()
        {
            StrategyBuilders = new Dictionary<Type, Func<IThrottleStrategy>>
                {
                    { typeof(SimpleInterval), CreateSimpleIntervalStrategy },
                    { typeof(MovingAverage), CreateMovingAverageStrategy }
                };
        }

        private static IThrottleStrategy CreateSimpleIntervalStrategy()
        {
            return new SimpleInterval(CreateSimpleRequestConfig());
        }

        private static IThrottleStrategy CreateMovingAverageStrategy()
        {
            return new MovingAverage(CreateMovingAverageConfig());
        }

        public IThrottleStrategy CreateInstance()
        {
            var strategyToCreate = GetStrategyToCreate();

            if (!StrategyBuilders.ContainsKey(strategyToCreate))
            {
                throw new InvalidOperationException(string.Format("Invalid strategy ({0})", strategyToCreate.FullName));
            }

            return StrategyBuilders[strategyToCreate].Invoke();
        }

        private static SimpleIntervalConfig CreateSimpleRequestConfig()
        {
            // TODO read from config / SC item etc
            const int minimumRequestInterval = 200;
            return new SimpleIntervalConfig(minimumRequestInterval);
        }

        private static MovingAverageConfig CreateMovingAverageConfig()
        {
            // TODO read from config / SC item etc
            const int activityInterval = 7500;
            const int minimumRequestInterval = 750;
            return new MovingAverageConfig(activityInterval, minimumRequestInterval);
        }

        private Type GetStrategyToCreate()
        {
            // TODO read configuration to determine what type of strategy to employ
            return _defaultThrottleStrategy;
        }
    }
}
