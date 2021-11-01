using Common.DTO;
using System.Threading.Tasks;

namespace Common.RepositoryInterfaces
{
    public interface IUserAccessRepository
    {
        public Task<bool> ValidateUser(UserLoginDTO loginDTO);
    }
}
