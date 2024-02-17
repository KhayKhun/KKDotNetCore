using KKDotNetCore.ConsoleApp.RefitExamples;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class UserHttpClientController : Controller
    {
        private HttpClient _httpClient;
        public UserHttpClientController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/user");
            var lst = new List<UserDataModel>();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<UserDataModel>>(jsonString);
            }

            Console.WriteLine(lst is null);
            Console.WriteLine(lst);
            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserDataModel reqModel)
        {
            string jsonString = JsonConvert.SerializeObject(reqModel);
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, Application.Json);

            HttpResponseMessage res = await _httpClient.PostAsync("/api/user", httpContent);
            Console.WriteLine(await res.Content.ReadAsStringAsync());

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"/api/user/{id}");
            Console.WriteLine(res.StatusCode);
            if (res.IsSuccessStatusCode)
            {
                string jsonStr = await res.Content.ReadAsStringAsync();
                UserDataModel item = JsonConvert.DeserializeObject<UserDataModel>(jsonStr)!;

                return View("Edit", item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UserDataModel reqModel)
        {
            Console.WriteLine(reqModel);
            Console.WriteLine($"reqId => {id}");
            string jsonString = JsonConvert.SerializeObject(reqModel);
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, Application.Json);
            HttpResponseMessage res = await _httpClient.PutAsync($"/api/user/{id}", httpContent);
            Console.WriteLine(await res.Content.ReadAsStringAsync());
            Console.WriteLine(res.StatusCode);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine($"reqId => {id}");
            HttpResponseMessage res = await _httpClient.DeleteAsync($"/api/user/{id}");
            Console.WriteLine(res.StatusCode);
            return RedirectToAction("Index");
        }


    }
}
