using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Context;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;
using techYard.Service.Services.CategoryServices.Dtos;
using techYard.Service.Services.featuresServices.Dtos;

namespace techYard.Service.Services.featuresServices
{
    public class ProductFeatureServices : IProductFeatureServices
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        readonly techYardDbContext _context;
        public ProductFeatureServices(IUnitOfWork unitOfWork, IMapper mapper, techYardDbContext context)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IReadOnlyList<GetFeatureDto>> GetAllProductsFeatures()
        {
            var features = await _unitOfWork.Repository<ProductFeatures>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<GetFeatureDto>>(features);
        }

        public async Task<GetFeatureDto> GetProductFeaturesById(int id)
        {
            var feature = await _unitOfWork.Repository<ProductFeatures>().GetByIdAsync(id);
            if (feature == null)
            {
                return null;
            }
            return _mapper.Map<GetFeatureDto>(feature);
        }

        public async Task AddProductFeature(AddFeatureDto featureDto)
        {
            var feature = _mapper.Map<ProductFeatures>(featureDto);
            await _unitOfWork.Repository<ProductFeatures>().AddAsync(feature);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetFeatureDto> DeleteProductFeatureById(int id)
        {
            var feature = await _unitOfWork.Repository<ProductFeatures>().GetByIdAsync(id);
            if (feature == null)
            {
                return null;
            }
            await _unitOfWork.Repository<ProductFeatures>().Delete(id);
            await _unitOfWork.CompleteAsync();
            return new GetFeatureDto { };

        }


        public async Task<GetFeatureDto> UpdateProductFeatures(int id, AddFeatureDto featureDto)
        {
            var existingFeature = await _unitOfWork.Repository<ProductFeatures>().GetByIdAsync(id);
            if (existingFeature == null)
            {
                return null;
            }

            _mapper.Map(featureDto, existingFeature);
            await _unitOfWork.Repository<ProductFeatures>().Update(existingFeature);
            await _unitOfWork.CompleteAsync();
            return new GetFeatureDto { };

        }
    }
}
