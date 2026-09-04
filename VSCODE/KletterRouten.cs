using System;

namespace KletternRoutenApp.Models
{
    public class KletterRouten
    {
        public bool IsChecked { get; set; }
        public string? Content { get; set; }
        public string? Gym { get; set; }
        public string? Schwierigkeitsgrad { get; set; }
        public DateTimeOffset? Datum { get; set; }
    }
}