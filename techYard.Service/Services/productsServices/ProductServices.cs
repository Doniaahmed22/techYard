using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;
using techYard.Repository.Repositories;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.productsServices
{
    public class ProductServices :IProductServices
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<getProduct>> GetAllProducts()
        {
            var products = await _unitOfWork.Repository<Products>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<getProduct>>(products);
        }

        public async Task<getProduct> GetProductById(int id)
        {
            var products = await _unitOfWork.Repository<Products>().GetByIdAsync(id);
            return _mapper.Map<getProduct>(products);
        }
    }
}
