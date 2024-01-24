using Automatonymous.Binders;
using FundoBusinessLayer.Interface;
using FundoBusinessLayer.Service;
using FundoRepositoryLayer.Context;
using FundoRepositoryLayer.Interface;
using FundoRepositoryLayer.Service;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Text;




namespace FundoNotesApplicationPresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)


        {

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddDbContext<FundoContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("FundoDbConString")));

                //forgot password services
                builder.Services.AddMassTransit(x =>
                {
                    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                    {
                        config.UseHealthCheck(provider);
                        config.Host(new Uri("rabbitmq://localhost"), h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                    }));
                });

                builder.Services.AddMassTransitHostedService();
                builder.Services.AddScoped<IBus>(provider => provider.GetRequiredService<IBusControl>());

                //builder.Services.AddSwaggerGen();
                // swagger gen
                builder.Services.AddSwaggerGen(option =>
                {
                    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
        });
                });
            builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration["RedisCacheUrl"]; });
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        
                        );
                
            });

            builder.Services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(o =>
                    {
                        var Key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
                        o.SaveToken = true;
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Key)
                        };
                    });

                builder.Services.AddTransient<IUserBusiness, UserBusiness>();
                builder.Services.AddTransient<IUserRepo, UserRepo>();
                builder.Services.AddTransient<INotesBusiness, NotesBusiness>();
                builder.Services.AddTransient<INotesRepo, NotesRepo>();
                builder.Services.AddTransient<IProductBusiness, ProductBusiness>();
                builder.Services.AddTransient<IProductRepo, ProductRepo>();
                builder.Services.AddTransient<ILabelBusiness, LabelBusiness>();
                builder.Services.AddTransient<ILabelRepo, LabelRepo>();
                builder.Services.AddTransient<IColloboratorBusiness, ColloboratorBusiness>();
                builder.Services.AddTransient<IColloboratorRepo, ColloboratorRepo>();


                var app = builder.Build();



                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                 app.UseSession();
                app.UseHttpsRedirection();
            app.UseAuthentication();
        
            app.UseCors("AllowOrigin");
            app.UseAuthorization();


                app.MapControllers();

                app.Run();

            




        }
           


     }
        


 }

