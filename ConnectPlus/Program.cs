using ConnectPlus.Data;
using ConnectPlus.Interfaces;
using ConnectPlus.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ConnectPlusContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


builder.Services.AddScoped<ITipoContatoRepository, TipoContatoRepository>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api de Contatos",
        Description = "Aplicação para gerenciamento de contatos",
        Contact = new OpenApiContact
        {
            Name = "Stephani Dandara",
            Url = new Uri("https://github.com/Stephanilima95")
        }
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();