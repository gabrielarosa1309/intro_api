using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//app.MapGet("/", () => "Hello World!");

//Adiciona o serviço de Controllers
builder.Services.AddControllers();

//Adiciona o serviço de Swagger
builder.Services.AddSwaggerGen(options =>
{
    //Adiciona informações sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Filmes Manhã",
        Description = "API para gerenciamento de filmes - Introdução da sprint 2 - Backend API",
        Contact = new OpenApiContact
        {
            Name = "Gabriela",
            Url = new Uri("https://github.com/gabrielarosa1309")
        }
    });

    //Configura o Swagger para usar o arquivo XML gerado
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

//Começa a configuração do Swagger
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
//Finaliza a configuração do Swagger

//Adiciona mapeamento dos Controllers
app.MapControllers();

app.UseHttpsRedirection();

app.Run();