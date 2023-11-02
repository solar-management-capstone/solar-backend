using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SolarMP.Interfaces;
using SolarMP.Models;
using SolarMP.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add scope data here
builder.Services.AddScoped<IAccount, AccountServices>();
builder.Services.AddScoped<IPromotion, PromotionService>();
builder.Services.AddScoped<IAcceptance, AcceptanceServices>();
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<IPackage, PackageServices>();
builder.Services.AddScoped<IProcess, ProcessServices>();
builder.Services.AddScoped<ISurvey, SurveyServices>();
builder.Services.AddScoped<IBracket, BracketServices>();
builder.Services.AddScoped<IConstructionContract, ConstructionContractServices>();
builder.Services.AddScoped<IWarrantyReport, WarrantyServices>();
builder.Services.AddScoped<IPayment, PaymentServices>();
builder.Services.AddScoped<IRequest, RequestServices>();
builder.Services.AddScoped<ITwilio, TwilioServices>(); 
builder.Services.AddScoped<IFeedback, FeedbackServices>();



// add dbcontext and handler cycles and tracking data
builder.Services.AddDbContext<solarMPContext>(option =>
{
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// add xml comment in api
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("solarMP", new OpenApiInfo() { Title = "solarMP", Version = "v1" });
    //setup comment in swagger UI
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentFileFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);

    option.IncludeXmlComments(xmlCommentFileFullPath);
    
    //set up jwt token authorize
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// add jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/solarMP/swagger.json", "solarMPApi v1"));
}

app.UseCors(x => x.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

