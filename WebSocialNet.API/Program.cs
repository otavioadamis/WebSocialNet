using WebSocialNet.API.Helpers;
using WebSocialNet.Dal.Data;
using WebSocialNet.Service;
using Microsoft.EntityFrameworkCore;
using WebSocialNet.Domain.Interfaces.IRepositories;
using WebSocialNet.Dal.Repositories;
using WebSocialNet.Domain.Interfaces.IServices;
using WebSocialNet.API.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebSocialNet.Domain.DTOs.ChatDTOs;

namespace WebSocialNet.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var settings = builder.Configuration.GetSection("DatabaseSettings").Get<AppSettings>();
            var connectionString = settings.ConnectionString;

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(       
                options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
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
                        new string[]{}
                    }
                });
            });

            builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            //builder.Services.AddScoped<IPostRepo, PostRepo>();
            //builder.Services.AddScoped<ICommentRepo, CommentRepo>();
            builder.Services.AddScoped<IChatRepo, ChatRepo>();

            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IOpenAIService, OpenAIService>();
            builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
            builder.Services.AddScoped<ChatHubService>();
            builder.Services.AddScoped<IUserChatService, UserChatService>();

            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<JwtMiddleware>();

            app.UseHttpsRedirection();

            app.MapHub<ChatHubService>("chat-hub");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            /*using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }*/

            app.Run();
        }
    }
}


