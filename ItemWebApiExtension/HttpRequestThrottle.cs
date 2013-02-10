using System;
using System.Web;
using System.Web.Caching;

namespace ItemWebApiExtension
{
    public class HttpRequestThrottle : IThrottle
    {
        private readonly HttpContextBase _context;
        private readonly IThrottleStrategyFactory _throttleStrategyFactory;

        public HttpRequestThrottle(HttpContextBase context, IThrottleStrategyFactory throttleStrategyFactory)
            : this()
        {
            _context = context;
            _throttleStrategyFactory = throttleStrategyFactory;
        }

        public HttpRequestThrottle()
        {
            ActivityInterval = 7500;  // TODO should come from DI
        }

        private HttpContextBase Context
        {
            get
            {
                return _context ?? new HttpContextWrapper(HttpContext.Current);
            }
        }

        private IThrottleStrategyFactory ThrottleStrategyFactory
        {
            get
            {
                return _throttleStrategyFactory ?? new HardCodedThrottleStrategyFactory();
            }
        }

        /// <summary>
        /// Timeframe for tracking request activity (in milliseconds)
        /// </summary>
        public double ActivityInterval { get; set; }

        public bool IsAllowed()
        {
            return ! IsRequestedAllowedByStrategy();
        }

        public bool IsRequestedAllowedByStrategy()
        {
            var requestKey = GenerateRequestKey();
            var throttleStrategy = GetThrottleStrategyFromCache(requestKey);
            var isAllowed = throttleStrategy.IsAllowed(Context.Timestamp);
            RefreshThrottleStrategyInCache(requestKey, throttleStrategy);

            return isAllowed;
        }

        private string GenerateRequestKey()
        {
            return "AVG_THROTTLE_" + Context.Request.UserHostAddress;
        }

        private IThrottleStrategy GetThrottleStrategyFromCache(string requestKey)
        {
            if (Context.Cache[requestKey] == null)
            {
                var throttleStrategy = ThrottleStrategyFactory.CreateInstance(Context.Timestamp);
                AddToCache(requestKey, throttleStrategy);
            }

            return Context.Cache[requestKey] as IThrottleStrategy;
        }

        private void RefreshThrottleStrategyInCache(string requestKey, IThrottleStrategy throttleStrategy)
        {
            if (Context.Cache[requestKey] == null)
            {
                AddToCache(requestKey, throttleStrategy);
            }
        }

        private void AddToCache(string requestKey, IThrottleStrategy throttleStrategy)
        {
            Context.Cache.Add(
                requestKey,
                throttleStrategy,
                null,
                DateTime.Now.AddSeconds(ActivityInterval),
                Cache.NoSlidingExpiration,
                CacheItemPriority.Low,
                null);
        }
    }
}
