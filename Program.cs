using ForumProject.Data;
using ForumProject.DatabaseServices.BoardsServices;
using ForumProject.DatabaseServices.PostsServices;
using ForumProject.DatabaseServices.RepliesServices;
using ForumProject.DatabaseServices.UsersServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration.GetSection("JWTSecretKey").Value;

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ForumDataContext>();
builder.Services.AddScoped<GetBoardsService>();
builder.Services.AddScoped<GetPostsService>();
builder.Services.AddScoped<PostPostsService>();
builder.Services.AddScoped<PutPostsService>();
builder.Services.AddScoped<PostRepliesService>();
builder.Services.AddScoped<PostUsersServices>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), 
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("https://localhost:44403").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("corspolicy");
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
