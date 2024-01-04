using KKDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KKDotNetCore.ConsoleApp.RestSharpExamples
{
    public class RestSharpExample
    {
        private string _endpoint = "http://localhost:5006/api/Users";

        public async void Run()
        {
            //await ReadAsync();
            //await EditAsync(2);
            //await CreateAsync(new UserDataModel()
            //{
            //    User_Age = 12,
            //    User_Name = "Created with RestSharp",
            //});
            //await UpdateAsync(2, new UserDataModel()
            //{
            //    User_Age = 18,
            //    User_Name = "Nang Nang",
            //});
            await PatchAsync(2, new UserOldDataModel()
            {
                User_Name = "Nang Nang patched",
            });
            //await DeleteAsync(5);
        }

        private async Task ReadAsync()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_endpoint,Method.Get);

            var res = await client.ExecuteAsync(request);

            if (res.IsSuccessStatusCode)
            {
                string jsonStr = res.Content!;
                List<UserOldDataModel> lst = JsonConvert.DeserializeObject<List<UserOldDataModel>>(jsonStr)!;
                foreach (UserOldDataModel item in lst)
                {
                    Console.WriteLine($"User_Id => {item.User_Id}");
                    Console.WriteLine($"User_Name => {item.User_Name}");
                    Console.WriteLine($"User_Age => {item.User_Age}");
                    Console.WriteLine("-------------------------");
                }
            }

        }

        private async Task EditAsync(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_endpoint}/{id}", Method.Get);

            var res = await client.ExecuteAsync(request);
            if (res.IsSuccessStatusCode)
            {
                string jsonStr = res.Content!;
                UserOldDataModel item = JsonConvert.DeserializeObject<UserOldDataModel>(jsonStr)!;
                Console.WriteLine($"User_Id => {item.User_Id}");
                Console.WriteLine($"User_Name => {item.User_Name}");
                Console.WriteLine($"User_Age => {item.User_Age}");
                Console.WriteLine("-------------------------");
            }

        }

        private async Task CreateAsync(UserOldDataModel user)
        {

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_endpoint, Method.Post);
            request.AddJsonBody(user);
            var res = await client.ExecuteAsync(request);

            Console.WriteLine(res.Content);

        }

        private async Task UpdateAsync(int id, UserOldDataModel user)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_endpoint}/{id}", Method.Put);
            request.AddJsonBody(user);
            var res = await client.ExecuteAsync(request);

            Console.WriteLine(res.Content);

        }

        private async Task PatchAsync(int id, UserOldDataModel user)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_endpoint}/{id}", Method.Patch);
            request.AddJsonBody(user);
            var res = await client.ExecuteAsync(request);

            Console.WriteLine(res.Content);

        }

        private async Task DeleteAsync(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_endpoint}/{id}", Method.Delete);
            var res = await client.ExecuteAsync(request);

            Console.WriteLine(res.Content);
        }
    }
}
