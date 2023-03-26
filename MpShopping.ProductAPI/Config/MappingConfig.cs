using AutoMapper;
using MpShopping.ProductAPI.Model;
using MpShopping.Web.Models.Data.Dto;

namespace MpShopping.ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>();
                config.CreateMap<Product, ProductDTO>();
            });
            return mappingConfig;
        }
    }
}
