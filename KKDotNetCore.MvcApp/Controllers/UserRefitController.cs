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
    public class UserRefitController : Controller
    {
        private IUserApi _userApi;
        public UserRefitController(IUserApi userApi)
        {
            _userApi = userApi;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _userApi.GetUsers();

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
            var response = await _userApi.CreateUser(reqModel);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _userApi.GetUser(id);

            if (response is null) return RedirectToAction("Index");
                
            return View("Edit", response);
            
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UserDataModel reqModel)
        {
            var response = await _userApi.UpdateUser(id, reqModel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userApi.DeleteUser(id);

            return RedirectToAction("Index");

        }


    }
}
