using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MpShopping.Web.Models.Data.Dto;
using MpShopping.Web.Models.Interfaces;
using MpShopping.ProductAPI.Model.Context;

namespace MpShopping.ProductAPI.Model.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IMapper _mapper;
        private readonly MySQLContext _context;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> FindAll()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> FindById(long id)
        {
            var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> Create(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();

                if (product.Id <= 0) return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;                
            }
            catch
            {
                return false;
            }
        }

    }
}
