using System;

namespace JobApplicationTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new JobManager();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== JOB APPLICATION TRACKER ===");
                Console.WriteLine("1. Lägg till ny ansökan");
                Console.WriteLine("2. Visa alla ansökningar");
                Console.WriteLine("3. Filtrera efter status (LINQ)");
                Console.WriteLine("4. Sortera efter datum (LINQ)");
                Console.WriteLine("5. Visa statistik (LINQ)");
                Console.WriteLine("6. Uppdatera status");
                Console.WriteLine("7. Ta bort ansökan");
                Console.WriteLine("8. Avsluta");
                Console.Write("Välj: ");

                switch (Console.ReadLine())
                {
                    case "1": manager.AddJob(); break;
                    case "2": manager.ShowAll(); break;
                    case "3": manager.ShowByStatus(); break;
                    case "4": manager.ShowByDate(); break;
                    case "5": manager.ShowStatistics(); break;
                    case "6": manager.UpdateStatus(); break;
                    case "7": manager.DeleteJob(); break;
                    case "8": running = false; break;
                    default:
                        Console.WriteLine("Ogiltigt val. Tryck valfri knapp...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
