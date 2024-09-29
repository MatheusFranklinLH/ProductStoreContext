using LeMat.Domain.Handlers;
using LeMat.Domain.Repositories;
using LeMat.Infra.Contexts;
using LeMat.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace LeMat.Api;
public class Startup {
	public Startup(IConfiguration configuration, IWebHostEnvironment env) {
		Configuration = configuration;
		CurrentEnvironment = env;
	}

	public IConfiguration Configuration { get; }
	private IWebHostEnvironment CurrentEnvironment { get; }

	public void ConfigureServices(IServiceCollection services) {
		services.AddControllers();

		services.AddDbContext<SPContext>(optionsBuilder => {
			optionsBuilder.UseNpgsql(Configuration.GetConnectionString("connectionString"), options => options.MigrationsHistoryTable("__MigrationsHistory", "lemat"));
			if (CurrentEnvironment.IsDevelopment()) {
				optionsBuilder.LogTo(System.Console.WriteLine, new[] { DbLoggerCategory.Database.Name });
				optionsBuilder.EnableSensitiveDataLogging();
			}
		});
		// services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

		services.AddScoped<ISupplierRepository, SupplierRepository>();
		services.AddScoped<SupplierHandler, SupplierHandler>();
		services.AddTransient<IProductRepository, ProductRepository>();
		services.AddTransient<IFilesRepository, FilesRepository>();
		services.AddTransient<ProductHandler, ProductHandler>();
		services.AddScoped<ITransactionRepository, TransactionRepository>();

		// services
		//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		//    .AddJwtBearer(options => {
		// 	   options.Authority = "https://securetoken.google.com/project-1064011784157549102";
		// 	   options.TokenValidationParameters = new TokenValidationParameters {
		// 		   ValidateIssuer = true,
		// 		   ValidIssuer = "https://securetoken.google.com/project-1064011784157549102",
		// 		   ValidateAudience = true,
		// 		   ValidAudience = "project-1064011784157549102",
		// 		   ValidateLifetime = true
		// 	   };
		//    });

		services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo {
			Title = "LeMat",
			Version = "v1",
			Description = "",
		}));
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
		if (env.IsDevelopment()) {
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeMat"));
		}

		app.UseHttpsRedirection();

		app.UseRouting();
		app.UseCors(x => x
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

		// app.UseAuthentication();
		// app.UseAuthorization();

		app.UseEndpoints(endpoints => {
			endpoints.MapControllers();
		});
	}
}