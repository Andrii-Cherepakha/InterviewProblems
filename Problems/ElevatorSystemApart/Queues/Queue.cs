using Problems.ElevatorSystemApart.Models;

namespace Problems.ElevatorSystemApart.Queues
{
    internal class Queue
    {
        private List<Request> requests = new List<Request>(); // like queue by request time
        

        public void Add(Request request)
        {
            requests.Add(request);
        }

        public Request GetAny()
        {
            // get nearest to the current floor OR first arrived by time?
            Request request = null;
            if (requests.Any())
            {
                request = requests[0];
                requests.RemoveAt(0);
            }
            return request;
        }

        public List<Request> GetUp(int floor)
        {
            var rs = requests.Where(r => r.Direction == Direction.Up && r.Floor >= floor).ToList();
            requests.RemoveAll(r => rs.Any(r2 => r.Equals(r2)));
            return rs;
        }

        public List<Request> GetDown(int floor)
        {
            var rs = requests.Where(r => r.Direction == Direction.Down && r.Floor <= floor).ToList();
            requests.RemoveAll(r => rs.Any(r2 => r.Equals(r2)));
            return rs;
        }
    }
}
