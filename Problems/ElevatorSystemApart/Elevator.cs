using Problems.ElevatorSystemApart.Models;
using Problems.ElevatorSystemApart.Queues;

namespace Problems.ElevatorSystemApart
{
    internal class Elevator
    {
        private int currentFloor;
        private Queue queue;

        private HashSet<int> floorsToStop = new HashSet<int>();
        private List<Request> requests = new List<Request>(); // cache

        public Elevator(int floor, Queue queue)
        {
            this.currentFloor = floor;
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
            ProcessRequest(request);
            if (request.Floor >= currentFloor)
                MoveUp();
            else
                MoveDown();
        }

        private void MoveUp()
        {
            while (floorsToStop.Any())
            {
                History.Add("Now on " + currentFloor);
                var rs = queue.GetUp(currentFloor); // update the queue
                foreach (var request in rs)
                {
                    ProcessRequest(request);
                }

                if (floorsToStop.Contains(currentFloor))
                {
                    History.Add("Stopped at " + currentFloor);
                    ProcessFloor();
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
                foreach (var request in rs)
                {
                    ProcessRequest(request);
                }

                if (floorsToStop.Contains(currentFloor))
                {
                    History.Add("Stopped at " + currentFloor);
                    ProcessFloor();
                }

                floorsToStop.Remove(currentFloor);
                if (floorsToStop.Any()) currentFloor--;
            }
        }

        private void ProcessRequest(Request request)
        {
            floorsToStop.Add(request.Floor);
            requests.Add(request);
        }

        private void ProcessFloor()
        {
            foreach (var request in requests.Where(r => r.Floor == currentFloor).ToList())
            {
                History.Add(request.Id + " " + request.Description);
                requests.Remove(request);
            }
        }
    }
}
