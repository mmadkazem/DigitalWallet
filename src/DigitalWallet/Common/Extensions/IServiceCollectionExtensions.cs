namespace DigitalWallet.Common.Extensions;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services
            .AddAuthenticationConfig(configuration)
            .ConfigureDbContexts(configuration)
            .ConfigureValidator()
            .AddSharedServices()
            .AddSwaggerConfig()
            .AddCarter()
            .AddOptionsConfig(configuration)
            .AddServiceDiscovery();
        return services;
    }
    public static IApplicationBuilder UseShared(this IApplicationBuilder app)
    {
        app.UseMiddleware<BadRequestExceptionMiddleware>();
        app.UseMiddleware<NotFoundExceptionMiddleware>();
        return app;
    }
    private static IServiceCollection AddOptionsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<TokenOption>()
                .Bind(configuration.GetSection("BearerTokens"));

        services.AddOptions<RefreshTokenOption>()
                .Bind(configuration.GetSection("RefreshToken"));

        return services;
    }
    private static IServiceCollection ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        // DI DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
        return services;
    }
    private static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<BadRequestExceptionMiddleware>();
        services.AddScoped<NotFoundExceptionMiddleware>();
        return services;
    }
    private static IServiceCollection ConfigureValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies([typeof(IAssemblyMarker).Assembly]);

        return services;
    }
    private static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Liaro", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
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
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
    public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("BearerTokens");
        var refreshOptions = configuration.GetSection("RefreshToken");
        services
            .AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options["Issuer"], // site that makes the token
                    ValidateIssuer = true,
                    ValidAudience = options["Audience"], // site that consumes the token
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options["Key"])),
                    ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                    ValidateLifetime = true, // validate the expiration
                    ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                };
            })
            .AddJwtBearer("RefreshScheme", cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = refreshOptions["Issuer"], // site that makes the token
                    ValidateIssuer = true,
                    ValidAudience = refreshOptions["Audience"], // site that consumes the token
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshOptions["Key"])),
                    ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                    ValidateLifetime = true, // validate the expiration
                    ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                };
            });
        services.AddAuthorization();
        return services;
    }
}
