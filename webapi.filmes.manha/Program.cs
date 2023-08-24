var builder = WebApplication.CreateBuilder(args);

//app.MapGet("/", () => "Hello World!");

//Adiciona o serviço de Controllers
builder.Services.AddControllers();

//Adiciona o serviço de Swagger
builder.Services.AddSwaggerGen();

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