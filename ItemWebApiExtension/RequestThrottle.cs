using Sitecore.ItemWebApi.Pipelines.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace ItemWebApiExtension
{
    public class RequestThrottle : RequestProcessor
    {
        #region Members
        public double ActivityInterval
        {
            get;
            set;
        }

        public int MinimumRequestInterval
        {
            get;
            set;
        }

        private HttpContextWrapper _context;

        private HttpContextWrapper Context
        {
            get
            {
                if (_context != null)
                    return _context;
                else
                    return new HttpContextWrapper(HttpContext.Current);
            }
        }


        public RequestThrottle(HttpContextWrapper context) : this()
        {
            _context = context;
        }

        public RequestThrottle()
        {
            ActivityInterval = 10000;
            MinimumRequestInterval = 1000;
        } 
        #endregion

        public override void Process(RequestArgs arguments)
        {
            if (AverageRequestIntervalLessThanMinimum())
            {
                throw new UnauthorizedAccessException("Request limit exceeded.");
            }
        }

        #region Implementation
        public bool AverageRequestIntervalLessThanMinimum()
        {
            var requestKey = GenerateRequestKey();
            var movingAverage = GetMovingAverage(requestKey);

            movingAverage.IncrementCountAndRecalculate(Context.Timestamp);
            UpdateMovingAverage(requestKey, movingAverage);

            return (movingAverage.AverageResponseInterval < MinimumRequestInterval);
        }

        private string GenerateRequestKey()
        {
            return "AVG_THROTTLE_" + Context.Request.UserHostAddress;
        }

        private MovingAverage GetMovingAverage(string requestKey)
        {
            if (Context.Cache[requestKey] == null)
            {
                Context.Cache.Add(
                    requestKey,
                    new MovingAverage(ActivityInterval, Context.Timestamp),
                    null,
                    DateTime.Now.AddMilliseconds(ActivityInterval),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Low,
                    null);
            }

            return Context.Cache[requestKey] as MovingAverage;
        }

        private MovingAverage UpdateMovingAverage(string requestKey, MovingAverage movingAverage)
        {
            if (Context.Cache[requestKey] == null)
            {
                Context.Cache.Add(
                    requestKey,
                    movingAverage,
                    null,
                    DateTime.Now.AddSeconds(ActivityInterval),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Low,
                    null);
            }

            return Context.Cache[requestKey] as MovingAverage;
        }

        public class MovingAverage
        {
            public int Count { get; set; }
            public double AverageResponseInterval { get; set; }
            public DateTime LastRequest { get; set; }

            public MovingAverage(double interval, DateTime requestTime)
            {
                Count = 1;
                LastRequest = requestTime;
                AverageResponseInterval = interval;
            }

            public void IncrementCountAndRecalculate(DateTime requestTime)
            {
                var timespan = requestTime.Subtract(LastRequest);
                if (timespan.TotalMilliseconds != 0)
                {
                    AverageResponseInterval += ((double)timespan.TotalMilliseconds - AverageResponseInterval) / ++Count;
                }

                LastRequest = requestTime;
            }
        }
        #endregion
    }
}