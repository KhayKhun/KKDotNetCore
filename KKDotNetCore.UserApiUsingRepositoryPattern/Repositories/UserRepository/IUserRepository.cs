using KKDotNetCore.UserApiUsingRepositoryPattern.Entities.Models;

namespace KKDotNetCore.UserApiUsingRepositoryPattern.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<int> CreateUser(User user);
        Task<int> UpdateUser(int id, User user);
        Task<int> DeleteUser(int id);
    }
}
