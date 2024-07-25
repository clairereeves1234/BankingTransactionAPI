using BankingTransactionAPI.Repositories; 
using BankingTransactionAPI.Services;
using BankingTransactionAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 

builder.Services.AddDbContext<TransactionDbContext>(
        options => options.UseInMemoryDatabase("MemoryDatabase"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization(); 

app.MapControllers();

app.Run();