namespace ItemWebApiExtension
{
    public interface IThrottleStrategyFactory
    {
        IThrottleStrategy CreateInstance();
    }
}
