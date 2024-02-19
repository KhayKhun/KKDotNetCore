using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKDotNetCore.MvcApp
{
    [Table("Tbl_Users")]
    public class UserDataModel
    {
        [Key]
        public int UserId { get; set; }
        public String? UserName { get; set; }
        public String? UserEmail { get; set; }
        public String? UserPhone { get; set; }
        public String? UserAddress { get; set; }
    }
    public class UserResponseModel
    {
        public PageSettingModel PageSetting {  get; set; }
        public List<UserDataModel> Data { get; set; }
    }

    public class PageSettingModel
    {
        public PageSettingModel() { }

        public PageSettingModel(int pageNo, int pageSize, int pageCount, int pageRowCount) {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
            PageRowCount = pageRowCount;
        }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRowCount { get; set; }
    }

}
