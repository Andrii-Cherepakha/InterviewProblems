using Problems.ElevatorSystem.Models;

namespace Problems.ElevatorSystem
{
    internal class Elevator
    {
        private int currentFloor;
        // private Direction direction;
        // private State state;
        private Queue queue;

        private HashSet<int> floorsToStop = new HashSet<int>();

        // public static readonly int MAX_FLOOR = 10;

        public Elevator(int floor, Queue queue)
        {
            this.currentFloor = floor;
            // this.state = State.Idle;
            this.queue = queue;
            History.Add("Start on " + floor);
        }

        public void Start()
        {
            var request = queue.GetAny();
            while (request != null)
            {
                Work(request);
                request = queue.GetAny();
            }
        }

        private void Work(Request request)
        {
            if (request.Direction == Direction.Up)
            {
                if (request.From >= currentFloor)
                {
                    floorsToStop.Add(request.From);
                    floorsToStop.Add(request.To);
                    MoveUp();
                }
                else if (request.From < currentFloor)
                {
                    floorsToStop.Add(request.From);
                    MoveDown();
                    floorsToStop.Add(request.To);
                    MoveUp();
                }
            } else if (request.Direction == Direction.Down)
            {
                if (request.From <= currentFloor)
                {
                    floorsToStop.Add(request.From);
                    floorsToStop.Add(request.To);
                    MoveDown();
                } else if (request.From > currentFloor)
                {
                    floorsToStop.Add(request.From);
                    MoveUp();
                    floorsToStop.Add(request.To);
                    MoveDown();
                }
            }
        }

        private void MoveUp()
        {
            while(floorsToStop.Any())
            {
                History.Add("Now on " + currentFloor);
                var rs = queue.GetUp(currentFloor); // update the queue
                foreach (var r in rs)
                {
                    floorsToStop.Add(r.From);
                    floorsToStop.Add(r.To);
                }

                if (floorsToStop.Contains(currentFloor))
                {
                    History.Add("Stopped at " + currentFloor);
                }

                floorsToStop.Remove(currentFloor);
                if (floorsToStop.Any()) currentFloor++;
            }
        }

        private void MoveDown()
        {
            while (floorsToStop.Any())
            {
                History.Add("Now on " + currentFloor);
                var rs = queue.GetDown(currentFloor); // update the queue
                foreach (var r in rs)
                {
                    floorsToStop.Add(r.From);
                    floorsToStop.Add(r.To);
                }

                if (floorsToStop.Contains(currentFloor))
                {
                    History.Add("Stopped at " + currentFloor);
                }

                floorsToStop.Remove(currentFloor);
                if (floorsToStop.Any()) currentFloor--;
            }
        }
    }
}
