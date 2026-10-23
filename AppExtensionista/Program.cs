using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AppExtensionista.Data;
using AppExtensionista.Models;
using AppExtensionista.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

namespace AppExtensionista;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // Define diretório de dados em AppData para o banco SQLite
        var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppExtensionista");
        if (!Directory.Exists(appDataFolder))
        {
            Directory.CreateDirectory(appDataFolder);
        }

        // Se a wwwroot não existir ao lado do .EXE, ele cria uma no TEMP e define como BaseDirectory
        GarantirDiretorioWwwroot();

        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

        // Caminho isolado do banco SQLite
        var dbPath = Path.Combine(appDataFolder, "app_extensionista.db");
        var connectionString = $"Data Source={dbPath}";

        // Serviços e DbContext
        appBuilder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        appBuilder.Services.AddScoped<ISettingsService, SettingsService>();
        appBuilder.Services.AddScoped<IAlimentoService, AlimentoService>();
        appBuilder.Services.AddScoped<IGuiaArmazenamentoService, GuiaArmazenamentoService>();
        appBuilder.Services.AddScoped<IEstoqueService, EstoqueService>();
        appBuilder.Services.AddScoped<IDescarteService, DescarteService>();

        appBuilder.RootComponents.Add<App>("#app");

        var app = appBuilder.Build();

        // Inicialização do Banco
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();

            if (!context.AppSettings.Any())
            {
                context.AppSettings.Add(new AppSettingsModel { });
                context.SaveChanges();
            }
        }

        // Janela Nativa
        app.MainWindow
            .SetTitle("App Extensionista - Controle de Alimentos")
            .SetSize(1024, 720)
            .Center()
            .SetResizable(true);

        app.Run();
    }

    private static void GarantirDiretorioWwwroot()
    {
        var localWwwroot = Path.Combine(AppContext.BaseDirectory, "wwwroot");
        if (!Directory.Exists(localWwwroot))
        {
            var tempWwwroot = Path.Combine(Path.GetTempPath(), "AppExtensionista", "wwwroot");
            Directory.CreateDirectory(tempWwwroot);

            // Se index.html não existir na pasta TEMP, gera o HTML base dele
            var indexPath = Path.Combine(tempWwwroot, "index.html");
            if (!File.Exists(indexPath))
            {
                var indexHtmlContent = @"<!DOCTYPE html>
<html lang=""pt-br"">
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>App Extensionista</title>
    <base href=""/"" />
</head>
<body style=""background-color: #1a1a1a; margin: 0; padding: 0; overflow-x: hidden;"">
    <div id=""app"">Carregando...</div>
    <script src=""_framework/blazor.webview.js""></script>
</body>
</html>";
                File.WriteAllText(indexPath, indexHtmlContent);
            }

            // Altera o diretório corrente para que o Photino encontre a wwwroot temporária
            Directory.SetCurrentDirectory(Path.Combine(Path.GetTempPath(), "AppExtensionista"));
        }
    }
}