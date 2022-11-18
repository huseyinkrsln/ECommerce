using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//Tüm isteklere karşı açık olmasını istersek
// builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyMethod()));
//bizim istediklerimize apinin cevap vermesini istersek
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));


builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

//builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/**JWT KULLANIMI ADım 1 -> DOğrulama
 * Jwt için yapılan ilk adım doğrulama
FArklı şemalara sahip olcaz bu uygulamada. YAni yönetşm paneli için ayrı ui için ayrı
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme); bunuda kullanabilirdik fakat admin özel schema yapacağımız için tercih etmedik
adım 2 application katmanında-abstraction-token klasöründe
*/

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        // token ı doğrulama parametreleri(yani hangi değerlere bakacak doğrularken)
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, // oluşturulacak token değerlerini kimlerin/hangi originlerin/sitelerin kullanacağını belirlediğimiz değerdir Exp: www.bilmemne.com
            ValidateIssuer = true,//oluşturulacak token değerini kimin dağıttığını ifade edeceğimiz alandır Exp: www.myapi.com
            ValidateLifetime = true, // oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır
            ValidateIssuerSigningKey = true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key verisinin doğrulanmasıdır
        
            //doğrulanacak değerler
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))


        };
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
