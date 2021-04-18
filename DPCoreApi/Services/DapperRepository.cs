using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DPCoreApi.Services
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IConfiguration _configuration;

        public DapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<T> GetAll<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("default"));
            return db.Query<T>(query, sp_params, commandType: commandType).ToList();
        }

        public T GetByID<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("default"));
            return db.Query<T>(query, sp_params, commandType: commandType).FirstOrDefault();
        }

        public T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;

            using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("default"))) {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    dbConnection.Query<T>(query, sp_params, commandType: commandType, transaction: transaction);

                    result = sp_params.Get<T>("retVal"); //get output parameter value

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            };

                return result;
            }

       
    }
}
