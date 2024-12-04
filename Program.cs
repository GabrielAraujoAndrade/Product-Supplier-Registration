using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext com a string de conex�o
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar servi�os ao cont�iner
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure o pipeline de solicita��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
