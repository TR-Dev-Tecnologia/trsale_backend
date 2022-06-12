using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using TRSale.CrossCutting;
using TRSale.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigRepositories.Config(builder.Services);
ConfigServices.Config(builder.Services);

Environment.SetEnvironmentVariable("Secret", builder.Configuration["Authentication:Secret"]);
Environment.SetEnvironmentVariable("Issuer", builder.Configuration["Authentication:Issuer"]);
Environment.SetEnvironmentVariable("Audience", builder.Configuration["Authentication:Audience"]);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string[] listaCors = new string[] {
        "https://trsale.com",
        "https://localhost:4200"

     };
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
    builder =>
    {

        builder.WithOrigins(listaCors)
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod();
    });
});


#region Authentication with cookie
var secret = builder.Configuration["Authentication:Secret"];
var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(i =>
{
    i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Authentication:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
    x.SaveToken = true;
    x.RequireHttpsMetadata = false;
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("tk_TRSale"))
            {
                context.Token = context.Request.Cookies["tk_TRSale"];
            }
            return Task.CompletedTask;
        },
    };

}).AddCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.IsEssential = true;
    });

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; options.Providers.Add<BrotliCompressionProvider>();
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Environment.SetEnvironmentVariable("DomainCookie", null);
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
} else
{
    Environment.SetEnvironmentVariable("DomainCookie", builder.Configuration["DomainCookie"]);
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsApi");
app.MapControllers();
app.Run();
