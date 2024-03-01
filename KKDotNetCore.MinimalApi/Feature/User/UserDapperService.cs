using KKDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace KKDotNetCore.MinimalApi.Feature.User
{
    public static class UserDapperService
    {
        public static IEndpointRouteBuilder UseUserDapperService(this IEndpointRouteBuilder app)
        {

            app.MapGet("/api/user/", ([FromServicesAttribute] DapperService _service) =>
            {
                string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User]
";
                List<UserOldDataModel> lst = _service.Query<UserOldDataModel>(query);

                return Results.Ok(lst);
            })
            .WithName("GetUsers")
            .WithOpenApi();

            app.MapGet("/api/user/{reqId}", ([FromServicesAttribute] DapperService _service, int reqId) =>
            {
                string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User] WHERE User_Id = @User_Id
";
                UserOldDataModel user = _service.Query<UserOldDataModel>(query, param: new UserOldDataModel { User_Id = reqId }).FirstOrDefault();

                if (user is null)
                {
                    Log.Information("/api/user/{reqId}: user not found. Response status 404.");
                    return Results.BadRequest();
                }

                Log.Information("/api/user/{reqId}: user found. Response status 200.");
                return Results.Ok(user);
            })
            .WithName("GetUser")
            .WithOpenApi();

            app.MapPost("/api/user/", ([FromServicesAttribute] DapperService _service, [FromBody] UserOldDataModel reqModel) =>
            {
                string query = @"
INSERT INTO [dbo].[Tbl_User]
           ([User_Name]
           ,[User_Age])
     VALUES
           (@User_Name
           ,@User_Age)";

                int result = _service.Execute(query, param: new UserOldDataModel { User_Name = reqModel.User_Name, User_Age = reqModel.User_Age });

                string message = result > 0 ? "Saved user." : "Failed to save";

                Log.Information("/api/user/: Response on create user");
                return Results.Ok(message);
            })
            .WithName("CreateUser")
            .WithOpenApi();

            app.MapPut("/api/user/{reqId}", ([FromServicesAttribute] DapperService _service, int reqId, [FromBody] UserOldDataModel reqModel) =>
            {
                string query = @"UPDATE [dbo].[Tbl_User]
   SET [User_Name] = @User_Name
      ,[User_Age] = @User_Age
 WHERE User_Id = @User_Id";

                int result = _service.Execute(query, param: new UserOldDataModel { User_Id = reqModel.User_Id, User_Name = reqModel.User_Name, User_Age = reqModel.User_Age });

                string message = result > 0 ? "Updated user." : "Failed to update";

                Log.Information("/api/user/{reqId}: Response on update user");

                return Results.Ok(message);
            })
            .WithName("PutUser")
            .WithOpenApi();

            app.MapDelete("/api/user/{reqId}", ([FromServicesAttribute] DapperService _service, int reqId) =>
            {

                string query = @"DELETE FROM [dbo].[Tbl_User]
      WHERE User_id = @User_Id";

                int result = _service.Execute(query, param: new UserOldDataModel { User_Id = reqId });

                string message = result > 0 ? "Deleted user." : "Failed to delete";

                Log.Information("/api/user/{reqId}: Response on delete user");

                return Results.Ok(message);
            })
            .WithName("DeleteUser")
            .WithOpenApi();

            return app;
        }
    }
}
