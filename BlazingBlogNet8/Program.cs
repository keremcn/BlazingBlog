using BlazingBlogNet8.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<UserService>()
                .AddTransient<CategoryService>()
                .AddTransient<BlogPostService>();

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<BlogAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(serviceProvider =>
    serviceProvider.GetRequiredService<BlogAuthenticationStateProvider>());

//var blogConnectionString = builder.Configuration.GetConnectionString("Blog");
var blogConnectionString = builder.Configuration.GetConnectionString("BlogLite");

//builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(blogConnectionString), ServiceLifetime.Transient);
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlite(blogConnectionString), ServiceLifetime.Transient);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
