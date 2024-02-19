
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// https://learn.microsoft.com/vi-vn/aspnet/core/security/cors
const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                      builder.WithOrigins("*")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
                      //.AllowCredentials();
                    });
});

var app = builder.Build();

// Enable Cross-Origin for APIs
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

// Redirect to HTTPS
//if (!app.Environment.IsDevelopment())
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();


#region APIs Mapping.

// Minimal APIs
app.MapGet("api", () => "Hello World!\nMinimal APIs!");

#endregion


app.MapFallbackToPage("/_Host");

app.Run();