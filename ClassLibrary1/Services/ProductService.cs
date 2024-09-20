using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

//using SlnMain.Infrastructure.DbContext;
using SlnMain.Domain;
using SlnMain.Domain.Models;
using SlnMain.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SlnMain.Aplication.Services
{
    public class ProductService: IProductService
    {
      
        private readonly DbCarvajalContext _dbcontext;

        public ProductService(DbCarvajalContext dbcontext)
        {
            _dbcontext = dbcontext;
        }



        public async void UpdateProduct(string description, decimal price, int ivaPercent, int stock)
        {
            // insert product entity framework
          
                var product = _dbcontext.Set<Product>();
                product.Add(new Product { Description = description, Price = price, IvaPercent = ivaPercent, Stock = stock });
                _dbcontext.SaveChanges();
          
        }

        public async void CreateProduct( string description, decimal price, int ivaPercent, int stock)
        {
            // insert product entity framework
          
                var product = _dbcontext.Set<Product>();
                product.Add(new Product { Description = description, Price = price, IvaPercent = ivaPercent, Stock = stock });
                _dbcontext.SaveChanges();       
            
        }

        public IEnumerable<Product> GetProducts()
        {
            var listOfProducts = _dbcontext.Products.ToList();
            return listOfProducts;
        }

        public Product Get(int id)
        {
            var product = _dbcontext.Products.FirstOrDefault(d => d.Id == id);
            return product;
        }

    }
}