using Core.DTOs;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IUserInfosRepository
    {
        public Task<List<PeapleInfoDTO>> GetAllPeapleInfo();
    }
    public class UserInfosRepository : IUserInfosRepository
    {
        private IConfiguration _configuration;
        private readonly string _conectionString;

        public UserInfosRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conectionString = _configuration["Connection"];
        }

        public async Task<List<PeapleInfoDTO>> GetAllPeapleInfo()
        {
            var conection = new SqlConnection(_conectionString);
            await conection.OpenAsync();
            List<PeapleInfoDTO> Output = new List<PeapleInfoDTO>();
            string Sql = "USP_GetAllUsers";
            var users = await conection.QueryAsync<PeapleInfoDTO>(Sql, commandType: CommandType.StoredProcedure);
            foreach (var user in users)
            {
                Output.Add(user);
            }
            return Output;
        }
    }
}
