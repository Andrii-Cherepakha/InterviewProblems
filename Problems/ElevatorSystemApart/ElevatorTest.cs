using NUnit.Framework;
using Problems.ElevatorSystemApart.Models;
using Problems.ElevatorSystemApart.Queues;

namespace Problems.ElevatorSystemApart
{
    internal class ElevatorTest
    {
        private Queue queue;

        [SetUp]
        public void SetUp()
        {
            History.Clear();
            queue = new Queue();
        }

        [TearDown]
        public void TearDown()
        {
            History.Print();
        }

        #region One request

        [Test]
        public void One_FromAboveToUp()
        {
            var elevator = new Elevator(1, queue);
            queue.Add(new Request(1, "IN", Direction.Up, 5));
            queue.Add(new Request(1, "OUT", Direction.Up, 8));
            elevator.Start();
        }

        [Test]
        public void One_FromBelowToUp()
        {
            var elevator = new Elevator(7, queue);
            queue.Add(new Request(1, "IN", Direction.Up, 5));
            queue.Add(new Request(1, "OUT", Direction.Up, 8));
            elevator.Start();
        }

        [Test]
        public void One_FromAboveToDown()
        {
            var elevator = new Elevator(7, queue);
            queue.Add(new Request(1, "IN", Direction.Down, 5));
            queue.Add(new Request(1, "OUT", Direction.Down, 4));
            elevator.Start();
        }

        [Test]
        public void One_FromBelowToDown()
        {
            var elevator = new Elevator(2, queue);
            queue.Add(new Request(1, "IN", Direction.Down, 8));
            queue.Add(new Request(1, "OUT", Direction.Down, 1));
            elevator.Start();
        }

        #endregion

        [Test]
        public void One_FromBelowToDown_SameFloor()
        {
            var elevator = new Elevator(2, queue);
            queue.Add(new Request(1, "IN", Direction.Up, 4));
            queue.Add(new Request(2, "IN", Direction.Up, 6));
            queue.Add(new Request(1, "OUT", Direction.Up, 6));
            queue.Add(new Request(2, "OUT", Direction.Up, 8));
            elevator.Start();
        }

        [Test]
        public void Two_FromBelowToUp_NoIntersected()
        {
            var elevator = new Elevator(1, queue);
            queue.Add(new Request(1, "IN", Direction.Up, 3));
            queue.Add(new Request(1, "OUT", Direction.Up, 5));
            queue.Add(new Request(2, "IN", Direction.Up, 7));
            queue.Add(new Request(2, "OUT", Direction.Up, 9));
            elevator.Start();
        }

        [Test]
        public void Two_FromBelowToUp_Intersected()
        {
            var elevator = new Elevator(1, queue);
            queue.Add(new Request(1, "IN", Direction.Up, 3));
            queue.Add(new Request(1, "OUT", Direction.Up, 6));
            queue.Add(new Request(2, "IN", Direction.Up, 4));
            queue.Add(new Request(2, "OUT", Direction.Up, 9));
            elevator.Start();
        }

        [Test]
        public void Two_FromBelowToUp_Overlapped()
        {
            var elevator = new Elevator(1, queue);
            queue.Add(new Request(1, "IN", Direction.Up, 3));
            queue.Add(new Request(1, "OUT", Direction.Up, 9));
            queue.Add(new Request(2, "IN", Direction.Up, 5));
            queue.Add(new Request(2, "OUT", Direction.Up, 7));
            elevator.Start();
        }

        [Test]
        public void Two_FromAboveToUp_NoIntersected_1()
        {
            var elevator = new Elevator(10, queue);
            queue.Add(new Request(1, "IN", Direction.Up, 3));
            // elevator started to move toward 3 floor, let's say on 8 floor get the next request:
            queue.Add(new Request(2, "IN", Direction.Up, 7));
            queue.Add(new Request(1, "OUT", Direction.Up, 5));
            queue.Add(new Request(2, "OUT", Direction.Up, 9));
            elevator.Start();
        }

        [Test]
        public void Two_FromAboveToUp_NoIntersected_2()
        {
            var elevator = new Elevator(10, queue);
            queue.Add(new Request(1, "IN", Direction.Up, 7));
            // elevator started to move toward 7 floor, let's say on 8 floor get the next request:
            queue.Add(new Request(2, "IN", Direction.Up, 3));
            // elevator arrived to 7 floor. What's next?
            // Go down to take on #2 because it requested IN before #1 requested OUT?
            // Go up to take off #1 because the ride is not ended?
            queue.Add(new Request(1, "OUT", Direction.Up, 9));
            queue.Add(new Request(2, "OUT", Direction.Up, 5));
            elevator.Start();
        }
    }
}
