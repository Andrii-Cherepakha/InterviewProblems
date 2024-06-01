namespace Problems.RateLimiter
{
    public class TokenBucket
    {
        private readonly int capacity;
        private readonly int refillRate;
        private readonly int refillPeriod;

        private int size;
        private DateTime lastUpdated;

        public TokenBucket(int capacity, int refillRate, int refillPeriod)
        {
            this.capacity = capacity;
            this.refillRate = refillRate; // how many
            this.refillPeriod = refillPeriod; // per how long, seconds
            size = capacity;
            lastUpdated = DateTime.UtcNow;
        }

        public bool IsRequestAllowed(int cost)
        {
            if (Size >= cost)
            {
                size -= cost;
                return true;
            }

            return false;
        }

        protected internal int Size
        { 
            get 
            {
                Refill();
                return size; 
            } 
        }

        private void Refill()
        {
            DateTime now = DateTime.UtcNow;
            int secondsLeft = (now - lastUpdated).Seconds; 
            // int refill = secondsLeft / refillPeriod * refillRate; // precision does matter
            int refill = (int)(secondsLeft / (double) refillPeriod * refillRate);
            size = Math.Min(size + refill, capacity);
            lastUpdated = now;
        }
    }
}
