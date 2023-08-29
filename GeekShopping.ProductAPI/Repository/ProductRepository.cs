using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Attributes
        private readonly MySqlContext _context;
        private IMapper _mapper;
        #endregion

        #region ctor
        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper =  mapper;
        }
        #endregion

        #region Metódos
        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Product? product = 
                await _context.Products.Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(vo);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Product? product =
                    await _context.Products.Where(p => p.Id == id)
                        .FirstOrDefaultAsync();
                if (product == null) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
