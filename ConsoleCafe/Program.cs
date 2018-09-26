using LibCafe;
using System;

namespace ConsoleCafe
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPints = 5;
            PintDish pintDish = new PintDish(numberOfPints, "Marcel");
            PintDish pintDish2 = new PintDish(numberOfPints + 10, "Ursula");

            pintDish.PintStarted += PintDish_PintStarted;
            pintDish.PintCompleted += PintDish_PintCompleted;
            pintDish.DishHalfway += PintDish_DishHalfway;
            pintDish.DishCompleted += PintDish_DishCompleted;
            pintDish2.PintStarted += PintDish_PintStarted;
            pintDish2.PintCompleted += PintDish_PintCompleted;
            pintDish2.DishHalfway += PintDish_DishHalfway;
            pintDish2.DishCompleted += PintDish_DishCompleted;


            for (int i = 0; i < numberOfPints + 10 ; i++)
            {
                try
                {
                    pintDish.AddPint();
                    pintDish2.AddPint();
                    Console.WriteLine($"Pint {pintDish.PintCount} added\n\n");
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }

        

        private static void PintDish_PintStarted(object sender, EventArgs e)
        {
            Console.WriteLine($"Brewing Pint... on dish{((PintDish)sender).DishName}");
        }

        private static void PintDish_PintCompleted(object sender, PintCompletedArgs e)
        {
            Console.WriteLine($"{e.Brand} brewed by {e.Waiter}, cheers!");
        }

        private static void PintDish_DishHalfway(object sender, EventArgs e)
        {
            Console.WriteLine($"Dish halfway, get ready...");
        }

        private static void PintDish_DishCompleted(object sender, DishCompletedArgs e)
        {
            Console.WriteLine($"Dish completed in {e.CreationTimeNeeded.TotalMilliseconds} ms, enjoy your drinks!");
        }
    }
}
