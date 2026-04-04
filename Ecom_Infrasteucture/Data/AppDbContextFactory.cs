using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json;

namespace Ecom_Infrasteucture.Data
{
    public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var settingsPath = FindAppSettingsPath();
            var connectionString = ReadConnectionString(settingsPath, "Ecom");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"ConnectionStrings:Ecom is empty in '{settingsPath}'.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }

        private static string FindAppSettingsPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            foreach (var directory in EnumerateParents(currentDirectory))
            {
                var apiSettings = Path.Combine(directory, "Ecom_Api", "appsettings.json");
                if (File.Exists(apiSettings))
                {
                    return apiSettings;
                }

                var rootSettings = Path.Combine(directory, "appsettings.json");
                if (File.Exists(rootSettings))
                {
                    return rootSettings;
                }
            }

            throw new FileNotFoundException(
                "Could not find appsettings.json for connection string. Looked for 'Ecom_Api/appsettings.json' and 'appsettings.json' from the current directory upwards.",
                Path.Combine(currentDirectory, "Ecom_Api", "appsettings.json"));
        }

        private static IEnumerable<string> EnumerateParents(string startDirectory)
        {
            for (var directory = startDirectory; !string.IsNullOrWhiteSpace(directory); directory = Directory.GetParent(directory)?.FullName)
            {
                yield return directory;
            }
        }

        private static string? ReadConnectionString(string appSettingsPath, string name)
        {
            var baseValue = ReadConnectionStringFromJson(appSettingsPath, name);

            var baseDirectory = Path.GetDirectoryName(appSettingsPath) ?? Directory.GetCurrentDirectory();
            var developmentPath = Path.Combine(baseDirectory, "appsettings.Development.json");

            var developmentValue = File.Exists(developmentPath)
                ? ReadConnectionStringFromJson(developmentPath, name)
                : null;

            return string.IsNullOrWhiteSpace(developmentValue) ? baseValue : developmentValue;
        }

        private static string? ReadConnectionStringFromJson(string jsonPath, string name)
        {
            using var stream = File.OpenRead(jsonPath);
            using var json = JsonDocument.Parse(stream);

            if (json.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings) is false ||
                connectionStrings.TryGetProperty(name, out var connectionStringElement) is false)
            {
                return null;
            }

            return connectionStringElement.GetString();
        }
    }
}
