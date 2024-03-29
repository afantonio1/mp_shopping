﻿using MpShopping.Web.Models.Data.Dto;

namespace MpShopping.Web.Models.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> FindAll();
        Task<ProductDTO> FindById(long id);
        Task<ProductDTO> Create(ProductDTO productDTO);
        Task<ProductDTO> Update(ProductDTO productDTO);
        Task<bool> Delete(long id);
    }
}
