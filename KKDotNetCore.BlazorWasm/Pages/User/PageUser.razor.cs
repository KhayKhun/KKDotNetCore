using KKDotNetCore.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using static MudBlazor.CategoryTypes;

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
    }
}
