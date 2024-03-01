using KKDotNetCore.ConsoleApp.AdoDotNetExamples;
using KKDotNetCore.ConsoleApp.DapperExamples;
using KKDotNetCore.ConsoleApp.EFCoreExamples;
using KKDotNetCore.ConsoleApp.HttpClientExamples;
using KKDotNetCore.ConsoleApp.RefitExamples;
using KKDotNetCore.ConsoleApp.RestSharpExamples;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Data.SqlClient;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Hour)
            .WriteTo.MSSqlServer(
        connectionString: "Server=.;Database=UserDb;User ID=sa;Password=sa@123456;TrustServerCertificate=True;",
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Tbl_Log", AutoCreateSqlDatabase = true })
            .CreateLogger();

Log.Information("Hello World!");
//AdoDotNetExample2 ado = new AdoDotNetExample2();
//ado.Run();

DapperExample2 dpe = new DapperExample2();
dpe.Run();

//EFCoreExample efc = new EFCoreExample();
//efc.Run();

//HttpClientExample httpClientExample = new HttpClientExample();
//httpClientExample.Run();

//RestSharpExample rse = new RestSharpExample();
//rse.Run();
//RefitExample refitExample = new RefitExample();
//await refitExample.Run();