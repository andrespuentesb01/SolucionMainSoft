using SlnMain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnMain.Aplication.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public Product Get(int id);

        public void UpdateProduct(string description, decimal price, int ivaPercent, int stock);

        public void CreateProduct(string description, decimal price, int ivaPercent, int stock);
    }
}
