using System;

namespace JobApplicationTracker
{
    //första commit
    // Klass = mall för en (1) ansökan
    public class JobApplication
    {
        // Attribut (egenskaper)
        public string CompanyName { get; set; } = string.Empty; // Företag
        public string PositionTitle { get; set; } = string.Empty; // Jobbtitel
        public Status Status { get; set; } = Status.Applied; // Enum-status
        public DateTime ApplicationDate { get; set; } = DateTime.Now; // Skickad datum
        public DateTime? ResponseDate { get; set; } = null;         // Svar (kan saknas)
        public int SalaryExpectation { get; set; } = 0;            // Önskad lön (kr)

        // Metod: antal dagar sedan ansökan skickades
        public int GetDaysSinceApplied() => (DateTime.Now - ApplicationDate).Days;

        // Metod: kort text att skriva ut
        public string GetSummary() =>
            $"{CompanyName} - {PositionTitle} [{Status}] skickad {ApplicationDate:yyyy-MM-dd}";
    }
}
