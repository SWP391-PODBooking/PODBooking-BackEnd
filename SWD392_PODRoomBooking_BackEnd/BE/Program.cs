using BE.src.Domains.Database;
using BE.src.Repositories;
using BE.src.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BE.src.Shared.Constant;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JWT.Issuer,
        ValidAudience = JWT.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.SecretKey))
    };
});

builder.Services.AddScoped<IUserServ, UserServ>(); builder.Services.AddScoped<IRoomServ, RoomServ>();
builder.Services.AddScoped<IMembershipServ, MembershipServ>();
builder.Services.AddScoped<IBookingServ, BookingServ>();
builder.Services.AddScoped<IAreaServ, AreaServ>();
builder.Services.AddScoped<IRoomServ, RoomServ>();
builder.Services.AddScoped<IBookingServ, BookingServ>();
builder.Services.AddScoped<IAmenityServiceServ, AmenityServiceServ>();
builder.Services.AddScoped<ITransactionServ, TrasactionServ>();
builder.Services.AddScoped<IAnalysticServ, AnalysticServ>();
builder.Services.AddScoped<IReportServ, ReportServ>();

builder.Services.AddScoped<IUserRepo, UserRepo>(); builder.Services.AddScoped<IRoomRepo, RoomRepo>();
builder.Services.AddScoped<IMembershipRepo, MembershipRepo>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IAreaRepo, AreaRepo>();
builder.Services.AddScoped<IRoomRepo, RoomRepo>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IAmenityServiceRepo, AmenityServiceRepo>();
builder.Services.AddScoped<ITransactionRepo, TrasactionRepo>();
builder.Services.AddScoped<IAnalysticRepo, AnalysticRepo>();
builder.Services.AddScoped<IReportRepo, ReportRepo>();

builder.Services.AddDbContext<PodDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseDeveloperExceptionPage();

app.UseRouting();

app.MapControllers();

app.Run();