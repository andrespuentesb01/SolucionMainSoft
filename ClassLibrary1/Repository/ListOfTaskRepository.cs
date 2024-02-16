using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SlnPactia.Infrastructure.DbContext;
using SlnPactia.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SlnPactia.Aplication.Repository
{
    public class ListOfTaskRepository
    {


        private readonly DbPactiaContext _dbcontext;

        public ListOfTaskRepository(DbPactiaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<ListOfTask> updateListOfTask(int id, string status)
            {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@IdTask",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = id
                        },
                        new SqlParameter() {
                            ParameterName = "@Status",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = status
                        }};
            //Execute the Sp and get the info of the distinct row affected
            var list = _dbcontext.ListOfTasks.FromSqlRaw("[dbo].[SpUpdateTask] @IdTask, @Status", param).ToList();
            return list;
        }

        public List<ListOfTask> createListOfTask(string nameOfTask)
        { 
            var param = new SqlParameter[] {

                        new SqlParameter() {
                            ParameterName = "@NameOfTask",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = nameOfTask
                        }};
            //Execute the Sp and get the info of the distinct row affected
            var list = _dbcontext.ListOfTasks.FromSqlRaw("[dbo].[SpCreateTask] @NameOfTask", param).ToList();




            return list;
        }

        public List<ListOfTask> getListOfTasks()
        {
            //Execute the entity framework function to list.
            var list = _dbcontext.ListOfTasks.ToList();
            return list;

        }

        public List<ListOfTask> deleteListOfTask(int id)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@IdTask",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = id
                        },
                    };
            //Execute the Sp and get the info of the distinct row affected
            var list = _dbcontext.ListOfTasks.FromSqlRaw("[dbo].[SpDeleteTask] @IdTask", param).ToList();
            return list;

        }
    }
}