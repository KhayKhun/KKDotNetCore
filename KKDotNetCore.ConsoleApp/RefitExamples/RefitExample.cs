using KKDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IUserApi _userApi = RestService.For<IUserApi>("http://localhost:5006");
        public async Task Run()
        {
            //await Read();
            //await Edit(2);
            //await Create(new UserDataModel()
            //{
            //    UserName = "Test",
            //    UserAddress = "Test",
            //    UserEmail = "Test",
            //    UserPhone = "Test",
            //});
            //await Update(1,new UserDataModel()
            //{
            //    UserName = "Test 1",
            //    UserAddress = "Test",
            //    UserEmail = "Test",
            //    UserPhone = "Test",
            //});
            //await Patch(2,new UserDataModel()
            //{
            //    UserPhone = "09090909",
            //});
            //await Delete(3);
            await Pegi(1, 2);
        } 

        private async Task Read()
        {
            var lst = await _userApi.GetUsers();
            foreach (UserDataModel item in lst)
            {
                Console.WriteLine($"User_Id => {item.UserId}");
                Console.WriteLine($"User_Name => {item.UserName}");
                Console.WriteLine($"User_Email => {item.UserEmail}");
                Console.WriteLine($"User_Phone => {item.UserPhone}");
                Console.WriteLine($"User_Address => {item.UserAddress}");
                Console.WriteLine("-------------------------");
            }
        }
        private async Task Edit(int id)
        {
            try {
                UserDataModel item = await _userApi.GetUser(id);
                Console.WriteLine($"User_Id => {item.UserId}");
                Console.WriteLine($"User_Name => {item.UserName}");
                Console.WriteLine($"User_Email => {item.UserEmail}");
                Console.WriteLine($"User_Phone => {item.UserPhone}");
                Console.WriteLine($"User_Address => {item.UserAddress}");
                Console.WriteLine("-------------------------");
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private async Task Create(UserDataModel user)
        {
            try {
                await _userApi.CreateUser(user);
                Console.WriteLine("insert success");
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private async Task Pegi(int PageNo,int PageSize)
        {
            try {
                var data = await _userApi.PegiUser(PageNo,PageSize);
                Console.WriteLine(data);
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private async Task Update(int id, UserDataModel user)
        {
            try {
                await _userApi.UpdateUser(id,user);
                Console.WriteLine("update success");
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private async Task Patch(int id, UserDataModel user)
        {
            try {
                await _userApi.PatchUser(id,user);
                Console.WriteLine("patch success");
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private async Task Delete(int id)
        {
            try {
                await _userApi.DeleteUser(id);
                Console.WriteLine("delete success");
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}
