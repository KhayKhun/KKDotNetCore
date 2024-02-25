using Contracts;
using KKDotNetCore.UserApiUsingRepositoryPattern.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.UserApiUsingRepositoryPattern.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly ILoggerManager _logger;

        public UserRepository(RepositoryContext repositoryContext, ILoggerManager logger) {
            _repositoryContext = repositoryContext;
            _logger = logger;
        }

        public async Task<int> CreateUser(User user)
        {
            await _repositoryContext.User!.AddAsync(user);
            int result = await _repositoryContext.SaveChangesAsync();

            string message = result > 0 ? "successful" : "failed";
            _logger.LogDebug($"CreateUser: user creating {message}");

            return result;
        }

        public async Task<int> DeleteUser(int id)
        {
            User? item = await _repositoryContext.User!.FirstOrDefaultAsync(x => x.UserId == id);

            if (item is null)
            {
                _logger.LogDebug($"UpdateUser: user with UserId={id} is null.");
                return 0;
            }
            _repositoryContext.Remove(item);
            int result = await _repositoryContext.SaveChangesAsync();

            string message = result > 0 ? "successful" : "failed";
            _logger.LogDebug($"DeleteUser: delete user with UserId={id} {message}.");

            return result;
        }

        public async Task<User> GetUser(int id)
        {
           User? user = await _repositoryContext.User!
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == id);

            _logger.LogDebug($"GetUser: return user with UserId={id}, is null = {user is null}.");

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            _logger.LogInfo("GetUsers: Returning user list.");

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
                _logger.LogDebug($"UpdateUser: user with UserId={id} is null.");
                return 0;
            }

            item.UserName = user.UserName;
            item.UserPhone = user.UserPhone;
            item.UserAddress = user.UserAddress;
            item.UserEmail = user.UserEmail;

            int result = await _repositoryContext.SaveChangesAsync();

            string message = result > 0 ? "successful" : "failed";
            _logger.LogDebug($"Updated: update user with UserId={id} {message}.");

            return result;
        }
    }
}
