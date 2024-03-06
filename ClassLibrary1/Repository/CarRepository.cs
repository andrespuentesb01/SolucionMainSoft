using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
//using SlnMain.Infrastructure.DbContext;
using SlnMain.Domain;
using SlnMain.Domain.Models;
using SlnMain.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SlnMain.Aplication.Repository
{
    public class CarRepository
    {




        private readonly DbRentCarContext _dbcontext;

        public CarRepository(DbRentCarContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void createCar(string plate, string branch, string year, string model)
        {
            //Execute car in entity framework function to list.
            using (_dbcontext)
            {
                var car = _dbcontext.Set<Car>();
                car.Add(new Car { Branch = branch, Model = model, Year = year, Plate = plate });

                _dbcontext.SaveChanges();
            }
        }

        public List<Car> getCarsFilter(int idDelivery, int idCollect)
        {
            //Execute the entity framework function to list.
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@idDelivery",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = idDelivery
                        },
                         new SqlParameter() {
                            ParameterName = "@idCollect",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = idCollect
                        }
                    };
            //Execute the Sp and get the info of the distinct row affected
            var listCars = _dbcontext.Cars.FromSqlRaw("[dbo].[spFindCarsWithSite] @idDelivery, @idCollect ", param).ToList();
            return listCars;
        }

        public List<Car> getCarsNewSite()
        {
            var param = new SqlParameter[] {};
            //Execute the Sp and get the info of the distinct row affected
            var listCars = _dbcontext.Cars.FromSqlRaw("[dbo].[spFindNewCars] ", param).ToList();
            return listCars;
        }
    }
}