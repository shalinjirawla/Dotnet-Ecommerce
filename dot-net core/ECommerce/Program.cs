using ECommerce.Models.Interfaces;
using ECommerce.Models.Models;
using ECommerce.Models.Repository;
using ECommerce.Services.AutoMapperProfile;
using ECommerce.Services.Interfaces;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// cors dependency
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsapp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


// dbContext Setup

builder.Services.AddDbContext<AppDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceConnString"), x => x.MigrationsAssembly("ECommerce.Models"));
    Options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddSwaggerGen(option =>
{
    //option.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTDemo", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter jwt access token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
ConfigureJwtAuthService(builder.Services);


// Automapper Profile 

builder.Services.AddAutoMapper(typeof(MapperProfile));

#region Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductRatingRepository, ProductRatingRepository>();
builder.Services.AddScoped<IProductRatingService, ProductRatingService>();

#endregion


// Add configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Register uploadsFolderPath
string uploadsFolderPath = builder.Configuration.GetValue<string>("UploadsFolderPath");
builder.Services.AddSingleton(uploadsFolderPath);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureJwtAuthService(IServiceCollection services)
{
    var config = builder.Configuration.GetSection("JWTConfig");
    var symmetricKeyAsBase64 = config["SecretKey"];
    var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
    var signingKey = new SymmetricSecurityKey(keyByteArray);

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = config["Issuer"],
            ValidateAudience = true,
            ValidAudience = config["Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };
    });
}