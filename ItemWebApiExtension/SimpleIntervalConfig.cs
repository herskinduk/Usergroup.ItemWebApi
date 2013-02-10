namespace ItemWebApiExtension
{
    public class SimpleIntervalConfig
    {
        public SimpleIntervalConfig(int minimumRequestInterval)
        {
            MinimumRequestInterval = minimumRequestInterval;
        }

        public int MinimumRequestInterval { get; set; }
    }
}
