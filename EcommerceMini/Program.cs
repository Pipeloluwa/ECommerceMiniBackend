using BusinessLogicLayer.AutoMappings;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.MiddleWare;
using BusinessLogicLayer.Services;
using DataAccessLayer.Dapper;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



// Add CORS policy
var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!.Split(",");

builder.Services.AddCors(options =>
{
    options.AddPolicy("_allowFrontend",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); //Only needed if using authentication (cookies, tokens, etc.)
        });
});


// Add services to the container.

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.Audience = allowedOrigins[0];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero, // THIS IS TO PREVENT JWT BY DEFAULT FROM AUTOMATICALLY ADDING EXTRA 5 MINS TO EXPIRATION TIME
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    }
  );


builder.Services.AddScoped<UserJwtValidityMiddleWare>();
builder.Services.AddScoped<AdminJwtValidityMiddleWare>();


builder.Services.AddTransient<IDbConnection>(options =>
   new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IPasswordHash, PasswordHash>();

builder.Services.AddAutoMapper(typeof(FoodProductAutoMap));
builder.Services.AddScoped<IDapperConnection, DapperConnection>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IFoodProductRepository, FoodProductRepository>();
builder.Services.AddScoped<IFoodProductService, FoodProductService>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();   


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}


if (!app.Environment.IsDevelopment())
{
 app.UseHttpsRedirection();
}


app.UseCors("_allowFrontend");
//app.UseCors("AllowFrontend");


app.UseAuthorization();

app.MapControllers();

app.Run();
