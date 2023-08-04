
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity?> GetUserByUserNameAsync(string username)
        {
            var receivedUser = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);

            return receivedUser;
        }
    }
}
