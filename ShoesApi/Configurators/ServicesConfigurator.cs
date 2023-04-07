using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShoesApi.Filters;
using ShoesApi.Services;
using Swashbuckle.AspNetCore.Filters;

namespace ShoesApi.Configurators
{
	/// <summary>
	/// Конфигуратор сервисов
	/// </summary>
	public static class ServicesConfigurator
	{
		/// <summary>
		/// Сконфигурировать сервисы
		/// </summary>
		/// <param name="builder">Билдер приложения</param>
		public static void ConfigureServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddControllers(opt =>
				{
					opt.Filters.Add<VoidAndTaskTo204NoContentFilter>();
				});

			builder.Services.AddEndpointsApiExplorer()
				.AddHttpContextAccessor()
				.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
				.AddCors()
				.AddSwagger()
				.AddAuthorization(builder.Configuration)
				.AddScoped<IUserService, UserService>()
				.AddSingleton<IDateTimeProvider, DateTimeProvider>()
				.AddDatabase(builder.Configuration);
		}

		/// <summary>
		/// Сконфигурировать Swagger
		/// </summary>
		/// <param name="services">Сервисы</param>
		private static IServiceCollection AddSwagger(this IServiceCollection services)
			=> services
				.AddSwaggerGen(options =>
				{
					options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
					{
						Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
						In = ParameterLocation.Header,
						Name = "Authorization",
						Type = SecuritySchemeType.ApiKey
					});

					options.OperationFilter<SecurityRequirementsOperationFilter>();
					options.SupportNonNullableReferenceTypes();

					var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
					options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
				});

		/// <summary>
		/// Сконфигурировать сервисы авторизации
		/// </summary>
		/// <param name="services">Сервисы</param>
		/// <param name="configurationManager">Менеджер конфигурации приложения</param>
		private static IServiceCollection AddAuthorization(this IServiceCollection services, ConfigurationManager configurationManager)
		{
			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
								.GetBytes(configurationManager.GetSection("AppSettings:Token").Value)),
							ValidateIssuer = false,
							ValidateAudience = false
						};
					});
			return services;
		}

		/// <summary>
		/// Сконфигурировать подключение к БД
		/// </summary>
		/// <param name="services">Сервисы</param>
		/// <param name="configurationManager">Менеджер конфигурации приложения</param>
		private static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
		{
			var connString = configurationManager.GetConnectionString("ShoesDb");
			return services.AddDbContext<ShoesDbContext>(opt =>
				{
					opt.UseNpgsql(connString);
					opt.UseSnakeCaseNamingConvention();
				});
		}
	}
}
