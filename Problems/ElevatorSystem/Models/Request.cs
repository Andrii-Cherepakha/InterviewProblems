namespace Problems.ElevatorSystem.Models
{
    internal class Request
    {
        public Direction Direction { get; private set; }
        public int From { get; private set; }
        public int To { get; private set; }

        public Request(Direction direction, int from, int to)
        {
            if (direction == Direction.Up && from >= to)
                throw new ArgumentException("FROM cannot be less than TO for UP direction.");

            if (direction == Direction.Down && from <= to)
                throw new ArgumentException("FROM cannot be greater than TO for DOWN direction.");

            this.Direction = direction;
            this.From = from;
            this.To = to;
        }
    }
}