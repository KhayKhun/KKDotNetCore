using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace KKDotNetCore.MinimalApi.Feature.User
{
    public static class UserService
    {
        public static IEndpointRouteBuilder UseUserService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/user/{pageNo}/{pageSize}", async ([FromServicesAttribute] AppDbContext dbContext, int pageNo, int PageSize) =>
            {
                Log.Information($"/api/user/{{pageNo}}/{{pageSize}}: Response users pageNo={pageNo}, pageSize={PageSize}");
                return await dbContext.User
                .AsNoTracking()
                .OrderByDescending(u => u.UserId)
                .Skip((pageNo - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            })
.WithName("GetUsers")
.WithOpenApi();

            app.MapGet("/api/user/{reqId}", async ([FromServicesAttribute] AppDbContext dbContext, int reqId) =>
            {
                var user = await dbContext.User
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == reqId);

                if (user is null)
                {
                    Log.Information("/api/user/{reqId}: No user found");
                    return Results.NotFound();
                }

                Log.Information("/api/user/{reqId}: user found. Response status 200.");

                return Results.Ok(user);


            })
            .WithName("GetUser")
            .WithOpenApi();

            app.MapPost("/api/user/", async ([FromServicesAttribute] AppDbContext dbContext, [FromBody] UserDataModel reqModel) =>
            {
                await dbContext.User.AddAsync(reqModel);
                int result = await dbContext.SaveChangesAsync();

                string message = result > 0 ? "Saved user." : "Failed to save";

                Log.Information("/api/user/: Response on create user");
                return Results.Ok(message);
            })
            .WithName("CreateUser")
            .WithOpenApi();
            
            app.MapPut("/api/user/{reqId}", async ([FromServicesAttribute] AppDbContext dbContext,int reqId, [FromBody] UserDataModel reqModel) =>
            {
                UserDataModel? item = await dbContext.User.FirstOrDefaultAsync(x => x.UserId == reqId);

                if (item is null) return Results.NotFound();

                item.UserName = reqModel.UserName;
                item.UserPhone = reqModel.UserPhone;
                item.UserAddress = reqModel.UserAddress;
                item.UserEmail = reqModel.UserEmail;

                int result = await dbContext.SaveChangesAsync();

                string message = result > 0 ? "Updated user." : "Failed to update";

                Log.Information("/api/user/{reqId}: Response on update user");

                return Results.Ok(message);
            })
            .WithName("PutUser")
            .WithOpenApi();
            
            app.MapPatch("/api/user/{reqId}", async ([FromServicesAttribute] AppDbContext dbContext,int reqId, [FromBody] UserDataModel reqModel) =>
            {
                UserDataModel? item = await dbContext.User.FirstOrDefaultAsync(x => x.UserId == reqId);

                if (item is null) return Results.NotFound();

                if (!string.IsNullOrEmpty(reqModel.UserPhone))
                {
                    item.UserPhone = reqModel.UserPhone;
                }
                if (!string.IsNullOrEmpty(reqModel.UserName))
                {
                    item.UserName = reqModel.UserName;
                }
                if (!string.IsNullOrEmpty(reqModel.UserAddress))
                {
                    item.UserAddress = reqModel.UserAddress;
                }
                if (!string.IsNullOrEmpty(reqModel.UserEmail))
                {
                    item.UserEmail = reqModel.UserEmail;
                }

                int result = await dbContext.SaveChangesAsync();

                string message = result > 0 ? "Updated user." : "Failed to update";

                Log.Information("/api/user/{reqId}: Response on patch user");

                return Results.Ok(message);
            })
            .WithName("PatchUser")
            .WithOpenApi();
            
            app.MapDelete("/api/user/{reqId}", async ([FromServicesAttribute] AppDbContext dbContext,int reqId) =>
            {
                UserDataModel? item = await dbContext.User.FirstOrDefaultAsync(x => x.UserId == reqId);

                if (item is null) return Results.NotFound();

                dbContext.Remove(item);

                int result = await dbContext.SaveChangesAsync();

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
