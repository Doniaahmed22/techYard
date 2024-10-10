using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Service.Services.featuresServices.Dtos
{
    public class GetFeatureDto
    {
        public int Id { get; set; }
        public string? model { get; set; }
        public string? processor { get; set; }
        public string? OS { get; set; }
        public string? graphicCard { get; set; }
        public string? storage { get; set; }
        public string? ramSize { get; set; }
        public string? ramType { get; set; }
        public string? dimensions { get; set; }
        public string? weight { get; set; }
        public Products? products { get; set; }
    }
}
