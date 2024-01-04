using KKDotNetCore.ConsoleApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.ConsoleApp.RefitExamples
{
    public interface IUserApi
    {
        [Get("/api/user")]
        Task<List<UserDataModel>> GetUsers();

        [Get("/api/user/{id}")]
        Task<UserDataModel> GetUser(int id);

        [Get("/api/user/pegi")]
        Task<Object> PegiUser(int PageNo,int PageSize);

        [Post("/api/user")]
        Task<string> CreateUser(UserDataModel user);

        [Put("/api/user/{id}")]
        Task<string> UpdateUser(int id,UserDataModel user);

        [Patch("/api/user/{id}")]
        Task<string> PatchUser(int id, UserDataModel user);

        [Delete("/api/user/{id}")]
        Task<string> DeleteUser(int id);
    }
}
