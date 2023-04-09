using Microsoft.Extensions.Azure;
using RazorPagesPizza.Services;
using Microsoft.Azure.Cosmos;
using Azure.Core;
using RazorPagesPizza;
using Azure.Identity;
using RazorPagesPizza.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<PizzaService>();

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddQueueServiceClient(builder.Configuration.GetValue<Uri>("StorageUrl"));
    clientBuilder.AddClient((CosmosOptions options, TokenCredential credential) =>
    {
        return new CosmosClient(options.AccountEndpoint, credential, new CosmosClientOptions
        {
            SerializerOptions = new()
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
            },
        });
    }).ConfigureOptions(builder.Configuration.GetSection(nameof(CosmosOptions)));

    clientBuilder.UseCredential(new DefaultAzureCredential());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
