using AppExtensionista.Data;
using AppExtensionista.Models;
using AppExtensionista.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Controllers e Views
builder.Services.AddControllersWithViews();

// Registro dos Services com Injeção de Dependencia
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IAlimentoService, AlimentoService>();
builder.Services.AddScoped<IGuiaArmazenamentoService, GuiaArmazenamentoService>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
builder.Services.AddScoped<IDescarteService, DescarteService>();

// Suporte ao Razor/Blazor Services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Garante que o banco .db e as tabelas sejam criadas automaticamente na inicialização
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Aplica as migrations sem apagar dados já existentes
    context.Database.Migrate();

    // Cria configs se o banco estiver limpo
    if (!context.AppSettings.Any())
    {
        context.AppSettings.Add(new AppSettingsModel{});
        context.SaveChanges();
    }
}

// Configurações do Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Controllers 
app.MapControllers();

// Configurações do Blazor
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();