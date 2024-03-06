using KKDotNetCore.RestApi.Feature.Log4net;

var builder = WebApplication.CreateBuilder(args);

var BlazorOrigin = "_blazorOrigin";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: BlazorOrigin,
    builder =>
    {
        builder.WithOrigins("https://localhost:7104", "http://localhost:5207");
    }
);

});
// Add services to the container.
builder.Services.AddLog4net();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(BlazorOrigin);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
