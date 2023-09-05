using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//app.MapGet("/", () => "Hello World!");

//Adiciona o serviço de Controllers
builder.Services.AddControllers();

//Adiciona Serviço de Jwt Bearer (forma de autenticação)
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

         //define se o tempo de expiração será validado
         ValidateLifetime = true,

         //forma de criptografia e valida a chave de autenticação
         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),

         //valida o tempo de expiração do token
         ClockSkew = TimeSpan.FromMinutes(5),

         //nome do issuer (de onde está vindo)
         ValidIssuer = "webapi.filmes.manha",

         //nome do audience (para onde está indo)
         ValidAudience = "webapi.filmes.manha"
     };
 });

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

    //Usando a autenticaçao no Swagger
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

//Adiciona autenticação
app.UseAuthentication();

//Adicona autorização
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();