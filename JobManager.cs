using System;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationTracker
{
    // Håller alla ansökningar och logiken kring dem
    public class JobManager
    {
        public List<JobApplication> Applications { get; } = new();

        //  Lägg till ny ansökan
        public void AddJob()
        {
            Console.Clear();
            var job = new JobApplication();

            Console.Write("Företagsnamn: ");
            job.CompanyName = Console.ReadLine() ?? "";

            Console.Write("Jobbtitel: ");
            job.PositionTitle = Console.ReadLine() ?? "";

            job.Status = ReadStatus("Välj status (1=Applied, 2=Interview, 3=Offer, 4=Rejected): ");

            Console.Write("Önskad lön (kr): ");
            int.TryParse(Console.ReadLine(), out int salary);
            job.SalaryExpectation = salary;

            Applications.Add(job);
            Console.WriteLine("\n Ansökan tillagd! Tryck valfri knapp...");
            Console.ReadKey();
        }

        //  Uppdatera status på en ansökan
        public void UpdateStatus()
        {
            Console.Clear();
            Console.Write("Skriv företagsnamn att uppdatera: ");
            string company = Console.ReadLine() ?? "";

            var job = Applications.FirstOrDefault(a =>
                a.CompanyName.Equals(company, StringComparison.OrdinalIgnoreCase));

            if (job is null)
            {
                Console.WriteLine(" Hittade ingen ansökan med det företagsnamnet.");
            }
            else
            {
                job.Status = ReadStatus("Ny status (1=Applied, 2=Interview, 3=Offer, 4=Rejected): ");
                Console.WriteLine(" Status uppdaterad.");
            }
            Console.WriteLine("\nTryck valfri knapp...");
            Console.ReadKey();
        }

        //  Ta bort en ansökan
        public void DeleteJob()
        {
            Console.Clear();
            Console.Write("Skriv företagsnamn att ta bort: ");
            string company = Console.ReadLine() ?? "";

            var job = Applications.FirstOrDefault(a =>
                a.CompanyName.Equals(company, StringComparison.OrdinalIgnoreCase));

            if (job is null) Console.WriteLine(" Ingen träff.");
            else
            {
                Applications.Remove(job);
                Console.WriteLine(" Borttagen.");
            }
            Console.WriteLine("\nTryck valfri knapp...");
            Console.ReadKey();
        }

        //  Visa alla ansökningar (med färg)
        public void ShowAll()
        {
            Console.Clear();
            if (!Applications.Any())
            {
                Console.WriteLine("Inga ansökningar ännu.");
            }
            else
            {
                foreach (var job in Applications)
                {
                    Console.ForegroundColor = job.Status switch
                    {
                        Status.Offer => ConsoleColor.Green,
                        Status.Rejected => ConsoleColor.Red,
                        Status.Interview => ConsoleColor.Yellow,
                        _ => ConsoleColor.White
                    };
                    Console.WriteLine(job.GetSummary());
                }
                Console.ResetColor();
            }
            Console.WriteLine("\nTryck valfri knapp...");
            Console.ReadKey();
        }

        //  LINQ: filtrera efter status
        public void ShowByStatus()
        {
            Console.Clear();
            var wanted = ReadStatus("Filtrera på (1=Applied, 2=Interview, 3=Offer, 4=Rejected): ");

            var filtered = Applications.Where(a => a.Status == wanted);

            if (!filtered.Any()) Console.WriteLine("❌ Inga matchningar.");
            else foreach (var j in filtered) Console.WriteLine(j.GetSummary());

            Console.WriteLine("\nTryck valfri knapp...");
            Console.ReadKey();
        }

        // LINQ: sortera efter datum (äldst först)
        public void ShowByDate()
        {
            Console.Clear();
            var sorted = Applications.OrderBy(a => a.ApplicationDate);
            foreach (var j in sorted) Console.WriteLine(j.GetSummary());
            Console.WriteLine("\nTryck valfri knapp...");
            Console.ReadKey();
        }

        //  LINQ: statistik – totalt, per status, genomsnittlig svarstid
        public void ShowStatistics()
        {
            Console.Clear();
            Console.WriteLine($"Totalt antal ansökningar: {Applications.Count}");

            Console.WriteLine("\nAntal per status:");
            var perStatus = Applications.GroupBy(a => a.Status)
                                        .Select(g => $"{g.Key}: {g.Count()} st");
            foreach (var row in perStatus) Console.WriteLine(row);

            var responseDays = Applications
                .Where(a => a.ResponseDate != null)
                .Select(a => (a.ResponseDate!.Value - a.ApplicationDate).TotalDays);

            Console.WriteLine(responseDays.Any()
                ? $"\nGenomsnittlig svarstid: {responseDays.Average():F1} dagar"
                : "\nGenomsnittlig svarstid: —");

            Console.WriteLine("\nTryck valfri knapp...");
            Console.ReadKey();
        }

        // Hjälpmetod: läs status enkelt (1–4)
        private static Status ReadStatus(string prompt)
        {
            Console.Write(prompt);
            return (Console.ReadLine() ?? "") switch
            {
                "2" => Status.Interview,
                "3" => Status.Offer,
                "4" => Status.Rejected,
                _ => Status.Applied
            };
        }
    }
}
