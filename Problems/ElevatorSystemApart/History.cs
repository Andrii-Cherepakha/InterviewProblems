namespace Problems.ElevatorSystemApart
{
    internal class History
    {
        private static List<string> items = new List<string>();

        public static void Clear()
        {
            items.Clear();
        }

        public static void Add(string item)
        {
            items.Add(item);
        }

        public static void Print() 
        {
            foreach (var item in items) 
            {
                Console.WriteLine(item);
            }
        }
    }
}
