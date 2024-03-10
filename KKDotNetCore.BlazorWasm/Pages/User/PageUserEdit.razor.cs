using KKDotNetCore.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace KKDotNetCore.BlazorWasm.Pages.User
{
    public partial class PageUserEdit
    {
        [Parameter]
        public int id {  get; set; }

        UserDataModel? model;

        protected override async Task OnInitializedAsync()
        {
            var res = await HttpClient.GetAsync($"api/user/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonstr = await res.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<UserDataModel>(jsonstr)!;
            }

        }

        private async Task Update()
        {
            var jsonstr = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(jsonstr, Encoding.UTF8, MediaTypeNames.Application.Json);

            var res = await HttpClient.PutAsync($"/api/user/{id}", content);
            if (res.IsSuccessStatusCode)
            {
                var message = await res.Content.ReadAsStringAsync();
                Snackbar.Add($"{message}", Severity.Success);
                Nav.NavigateTo("/setup/user");
            }

        }
    }
}
