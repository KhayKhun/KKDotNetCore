using KKDotNetCore.Models;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace KKDotNetCore.BlazorWasm.Pages.User
{
    public partial class PageUserCreate
    {
        private UserDataModel _reqModel = new UserDataModel();

        private async Task Save()
        {
            var jsonstr = JsonConvert.SerializeObject(_reqModel);
            HttpContent content = new StringContent(jsonstr, Encoding.UTF8, MediaTypeNames.Application.Json);

            var res = await HttpClient.PostAsync("/api/user", content);
            if(res.IsSuccessStatusCode)
            {
                var message = await res.Content.ReadAsStringAsync();
                Snackbar.Add($"{message}", Severity.Success);
                Nav.NavigateTo("/setup/user");
            }
        }
    }
}
