namespace KKDotNetCore.RestApi.Models
{
    public class UserResponseModel
    {
        public bool IsEndOfPage {get; set;}
        public int pageCount { get; set; }
        public int rowCount { get; set; }
        public int pageSize { get; set; }
        public int pageNo { get; set; }
        public List<UserDataModel> data { get; set; }
}
}
