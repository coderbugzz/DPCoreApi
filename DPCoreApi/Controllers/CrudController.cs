using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DPCoreApi.Models;
using DPCoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly IDapperRepository _repository;

        public CrudController(IDapperRepository repository)
        {
            _repository = repository;
        }

        [HttpPost(nameof(Create))]
        public async Task<int> Create(member data)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("Id", data.Id, DbType.Int32);
            dp_params.Add("Name", data.Name, DbType.String);
            dp_params.Add("Address", data.Address, DbType.String);
            dp_params.Add("Contact", data.Contact, DbType.String);
            dp_params.Add("retVal", DbType.String,direction:ParameterDirection.Output);

            var result = await Task.FromResult(_repository.execute_sp<int>("[dbo].[sp_AddMember]"
                ,dp_params,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetMembers))]
        public async Task<List<member>> GetMembers()
        {
            var result = await Task.FromResult(_repository.GetAll<member>($"Select * from [members]", null, commandType: CommandType.Text));

            return result;
        }

        [HttpGet(nameof(GetMembersById))]
        public async Task<member> GetMembersById(int Id)
        {
            var result = await Task.FromResult(_repository.GetByID<member>($"Select * from [members] where Id = {Id}", null, commandType: CommandType.Text));

            return result;
        }

        [HttpPut(nameof(Update))]
        public async Task<int> Update(member data)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("Id", data.Id, DbType.Int32);
            dp_params.Add("Name", data.Name, DbType.String);
            dp_params.Add("Address", data.Address, DbType.String);
            dp_params.Add("Contact", data.Contact, DbType.String);
            dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);

            var result = await Task.FromResult(_repository.execute_sp<int>("[dbo].[sp_UpdateMember]"
                , dp_params,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("Id", Id, DbType.Int32);
            dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);

            var result = await Task.FromResult(_repository.execute_sp<int>("[dbo].[sp_DeleteMember]"
                , dp_params,
                commandType: CommandType.StoredProcedure));
            return result;
        }
    }
}
