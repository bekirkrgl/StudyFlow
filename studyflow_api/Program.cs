
using Microsoft.EntityFrameworkCore;
using StudyFlow.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Controller servislerini ekliyoruz.
builder.Services.AddControllers();

// SQL Server baglanti bilgisini aliyoruz.
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "DefaultConnection baglanti bilgisi bulunamadi.");
}

// Entity Framework Core ve SQL Server baglantisi.
builder.Services.AddDbContext<StudyFlowDbContext>(
    options =>
        options.UseSqlServer(connectionString)
);

var app = builder.Build();

// HTTP isteklerini HTTPS'e yonlendiriyoruz.
app.UseHttpsRedirection();

// Yetkilendirme altyapisi.
app.UseAuthorization();

// Controller endpointlerini aktif ediyoruz.
app.MapControllers();

// Uygulamayi baslatiyoruz.
app.Run();
