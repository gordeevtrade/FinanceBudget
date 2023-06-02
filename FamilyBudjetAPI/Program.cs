using Budget.BuisnessLogic.Sevices;
using Budget.BuisnessLogic.Sevices.Interface;
using Budget.DAL.Repositories;
using Budget.DAL.Repositories.Interfaces;
using FamilyBudjetAPI;
using FamilyBudjetAPI.Mapping;
using FamilyBudjetAPI.Sevices;
using FamilyBudjetAPI.Sevices.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["GoogleAuth:Authority"];
    options.Audience = builder.Configuration["GoogleAuth:ClientId"];
    options.MetadataAddress = builder.Configuration["GoogleAuth:GoogleKeys"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddDbContext<FinanceContext>(options =>
{
    options.UseSqlServer(connection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
builder.Services.AddScoped<IUnitOfWOrk, UnitOfWork>();
builder.Services.AddScoped<ICategoryTypeService, CategoryTypeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IFinanceReportService, FinanceReportService>();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FinanceContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();