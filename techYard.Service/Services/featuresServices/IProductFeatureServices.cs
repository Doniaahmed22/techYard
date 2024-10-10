using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;

namespace techYard.Service.Services.featuresServices
{
    public interface IProductFeatureServices
    {
        Task<IReadOnlyList<GetFeatureDto>> GetAllProductsFeatures();
        Task<GetFeatureDto> GetProductFeaturesById(int id);
        Task AddProductFeature(AddFeatureDto featureDto);
        Task<GetFeatureDto> DeleteProductFeatureById(int id);
        Task<GetFeatureDto> UpdateProductFeatures(int id, AddFeatureDto entity);
    }
}
