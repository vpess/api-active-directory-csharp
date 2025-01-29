using ActiveDirectoryExplorer.Repositories;
using ActiveDirectoryExplorer.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);

    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Active Directory Explorer",
        Version = "v1",
        Description = "This API was developed to make Active Directory operations easier.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "vpess",
            Url = new Uri("https://github.com/vpess", UriKind.Absolute)
        }
    });
});

builder.Services.AddScoped<ActiveDirectoryAuthService>();
builder.Services.AddScoped<ValidationService>();

builder.Services.AddScoped<ComputerRepository>();
builder.Services.AddScoped<GroupRepository>();
builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//TODO Ordenar os dados dos objetos
//TODO Criar método para validar dados enviados para a API (BadRequests)
//TODO Tratar erros de execução (Objeto não encontrado, etc)
//TODO Criar uma boa documentação para o Swagger (Documentar possíveis códigos de erro, requests de exemplo, etc)