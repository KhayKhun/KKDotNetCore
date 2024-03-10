using KKDotNetCore.BlazorWasm.Shared;
using KKDotNetCore.Models;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace KKDotNetCore.BlazorWasm.Pages.User
{
    // "partial" makes connection
    public partial class PageUser
    {
        //[Inject]
        //public HttpClient HttpClient2 { get; set; }
        public int _pageNo = 1;
        public int _pageSize = 10;

        private UserResponseModel? Model;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await List(_pageNo, _pageSize);
            }
        }
        private async Task List(int pageNo,int pageSize = 10)
        {
            _pageNo = pageNo;
            _pageSize = pageSize;

            var response = await HttpClient.GetAsync($"api/user/pegi?pageNo={pageNo}&pageSize={pageSize}");
            //var response = await HttpClient.GetAsync("/");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                Model = JsonConvert.DeserializeObject<UserResponseModel>(jsonStr)!;
                // must call stateHasChanged in updating objects
                Console.WriteLine(Model.pageCount);
                StateHasChanged();
            }
            else
            {
                Console.WriteLine("bad");
            }
        }
        private async Task PageChanged(int i = 1)
        {
            await List(pageNo: i);
        }

        private async Task DeleteUser(int reqId)
        {
            var parameters = new DialogParameters<DialogBox>();
            parameters.Add(x => x.Message, "Are you sure to delete this user?");
            parameters.Add(x => x.ButtonText, "Delete");
            parameters.Add(x => x.Color, Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await DialogService.ShowAsync<DialogBox>("Confirm delete!", parameters, options);
            var result = await dialog.Result;

            if(result.Canceled) return;

            var jsonstr = JsonConvert.SerializeObject(reqId);
            HttpContent content = new StringContent(jsonstr, Encoding.UTF8, MediaTypeNames.Application.Json);

            var res = await HttpClient.DeleteAsync($"/api/user/{reqId}");
            if (res.IsSuccessStatusCode)
            {
                var message = await res.Content.ReadAsStringAsync();
                Snackbar.Add($"{message}", Severity.Error);
                Nav.NavigateTo("/setup/user");
                await List(_pageNo);
                StateHasChanged();
            }
        }
        private void NavigateEdit(int id)
        {
            Nav.NavigateTo($"setup/user/edit/{id}");
        }
    }
}
