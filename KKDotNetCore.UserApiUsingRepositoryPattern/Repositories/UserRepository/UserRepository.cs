using KKDotNetCore.UserApiUsingRepositoryPattern.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.UserApiUsingRepositoryPattern.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public UserRepository(RepositoryContext repositoryContext) {
            _repositoryContext = repositoryContext;
        }

        public async Task<int> CreateUser(User user)
        {
            await _repositoryContext.User!.AddAsync(user);
            int result = await _repositoryContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteUser(int id)
        {
            User? item = await _repositoryContext.User!.FirstOrDefaultAsync(x => x.UserId == id);

            if (item is null)
            {
                return 0;
            }
            _repositoryContext.Remove(item);
            int result = await _repositoryContext.SaveChangesAsync();

            return result;
        }

        public async Task<User> GetUser(int id)
        {
           User? user = await _repositoryContext.User!
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repositoryContext.User!
                .AsNoTracking()
                .OrderByDescending(x => x.UserId)
                .ToListAsync();
        }

        public async Task<int> UpdateUser(int id, User user)
        {
            User? item = await _repositoryContext.User
                .FirstOrDefaultAsync(x => x.UserId == id);
            if (item is null)
            {
                return 0;
            }

            item.UserName = user.UserName;
            item.UserPhone = user.UserPhone;
            item.UserAddress = user.UserAddress;
            item.UserEmail = user.UserEmail;

            int result = await _repositoryContext.SaveChangesAsync();

            return result;
        }
    }
}
