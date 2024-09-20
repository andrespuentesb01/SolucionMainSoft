using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using SlnMain.Infrastructure.DbContext;
using SlnMain.Domain;
using SlnMain.Domain.Models;
using SlnMain.Infrastructure;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SlnMain.Aplication.Services
{
    public class OrderService: IOrderService
    {

        private readonly DbCarvajalContext _dbcontext;

        public OrderService(DbCarvajalContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public List<OrderHeader> GetOrderHeaders()
        {
            var listOfHeader = _dbcontext.OrderHeaders.ToList();
            return listOfHeader;

        }
        public List<OrderDetail> getOrderDetails(int id)
        {
            //Execute the entity framework function to list.
            var listOfDetails = _dbcontext.OrderDetails.Where(c => c.IdHeader == id).ToList();
            return listOfDetails;

        }
        public  Boolean CreateOrder(string jsonToParam)
        {
            //Create a task automatically assigning it to status PENDIENTE
            var param = new SqlParameter[] {               
                        new SqlParameter() {
                            ParameterName = "@json",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 2000,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = jsonToParam
                        }
             };
                     //Execute the Sp and get the info of the distinct row affected
            var listRent = _dbcontext.OrderHeaders.FromSqlRaw("[dbo].[spCreateOrder] @json", param).ToList();
            return  true;
        }

       /* public RentDetails MapToValue(SqlDataReader reader)
        {
            var a = reader["cc"].ToString();
            return new RentDetails()
            {
                cc = reader["cc"].ToString(),
                collect = reader["collect"].ToString(),
                model = reader["model"].ToString(),
                delivery = reader["delivery"].ToString(),
                lastName = reader["lastName"].ToString(),
                name = reader["name"].ToString(),
                nameStatus = reader["nameStatus"].ToString(),
                plate = reader["plate"].ToString(),
                branch = reader["branch"].ToString(),

            };
        }*/

            }
}