using LeaveMGMTApi.Data;
using Microsoft.EntityFrameworkCore;
using LeaveMGMTApi.Middlewares;
using LeaveMGMTApi.Repository;
using LeaveMGMTApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IReposaryBase, RepositoryBase>();
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// custom middleware added for Exception and JWT Token Validation
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TokenValidatorMiddleware>();

app.Run();
