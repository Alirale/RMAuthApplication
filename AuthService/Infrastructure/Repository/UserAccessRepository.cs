using Common.DTO;
using Common.RepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure
{

    public class UserAccessRepository : IUserAccessRepository
    {
        private IConfiguration _configuration;
        private readonly string _conectionString;

        public UserAccessRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conectionString = _configuration["Connection"];
        }

        public async Task<bool> ValidateUser(UserLoginDTO loginDTO)
        {
            var conection = new SqlConnection(_conectionString);
            await conection.OpenAsync();

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@UserName", loginDTO.UserName);
            queryParameters.Add("@HashedPassword", loginDTO.HashedPassword);

            string Sql = "USP_FindUser";

            var user = await conection.QueryAsync<UserLoginDTO>(sql: Sql, param: queryParameters, commandType: CommandType.StoredProcedure);
            return true && user != null;
        }
    }
}
