using System;

namespace ItemWebApiExtension
{
    public interface IThrottleStrategyFactory
    {
        IThrottleStrategy CreateInstance(DateTime timestamp);
    }
}
