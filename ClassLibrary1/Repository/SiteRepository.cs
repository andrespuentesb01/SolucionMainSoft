using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using SlnMain.Infrastructure.DbContext;
using SlnMain.Domain;
using SlnMain.Domain.Models;
using SlnMain.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SlnMain.Aplication.Repository
{
    public class SiteRepository
    {


        private readonly DbRentCarContext _dbcontext;
        private readonly IConfiguration configuration;

        public SiteRepository(DbRentCarContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            this.configuration = configuration;
        }
        public List<Site> CreateSite(int idCar, int idDelivery, int idCollect)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@idCar",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = idCar
                        },
                         new SqlParameter() {
                            ParameterName = "@idDelivery",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = idDelivery
                        },
                        new SqlParameter() {
                            ParameterName = "@idCollect",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = idCollect
                        }
                    };
            //Execute the Sp and get the info of the distinct row affected
            var listSite = _dbcontext.Sites.FromSqlRaw("[dbo].[spCreateSiteCar] @IdCar, @idDelivery, @idCollect ", param).ToList();
            return listSite;
        }

        public List<SiteDetails> GetSiteDetails()
        {
            var _connectionString = configuration.GetConnectionString("CadenaSQL");
            //Execute the sp function to list.
            var param = new SqlParameter[] { };
            //Execute the Sp and get the info of the distinct row affected
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[spFindSitesDetails]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<SiteDetails>();
                    sql.OpenAsync();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.Add(MapToValue(reader));
                        }
                        return response;
                    }


                }
            }

        }
        public SiteDetails MapToValue(SqlDataReader reader)
        {
            //map data in details rent
            return new SiteDetails()
            {
                cityCollect = reader["cityCollect"].ToString(),
                model = reader["model"].ToString(),        
                siteCollect = reader["siteCollect"].ToString(),
                plate = reader["plate"].ToString(),
                branch = reader["branch"].ToString(),
                siteDelivery = reader["siteDelivery"].ToString(),
                cityDelivery = reader["cityDelivery"].ToString(),

            };
        }
    }
}