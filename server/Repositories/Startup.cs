using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySqlConnector;

namespace keeprFINAL;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		ConfigureCors(services);
		ConfigureAuth(services);
		services.AddControllers();
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "keeprFINAL", Version = "v1" });
		});
		services.AddSingleton<Auth0Provider>();
		services.AddScoped<IDbConnection>(x => CreateDbConnection());


		// SECTION: ACCOUNT
		services.AddScoped<AccountsRepository>();
		services.AddScoped<AccountService>();

		// SECTION: KEEPS
		services.AddScoped<KeepsRepository>();
		services.AddScoped<KeepsService>();

		// SECTION: VAULTS
		services.AddScoped<VaultsRepository>();
		services.AddScoped<VaultsService>();

		// SECTION: VAULTKEEPS
		services.AddScoped<VaultKeepsRepository>();
		services.AddScoped<VaultKeepsService>();

		// SECTION: PROFILES
		services.AddScoped<ProfilesRepository>();
		services.AddScoped<ProfilesService>();
	}

	private void ConfigureCors(IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("CorsDevPolicy", builder =>
				 {
					 builder
					 .AllowAnyMethod()
					 .AllowAnyHeader()
					 .AllowCredentials()
					 .WithOrigins(new string[]{
					 "http://localhost:8080", "http://localhost:8081"
				 });
				 });
		});
	}

	private void ConfigureAuth(IServiceCollection services)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.Authority = $"https://{Configuration["AUTH0_DOMAIN"]}/";
			options.Audience = Configuration["AUTH0_AUDIENCE"];
		});

	}

	private IDbConnection CreateDbConnection()
	{
		string connectionString = Configuration["CONNECTION_STRING"];
		return new MySqlConnection(connectionString);
	}


	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "keeprFINAL"));
			app.UseCors("CorsDevPolicy");
		}

		app.UseHttpsRedirection();

		app.UseDefaultFiles();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();

		app.UseAuthorization();


		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}

