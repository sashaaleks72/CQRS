
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        public Task<UserEntity?> GetUserByUserNameAsync(string username);
    }
}
