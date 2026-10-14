using System.Text.Json.Serialization;

namespace Carglass.TechnicalAssessment.Backend.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ProductType { get; set; }
        public long NumTerminal { get; set; }
        public string SoldAt { get; set; } = string.Empty;
    }
}
