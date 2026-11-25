using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SEWebApp.Models;

namespace SEWebApp.Data
{
    public static class RecallDataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services, string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException(
                    "FDA recall dataset was not found. Copy recalls.json into the Data folder.",
                    jsonFilePath);

            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();

            var json = await File.ReadAllTextAsync(jsonFilePath);
            var recalls = JsonSerializer.Deserialize<List<RecallNotice>>(json)
                          ?? new List<RecallNotice>();

            // Do not overwrite records already present in the database.
            var savedNumbers = await db.RecallNotices
                .AsNoTracking()
                .Select(r => r.RECALL_NUMBER)
                .ToListAsync();

            var existing = new HashSet<string>(
                savedNumbers, StringComparer.OrdinalIgnoreCase);
            var seenInFile = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var missing = recalls.Where(r =>
                !string.IsNullOrWhiteSpace(r.RECALL_NUMBER) &&
                !existing.Contains(r.RECALL_NUMBER) &&
                seenInFile.Add(r.RECALL_NUMBER)).ToList();

            if (missing.Count > 0)
            {
                db.RecallNotices.AddRange(missing);
                await db.SaveChangesAsync();
            }

            Console.WriteLine($"FDA recall import: {missing.Count} new records added.");
        }
    }
}
