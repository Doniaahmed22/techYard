using System.ComponentModel.DataAnnotations.Schema;

namespace techYard.Data.Entities
{
    public class ProductFeatures : BaseEntity
    {
        public string? processor { get; set; }
        public string? graphicCard { get; set; }
        public string? storage { get; set; }
        public string? ramSize { get; set; }
        public string? ramType { get; set; }
        public string? dimensions { get; set; }
        public string? weight { get; set; }
        public string? ScreenSize { get; set; }
    }
}
