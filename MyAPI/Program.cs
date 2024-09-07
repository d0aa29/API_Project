using MyAPI;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Repository.IRepository;
using MyAPI.Repository;

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
