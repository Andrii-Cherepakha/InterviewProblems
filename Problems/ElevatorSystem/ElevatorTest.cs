using NUnit.Framework;
using Problems.ElevatorSystem.Models;

namespace Problems.ElevatorSystem
{
    public class ElevatorTest
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
            queue.Add(new Request(Direction.Up, 5, 8));
            elevator.Start();
        }

        [Test]
        public void One_FromBelowToUp()
        {
            var elevator = new Elevator(7, queue);
            queue.Add(new Request(Direction.Up, 5, 8));
            elevator.Start();
        }

        [Test]
        public void One_FromAboveToDown()
        {
            var elevator = new Elevator(7, queue);
            queue.Add(new Request(Direction.Down, 5, 4));
            elevator.Start();
        }

        [Test]
        public void One_FromBelowToDown()
        {
            var elevator = new Elevator(2, queue);
            queue.Add(new Request(Direction.Down, 8, 1));
            elevator.Start();
        }

        #endregion

        [Test]
        public void Two_FromBelowToUp_NoIntersected()
        {
            var elevator = new Elevator(1, queue);
            queue.Add(new Request(Direction.Up, 3, 5));
            queue.Add(new Request(Direction.Up, 7, 9));
            elevator.Start();
        }

        [Test]
        public void Two_FromBelowToUp_Intersected()
        {
            var elevator = new Elevator(1, queue);
            queue.Add(new Request(Direction.Up, 3, 6));
            queue.Add(new Request(Direction.Up, 4, 9));
            elevator.Start();
        }

        [Test]
        public void Two_FromBelowToUp_Overlapped()
        {
            var elevator = new Elevator(1, queue);
            queue.Add(new Request(Direction.Up, 3, 9));
            queue.Add(new Request(Direction.Up, 5, 7));
            elevator.Start();
        }

        [Test]
        public void Two_FromAboveToUp_NoIntersected_1()
        {
            var elevator = new Elevator(10, queue);
            queue.Add(new Request(Direction.Up, 3, 5));
            queue.Add(new Request(Direction.Up, 7, 9));
            elevator.Start();
        }

        [Test]
        public void Two_FromAboveToUp_NoIntersected_2()
        {
            var elevator = new Elevator(10, queue);
            queue.Add(new Request(Direction.Up, 7, 9));
            queue.Add(new Request(Direction.Up, 3, 5));
            elevator.Start();

            /*
              ----- elevator
                a
              -----
                b       UP
              -----
                c
              -----
                d       UP
              -----

              1) Keep nearest : a + b + b + b + c + d + d = a + 3b + c  + 2d
              2) Keep farthest: a + b + c + d + d + c + b = a + 2b + 2c + 2d

             * */
        }

        [Test]
        public void Two_FromAboveToUp_Intersected_1()
        {
            var elevator = new Elevator(10, queue);
            queue.Add(new Request(Direction.Up, 3, 6));
            queue.Add(new Request(Direction.Up, 4, 9));
            elevator.Start();
        }

        [Test]
        public void Two_FromAboveToUp_Intersected_2()
        {
            var elevator = new Elevator(10, queue);
            queue.Add(new Request(Direction.Up, 4, 9));
            queue.Add(new Request(Direction.Up, 3, 6));
            elevator.Start();

            /*
             
             */
        }

        [Test]
        public void Two_FromAboveToUp_DownInTheMiddle()
        {
            var elevator = new Elevator(10, queue);
            queue.Add(new Request(Direction.Up, 3, 6));
            queue.Add(new Request(Direction.Down, 5, 1)); // stop to pick up? ignore? pick up and ignore previous?
            elevator.Start();
        }
    }
}
