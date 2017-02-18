using System;

namespace LibCafe
{
    public delegate void PintStartedHandler(object sender, EventArgs e);
    public delegate void PintCompletedHandler(object sender, PintCompletedArgs e);
    public delegate void DishHalfwayHandler(object sender, EventArgs e);
    public delegate void DishCompletedHandler(object sender, DishCompletedArgs e);

    public class PintDish
    {
        public event DishHalfwayHandler DishHalfway;
        public event DishCompletedHandler DishCompleted;
        public event PintStartedHandler PintStarted;
        public event PintCompletedHandler PintCompleted;

        private int pintCount;
        private DateTime dishStarted;


        public int PintCount { get { return pintCount; } } // c#6.0 enkel get in property: set enkel in constructor
        public int MaxPints { get; }

        public PintDish(int maxPints)
        {
            MaxPints = maxPints;
            dishStarted = DateTime.Now;
        }

        public void AddPint()
        {
            if (pintCount >= MaxPints) throw new Exception("Dish full, order cancelled");

            DateTime pintStarted = DateTime.Now;
            PintStarted?.Invoke(this, EventArgs.Empty);
            pintCount++;
            PintCompleted?.Invoke(this, new PintCompletedArgs(pintStarted));

            int halfWayPoint =
                MaxPints % 2 == 0 ? MaxPints / 2 : MaxPints / 2 + 1;
            if (PintCount == halfWayPoint)
                DishHalfway?.Invoke(this, EventArgs.Empty);
            if (PintCount >= MaxPints)
                DishCompleted?.Invoke(this, new DishCompletedArgs(dishStarted));
        }
    }


    public class PintCompletedArgs : EventArgs
    {
        private static string[] Brands = { "Cara Pils", "Jupiler", "Stella Artois", "Bavik" };
        private static string[] Waiters = { "Jeff", "Carine", "Billy", "Julie" };
        public static Random random = new Random();

        public string Brand { get; }
        public string Waiter { get; }

        public PintCompletedArgs(DateTime startTime)
        {
            Brand = Brands[random.Next(0, Brands.Length)];
            Waiter = Waiters[random.Next(0, Waiters.Length)];
        }
    }

    public class DishCompletedArgs : EventArgs
    {
        public TimeSpan CreationTimeNeeded { get; }

        public DishCompletedArgs(DateTime startTime)
        {
            CreationTimeNeeded = DateTime.Now - startTime;
        }
    }
}
