using WebSocialNet.API.Helpers;
using WebSocialNet.Dal.Data;
using WebSocialNet.Service;
using Microsoft.EntityFrameworkCore;
using WebSocialNet.Domain.Interfaces.IRepositories;
using WebSocialNet.Dal.Repositories;
using WebSocialNet.Domain.Interfaces.IServices;

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
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            //builder.Services.AddScoped<IPostRepo, PostRepo>();
            //builder.Services.AddScoped<ICommentRepo, CommentRepo>();

            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IOpenAIService, OpenAIService>();
            builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
            builder.Services.AddScoped<ChatHubService>();

            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapHub<ChatHubService>("chat-hub");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


