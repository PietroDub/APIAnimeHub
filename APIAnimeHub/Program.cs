using APIAnimeHub.Data;
using APIAnimeHub.Repositories;
using APIAnimeHub.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserAnimeListRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<TokenService>();
builder.Services.AddHttpClient();
builder.Services.AddTransient<JikanService>();

builder.Services.AddAuthorization();
// Configura a autenticação da aplicação utilizando JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    // Configura as opções específicas do JWT
    .AddJwtBearer(o =>
    {
        // Permite testes sem HTTPS (apenas desenvolvimento)
        o.RequireHttpsMetadata = false;

        // Define as regras que serão usadas para validar o token recebido
        o.TokenValidationParameters = new TokenValidationParameters
        {
            // Chave secreta usada para validar a assinatura do token
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Secret"]!
                )
            ),

            // Emissor válido do token (quem gerou o token)
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            // Destinatário válido do token (quem pode utilizá-lo)
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // Não permite tolerância extra no tempo de expiração
            ClockSkew = TimeSpan.Zero
        };
    });

// configurando o swagger para aceitar o jwt
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Digite: Bearer {seu_token}"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
