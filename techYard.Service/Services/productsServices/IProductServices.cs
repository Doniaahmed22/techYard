﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Service.Services.productsServices.Dtos;

namespace techYard.Service.Services.productsServices
{
    public interface IProductServices
    {
        Task<IReadOnlyList<getProduct>> GetAllProducts();
        Task<getProduct> GetProductById(int id);
        Task<AddProductDto> AddProduct(AddProductDto productDto);
        Task<getProduct> UpdateProduct(int id, getProduct productDto);

        Task<Products?> DeleteProduct(int id);

        //Task AddParent(ParentDto parentDto);
        //Task UpdateParent(int id, ParentDto entity);
        //Task DeleteParent(int id);
        //Task<IEnumerable<StudentNameGender>> GetStudentsOfParents(int id);
    }
}
