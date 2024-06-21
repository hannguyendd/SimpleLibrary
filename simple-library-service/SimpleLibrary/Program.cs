using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleLibrary.Domain.Contexts;
using SimpleLibrary.Domain.Shared.Configurations;
using SimpleLibrary.Extensions;
using SimpleLibrary.Middlewares;

const string AllowAllOrigins = "AllowAllOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection(SettingKey.Jwt));

builder.Services.AddDbContext<LibraryContext>((opt) =>
{
    opt.UseInMemoryDatabase("Library");
});
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var config = builder.Configuration.GetSection(SettingKey.Jwt).Get<JwtSetting>();

    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(config?.Secret ?? string.Empty)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddHttpContextAccessor();

builder.Services.RegisterAppDependencies();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddMappings();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: AllowAllOrigins,
        builder =>
            builder.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader()
            );
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowAllOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetService<LibraryContext>();
    context?.Database.EnsureCreated();
}

app.Run();

