using KKDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KKDotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        private string _endpoint = "http://localhost:5006/api/Users";

        public async void Run()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await CreateAsync(new UserDataModel()
            //{
            //    User_Age = 99,
            //    User_Name = "name",
            //});
            //await UpdateAsync(1,new UserDataModel()
            //{
            //    User_Age = 99,
            //    User_Name = "name updated",
            //});
            await PatchAsync(2, new UserDataModel()
            {
                User_Name = "nn name patched"
            });
            //await DeleteAsync(1);
        }

        private async Task ReadAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(_endpoint);
            Console.WriteLine(res.StatusCode);
            if (res.IsSuccessStatusCode)
            {
                string jsonStr = await res.Content.ReadAsStringAsync();
                List<UserDataModel> lst = JsonConvert.DeserializeObject<List<UserDataModel>>(jsonStr)!;
                foreach (UserDataModel item in lst)
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
            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync($"{_endpoint}/{id}");
            Console.WriteLine(res.StatusCode);
            if (res.IsSuccessStatusCode)
            {
                string jsonStr = await res.Content.ReadAsStringAsync();
                UserDataModel item = JsonConvert.DeserializeObject<UserDataModel>(jsonStr)!;
                    Console.WriteLine($"User_Id => {item.User_Id}");
                    Console.WriteLine($"User_Name => {item.User_Name}");
                    Console.WriteLine($"User_Age => {item.User_Age}");
                    Console.WriteLine("-------------------------");
            }

        }

        private async Task CreateAsync(UserDataModel user)
        {
            HttpClient client = new HttpClient();

            string jsonString = JsonConvert.SerializeObject(user);
            HttpContent httpContent = new StringContent(jsonString,Encoding.UTF8,Application.Json);
            HttpResponseMessage res = await client.PostAsync(_endpoint,httpContent);
            Console.WriteLine(await res.Content.ReadAsStringAsync());

        }

        private async Task UpdateAsync(int id,UserDataModel user)
        {
            HttpClient client = new HttpClient();

            string jsonString = JsonConvert.SerializeObject(user);
            HttpContent httpContent = new StringContent(jsonString,Encoding.UTF8,Application.Json);
            HttpResponseMessage res = await client.PutAsync($"{_endpoint}/{id}",httpContent);
            Console.WriteLine(await res.Content.ReadAsStringAsync());

        }
        private async Task PatchAsync(int id,UserDataModel user)
        {
            HttpClient client = new HttpClient();

            string jsonString = JsonConvert.SerializeObject(user);
            HttpContent httpContent = new StringContent(jsonString,Encoding.UTF8,Application.Json);
            HttpResponseMessage res = await client.PatchAsync($"{_endpoint}/{id}",httpContent);
            Console.WriteLine(await res.Content.ReadAsStringAsync());

        }

        private async Task DeleteAsync(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage res = await client.DeleteAsync($"{_endpoint}/{id}");
            Console.WriteLine(await res.Content.ReadAsStringAsync());

        }
    }
}
