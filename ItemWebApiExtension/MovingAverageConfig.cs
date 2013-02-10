namespace ItemWebApiExtension
{
    public class MovingAverageConfig
    {
        public MovingAverageConfig(double activityInterval, int minimumRequestInterval)
        {
            ActivityInterval = activityInterval;
            MinimumRequestInterval = minimumRequestInterval;
        }

        /// <summary>
        /// Timeframe for tracking request activity (in milliseconds)
        /// </summary>
        public double ActivityInterval { get; set; }

        /// <summary>
        /// Lower bound for average time between requests (in milliseconds)
        /// </summary>
        public int MinimumRequestInterval { get; set; }
    }
}
