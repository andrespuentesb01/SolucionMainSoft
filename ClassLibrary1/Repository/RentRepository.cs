using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using SlnMain.Infrastructure.DbContext;
using SlnMain.Domain;
using SlnMain.Domain.Models;
using SlnMain.Infrastructure;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SlnMain.Aplication.Repository
{
    public class RentRepository
    {

        private readonly DbRentCarContext _dbcontext;
        private readonly IConfiguration configuration;
        public RentRepository(DbRentCarContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            this.configuration = configuration;
        }
        public List<Rent> CreateRentDisponibility(int idCar, int idUser, string comments)
        {
            //Create a task automatically assigning it to status PENDIENTE
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@idCar",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = idCar
                        },
                         new SqlParameter() {
                            ParameterName = "@idUser",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = idUser
                        },
                        new SqlParameter() {
                            ParameterName = "@comments",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = comments
                        }
             };
                     //Execute the Sp and get the info of the distinct row affected
            var listRent = _dbcontext.Rents.FromSqlRaw("[dbo].[spCreateRent] @IdCar, @idUser, @comments ", param).ToList();
            return listRent;
        }

        public RentDetails MapToValue(SqlDataReader reader)
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
        }

        public  List<RentDetails> GetRentDetails()
        {
            var _connectionString = configuration.GetConnectionString("CadenaSQL");
            //Execute the sp function to list.
            var param = new SqlParameter[] {};
            //Execute the Sp and get the info of the distinct row affected
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[spGetRentsDetails]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<RentDetails>();
                    sql.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.Add(MapToValue(reader));
                        }
                        return  response;
                    }


                }
            }
        }
    }
}