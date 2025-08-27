using io.harness.cfsdk.client.api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var apiKey = builder.Configuration["FeatureFlags:SDKKey"];

// Initialize the SDK client (await to remove the warning)
await CfClient.Instance.Initialize(apiKey, Config.Builder().Build());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
