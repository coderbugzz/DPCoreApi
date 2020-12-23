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
        T execute_sp<T>(string sp, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);

    }
}
