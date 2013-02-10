using System;

using Sitecore.ItemWebApi.Pipelines.Request;

namespace ItemWebApiExtension
{
    public class RequestThrottle : RequestProcessor
    {
        private readonly IThrottle _throttle;

        public RequestThrottle(IThrottle throttle)
        {
            _throttle = throttle;
        }

        public RequestThrottle() : this(new HttpRequestThrottle())
        {
        }

        public override void Process(RequestArgs arguments)
        {
            if (! _throttle.IsAllowed())
            {
                throw new UnauthorizedAccessException("Request limit exceeded.");
            }
        }
    }
}