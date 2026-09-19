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
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAlimentoService, AlimentoService>();
builder.Services.AddScoped<IGuiaArmazenamentoService, GuiaArmazenamentoService>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
builder.Services.AddScoped<IDescarteService, DescarteService>();

var app = builder.Build();

// Garante que o banco .db e as tabelas sejam criadas automaticamente na inicialização
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Aplica as migrations sem apagar dados já existentes
    context.Database.Migrate();

    // Cria User ADM se o banco estiver limpo
    if (!context.Usuarios.Any())
    {
        context.Usuarios.Add(new UsuarioModel
        {
            Nome = "Administrador",
            Login = "Admin",
            Senha = "4731589", 
            DiasAlertaPadrao = 7
        });
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();