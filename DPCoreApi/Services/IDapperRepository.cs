using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DPCoreApi.Services
{
    public interface IDapperRepository
    {
        T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        T GetByID<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
    }
}
