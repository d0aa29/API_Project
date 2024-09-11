using MyAPI;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Repository.IRepository;
using MyAPI.Repository;
using Microsoft.AspNetCore.Identity;
using MyAPI.Models;

namespace MyAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<ApplicationDbContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
			});
			builder.Services.AddScoped<IVillaRepository,VillaRepository>();
            builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()  // Add this line to configure the user and role stores.
              .AddDefaultTokenProviders();
            //object value = builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //      //.AddEntityFrameworkStores<ApplicationDbContext>()
            //      .AddEntityFrameworkStores<ApplicationDbContext>()  // Add this line to configure the user and role stores.
            //       .AddDefaultTokenProviders();
            builder.Services.AddAutoMapper(typeof(MappingConfig));
			builder.Services.AddControllers(options=> {
				//options.ReturnHttpNotAcceptable = true;
			}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
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

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
