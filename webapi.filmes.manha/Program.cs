using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//app.MapGet("/", () => "Hello World!");

//Adiciona o servi�o de Controllers
builder.Services.AddControllers();

//Adiciona Servi�o de Jwt Bearer (forma de autentica��o)
builder.Services.AddAuthentication(Options =>
{

    Options.DefaultChallengeScheme = "JwtBearer";
    Options.DefaultAuthenticateScheme = "JwtBearer";

})

.AddJwtBearer("JwtBearer", options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         //valida quem esta solicitando  
         ValidateIssuer = true,

         //valida quem esta recebendo
         ValidateAudience = true,

         //define se o tempo de expira��o ser� validado
         ValidateLifetime = true,

         //forma de criptografia e valida a chave de autentica��o
         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),

         //valida o tempo de expira��o do token
         ClockSkew = TimeSpan.FromMinutes(5),

         //nome do issuer (de onde est� vindo)
         ValidIssuer = "webapi.filmes.manha",

         //nome do audience (para onde est� indo)
         ValidAudience = "webapi.filmes.manha"
     };
 });

//Adiciona o servi�o de Swagger
builder.Services.AddSwaggerGen(options =>
{
    //Adiciona informa��es sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Filmes Manh�",
        Description = "API para gerenciamento de filmes - Introdu��o da sprint 2 - Backend API",
        Contact = new OpenApiContact
        {
            Name = "Gabriela",
            Url = new Uri("https://github.com/gabrielarosa1309")
        }
    });

    //Configura o Swagger para usar o arquivo XML gerado
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //Usando a autentica�ao no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

//Come�a a configura��o do Swagger
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
//Finaliza a configura��o do Swagger

//Adiciona mapeamento dos Controllers
app.MapControllers();

//Adiciona autentica��o
app.UseAuthentication();

//Adicona autoriza��o
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();