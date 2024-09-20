using SlnMain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnMain.Aplication.Services
{
    public interface IOrderService
    {
        public Boolean CreateOrder(string jsonToParam);
        public List<OrderHeader> GetOrderHeaders();

        public List<OrderDetail> getOrderDetails(int id);

    }
}
