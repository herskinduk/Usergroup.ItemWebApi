using System;

namespace ItemWebApiExtension
{
    public interface IThrottleStrategy
    {
        bool IsAllowed(DateTime requestTime);
    }
}
