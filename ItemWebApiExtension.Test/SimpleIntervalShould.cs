using System;

using NUnit.Framework;

namespace ItemWebApiExtension.Test
{
    [TestFixture]
    public class SimpleIntervalShould
    {
        [Test]
        public void AllowTheFirstRequestItReceives()
        {
            // Arrange
            var strategy = new SimpleInterval(new SimpleIntervalConfig(150));

            // Act
            bool isAllowed = strategy.IsAllowed(DateTime.Now);

            // Assert
            Assert.That(isAllowed, Is.True);
        }

        [Test]
        public void DisAllowRequestIfWithinIntervalPeriod()
        {
            // Arrange
            var strategy = new SimpleInterval(new SimpleIntervalConfig(10000));
            DateTime firstRequestTime = DateTime.Now;
            strategy.IsAllowed(firstRequestTime);

            // Act
            bool isAllowed = strategy.IsAllowed(firstRequestTime.AddMilliseconds(100));

            // Assert
            Assert.That(isAllowed, Is.False);
        }


        [Test]
        public void AllowRequestIfOverIntervalPeriod()
        {
            // Arrange
            var strategy = new SimpleInterval(new SimpleIntervalConfig(10000));
            DateTime firstRequestTime = DateTime.Now;
            strategy.IsAllowed(firstRequestTime);

            // Act
            bool isAllowed = strategy.IsAllowed(firstRequestTime.AddMilliseconds(10001));

            // Assert
            Assert.That(isAllowed, Is.True);
        }
    }
}
