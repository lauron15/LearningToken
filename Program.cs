using JwtBearer.Models;
using JwtBearer.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Registro to Token. Sempre colocar.  
builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", handler: (TokenService service)
=> service.Generate(

//Essa parte é só para gerar o usuário, se eu tivesse criando pelo controller, creio que eu não precissaria disso. 
new User(    
id:1, 
Email: "teste@lauro.io", 
Password:"123", 
Roles: new[]
{
"student","premium"
})
));

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
