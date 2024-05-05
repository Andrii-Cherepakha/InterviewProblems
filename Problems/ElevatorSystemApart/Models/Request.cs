namespace Problems.ElevatorSystemApart.Models
{
    internal class Request
    {
        public int Id { get; private set; }
        public Direction Direction { get; private set; }
        public int Floor { get; private set; }
        public string Description { get; private set; }

        public long Ticks { get; private set; }

        public Request(int Id, string description, Direction direction, int floor)
        {
            this.Id = Id;
            this.Description = description;
            this.Floor = floor;
            this.Direction = direction;
            Ticks = DateTime.Now.Ticks;
        }
    }
}
