using KKDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace KKDotNetCore.MinimalApi.Feature.User
{
    public static class UserAdoDotNetService
    {
        public static IEndpointRouteBuilder UseUserAdoDotNetService(this IEndpointRouteBuilder app)
        {

            app.MapGet("/api/user/", ([FromServicesAttribute] AdoDotNetService _service) =>
            {
                string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User]";

                var lst = _service.Query<UserOldDataModel>(query);

                return Results.Ok(lst);
            })
            .WithName("GetUsers")
            .WithOpenApi();

            app.MapGet("/api/user/{reqId}", ([FromServicesAttribute] AdoDotNetService _service, int reqId) =>
            {
                string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User] WHERE [User_Id] = @id
";
                List<SqlParameter> lstParam = new List<SqlParameter>() {
                new("@id",reqId)
            };

                var user = _service.Query<UserOldDataModel>(query, sqlParameters: lstParam.ToArray()).FirstOrDefault();

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

            app.MapPost("/api/user/", ([FromServicesAttribute] AdoDotNetService _service, [FromBody] UserOldDataModel reqModel) =>
            {
                string query = @"
INSERT INTO [dbo].[Tbl_User]
           ([User_Name]
           ,[User_Age])
     VALUES
           (@User_Name
           ,@User_Age)";

                List<SqlParameter> lstParam = new List<SqlParameter>() {
                new("@User_Name",reqModel.User_Name),
                new("@User_Age",reqModel.User_Age)
            };

                var result = _service.Execute(query, sqlParameters: lstParam.ToArray());

                string message = result > 0 ? "Saved user." : "Failed to save";

                Log.Information("/api/user/: Response on create user");
                return Results.Ok(message);
            })
            .WithName("CreateUser")
            .WithOpenApi();

            app.MapPut("/api/user/{reqId}", ([FromServicesAttribute] AdoDotNetService _service, int reqId, [FromBody] UserOldDataModel reqModel) =>
            {
                string query = @"UPDATE [dbo].[Tbl_User]
   SET [User_Name] = @User_Name
      ,[User_Age] = @User_Age
 WHERE User_Id = @User_Id";


                List<SqlParameter> lstParam = new List<SqlParameter>() {
                new("@User_Name",reqModel.User_Name),
                new("@User_Id",reqModel.User_Id),
                new("@User_Age",reqModel.User_Age)
            };

                var result = _service.Execute(query, sqlParameters: lstParam.ToArray());

                string message = result > 0 ? "Updated user." : "Failed to update";

                Log.Information("/api/user/{reqId}: Response on update user");

                return Results.Ok(message);
            })
            .WithName("PutUser")
            .WithOpenApi();

            app.MapDelete("/api/user/{reqId}", ([FromServicesAttribute] AdoDotNetService _service, int reqId) =>
            {
                string query = @"DELETE FROM [dbo].[Tbl_User]
      WHERE User_id = @User_Id";

                List<SqlParameter> lstParam = new List<SqlParameter>() {
                new("@User_Id",reqId),
            };

                var result = _service.Execute(query, sqlParameters: lstParam.ToArray());

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
