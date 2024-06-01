using NUnit.Framework;

namespace Problems.RateLimiter
{
    internal class TokenBucketTest
    {
        [Test]
        public void OverCapacity()
        {
            int capacity = 10;
            var tb = new TokenBucket(capacity, 1, 1);
            Assert.That(tb.Size, Is.EqualTo(capacity));
            Thread.Sleep(2000);
            Assert.That(tb.Size, Is.EqualTo(capacity));
        }

        [Test]
        public void Refill1TokenPer2Seconds() 
        {
            int capacity = 10;
            var tb = new TokenBucket(capacity, 1, 2);
            int cost = 6;
            Assert.That(tb.IsRequestAllowed(cost), Is.True);
            Assert.That(tb.Size, Is.EqualTo(capacity - cost));

            Thread.Sleep(4200);
            int expectedRefill = 2;
            Assert.That(tb.Size, Is.EqualTo(capacity - cost + expectedRefill));
        }

        [Test]
        public void Refill10TokensPer2Seconds()
        {
            int capacity = 10;
            var tb = new TokenBucket(capacity, 10, 2);
            Assert.That(tb.Size, Is.EqualTo(capacity));

            int cost = 8;
            Assert.That(tb.IsRequestAllowed(cost), Is.True);
            Assert.That(tb.Size, Is.EqualTo(capacity - cost));

            Thread.Sleep(1100);
            int expectedRefill = 5;
            Assert.That(tb.Size, Is.EqualTo(capacity - cost + expectedRefill));
        }

        [Test]
        public void NoCapacity()
        {
            int capacity = 10;
            int cost = 6;
            var tb = new TokenBucket(capacity, 1, 10);
            Assert.That(tb.IsRequestAllowed(cost), Is.True);
            Assert.That(tb.IsRequestAllowed(cost), Is.False);
        }
    }
}
