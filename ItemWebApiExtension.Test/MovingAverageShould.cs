using System;

using NUnit.Framework;

namespace ItemWebApiExtension.Test
{
    [TestFixture]
    public class MovingAverageShould
    {
        [Test]
        public void AllowTheFirstRequestItReceives()
        {
            // Arrange
            var strategy = new MovingAverage(new MovingAverageConfig(10000, 1000));

            // Act
            bool isAllowed = strategy.IsAllowed(DateTime.Now);

            // Assert
            Assert.That(isAllowed, Is.True);
        }

        [Test]
        public void AllowRequestWhenMovingAverageLimitHasNotBeenTriggered()
        {
            // Arrange
            var strategy = new MovingAverage(new MovingAverageConfig(10000, 1000));
            DateTime firstRequestTime = DateTime.Now;
            strategy.IsAllowed(firstRequestTime);

            // Act
            bool isAllowed = strategy.IsAllowed(firstRequestTime.AddMilliseconds(100));

            // Assert
            Assert.That(isAllowed, Is.True);
        }

        [Test]
        public void DisAllowRequestWhenMovingAverageLimitHasBeenTriggered()
        {
            // Arrange
            var strategy = new MovingAverage(new MovingAverageConfig(10000, 1000));
            DateTime firstRequestTime = DateTime.Now;
            strategy.IsAllowed(firstRequestTime);
            const int maxRequests = 10;

            for (int i = 0; i < maxRequests; i++)
            {
                strategy.IsAllowed(firstRequestTime.AddMilliseconds((i * 100) + 100));
            }

            // Act
            bool isAllowed = strategy.IsAllowed(firstRequestTime.AddMilliseconds((maxRequests * 100) + 100));

            // Assert
            Assert.That(isAllowed, Is.False);
        }
    }
}
