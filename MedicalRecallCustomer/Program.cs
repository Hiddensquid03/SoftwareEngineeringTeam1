using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

// These are fictional examples for coursework, not actual safety alerts.
var notices = new List<Notice>
{
    new(1, "Infusion Pump A1", "MedEquip", "Infusion devices", new DateOnly(2026, 3, 18), "High",
        "Unexpected pause during infusion", "A possible interruption was identified during routine use.",
        "The review recommends checking the device before use and following the manufacturer's guidance."),
    new(2, "Patient Monitor M2", "CareTech", "Monitoring", new DateOnly(2026, 1, 22), "Medium",
        "Intermittent display issue", "Some readings may not appear consistently on screen.",
        "The team has highlighted the importance of regular equipment checks and reporting display problems."),
    new(3, "Syringe Driver S4", "HealthWorks", "Infusion devices", new DateOnly(2026, 1, 8), "High",
        "Alarm may be delayed", "An alarm response delay has been noted in the sample notice.",
        "Staff should follow local safety procedures and check the relevant manufacturer instructions."),
    new(4, "Digital Thermometer T3", "WellMed", "Diagnostics", new DateOnly(2025, 12, 5), "Low",
        "Inconsistent temperature reading", "The device may show an inaccurate reading under certain conditions.",
        "The team recommends checking the device's calibration guidance before relying on a result.")
};

var dataDirectory = Path.Combine(app.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataDirectory);
var commentFile = Path.Combine(dataDirectory, "comments.json");
var gate = new object();
List<Comment> comments;
try
{
    comments = File.Exists(commentFile)
        ? JsonSerializer.Deserialize<List<Comment>>(File.ReadAllText(commentFile)) ?? new()
        : new();
}
catch (JsonException)
{
    throw new InvalidOperationException("The comments file is invalid. Back it up and correct it before restarting.");
}

app.MapGet("/api/notices", (string? q, string? month, string? sort) =>
{
    IEnumerable<Notice> result = notices;
    if (!string.IsNullOrWhiteSpace(q))
    {
        var term = q.Trim();
        result = result.Where(n => new[] { n.Device, n.Manufacturer, n.Category, n.Issue, n.Summary, n.Finding }
            .Any(s => s.Contains(term, StringComparison.OrdinalIgnoreCase)));
    }
    if (!string.IsNullOrWhiteSpace(month))
    {
        if (month.Length != 7 || !DateOnly.TryParseExact(month + "-01", "yyyy-MM-dd", out var selected))
            return Results.BadRequest(new { error = "Invalid month." });
        result = result.Where(n => n.Date.Year == selected.Year && n.Date.Month == selected.Month);
    }
    result = sort == "oldest" ? result.OrderBy(n => n.Date) : result.OrderByDescending(n => n.Date);
    return Results.Ok(result);
});

app.MapGet("/api/suggestions", (string? q) =>
{
    if (string.IsNullOrWhiteSpace(q)) return Results.Ok(Array.Empty<string>());
    var suggestions = notices.SelectMany(n => new[] { n.Device, n.Manufacturer, n.Category })
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .Where(s => s.Contains(q.Trim(), StringComparison.OrdinalIgnoreCase))
        .Take(6).ToArray();
    return Results.Ok(suggestions);
});

app.MapGet("/api/notices/{id:int}/comments", (int id) =>
{
    if (!notices.Any(n => n.Id == id)) return Results.NotFound();
    lock (gate) return Results.Ok(comments.Where(c => c.NoticeId == id).OrderBy(c => c.CreatedAt).ToArray());
});

app.MapPost("/api/notices/{id:int}/comments", (int id, NewComment request) =>
{
    if (!notices.Any(n => n.Id == id)) return Results.NotFound();
    var name = request.Name?.Trim();
    var message = request.Message?.Trim();
    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(message) || name.Length > 50 || message.Length > 600)
        return Results.BadRequest(new { error = "Enter a name (up to 50 characters) and a comment (up to 600 characters)." });
    lock (gate)
    {
        var item = new Comment(comments.Count == 0 ? 1 : comments.Max(c => c.Id) + 1,
            id, name, message, DateTimeOffset.UtcNow);
        var updated = new List<Comment>(comments) { item };
        var temp = commentFile + ".tmp";
        try
        {
            File.WriteAllText(temp, JsonSerializer.Serialize(updated, new JsonSerializerOptions { WriteIndented = true }));
            File.Move(temp, commentFile, true);
            comments = updated;
        }
        catch (IOException)
        {
            return Results.Problem("Comment could not be saved. Please try again.", statusCode: 503);
        }
        return Results.Created($"/api/notices/{id}/comments", item);
    }
});

app.Run();

record Notice(int Id, string Device, string Manufacturer, string Category, DateOnly Date,
    string Risk, string Issue, string Summary, string Finding);
record NewComment(string? Name, string? Message);
record Comment(int Id, int NoticeId, string Name, string Message, DateTimeOffset CreatedAt);
