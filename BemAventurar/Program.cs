using BemAventurar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços MVC
builder.Services.AddControllers();

// Adiciona AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Injeta a Interface com a Service
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();
builder.Services.AddScoped<IModuloInterface, ModuloService>();
builder.Services.AddScoped<IPermissaoInterface, PermissaoService>();
builder.Services.AddScoped<IInscricaoInterface, InscricaoService>();
builder.Services.AddScoped<IEventoInterface, EventoService>();
builder.Services.AddScoped<IEventoFaqInterface, EventoFaqService>();
builder.Services.AddScoped<IEventoFotoInterface, EventoFotoService>();
builder.Services.AddScoped<IEventoItinerarioInterface, EventoItinerarioService>();
builder.Services.AddScoped<IEventoVideoInterface, EventoVideoService>();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("usuariosApp", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// JWT - Autenticação
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// Autorização
builder.Services.AddAuthorization();

// Swagger com suporte a JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BemAventurar API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo. Ex: Bearer {H2v1pBXLnJx9yYWMLW7U8vJbz3fElg2czcYDb6kMXmY=}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Inicializa o admin, se necessário
using (var scope = app.Services.CreateScope())
{
    var usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioInterface>();
    await usuarioService.InicializarAdminAsync();
}

// Ambiente de desenvolvimento: habilita Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS
app.UseCors("usuariosApp");

// HTTPS redirection
app.UseHttpsRedirection();

// Autenticação e Autorização
app.UseAuthentication(); // IMPORTANTE: sempre antes de UseAuthorization
app.UseAuthorization();

// Mapeia os controllers
app.MapControllers();

app.Run();
