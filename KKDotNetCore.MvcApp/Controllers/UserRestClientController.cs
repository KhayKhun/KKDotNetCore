using KKDotNetCore.ConsoleApp.RefitExamples;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Net.Http;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class UserRestClientController : Controller
    {
        private RestClient _restClient;
        public UserRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IActionResult> Index()
        {
            var lst = new List<UserDataModel>();

            RestRequest restRequest = new RestRequest("/api/user", Method.Get);
            var response = await _restClient.ExecuteAsync(restRequest);

            string jsonStr = response.Content!;
            lst = JsonConvert.DeserializeObject<List<UserDataModel>>(jsonStr);

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
            RestRequest restRequest = new RestRequest("/api/user", Method.Post);
            restRequest.AddBody(reqModel);
            
            var response = await _restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        public async Task<IActionResult> Edit(int id)
        {
            RestRequest restRequest = new RestRequest($"/api/user/{id}", Method.Get);

            var response = await _restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                UserDataModel item = JsonConvert.DeserializeObject<UserDataModel>(jsonStr)!;

                return View("Edit", item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UserDataModel reqModel)
        {
            RestRequest restRequest = new RestRequest($"/api/user/{id}", Method.Put);
            restRequest.AddBody(reqModel);

            var response = await _restClient.ExecuteAsync(restRequest);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            RestRequest restRequest = new RestRequest($"/api/user/{id}", Method.Delete);

            var response = await _restClient.ExecuteAsync(restRequest);

            return RedirectToAction("Index");
        }


    }
}
