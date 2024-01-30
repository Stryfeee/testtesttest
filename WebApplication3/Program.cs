using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebApplication3.Model;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = "/Login";
});


// Add reCAPTCHA configuration
builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.SignIn.RequireConfirmedEmail = true; // Optional: Require email confirmation for 2FA
    options.SignIn.RequireConfirmedPhoneNumber = false; // Optional: Require phone number confirmation for 2FA
    options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider; // Use default provider for authenticator tokens
    options.Tokens.ChangePhoneNumberTokenProvider = TokenOptions.DefaultPhoneProvider; // Use default provider for changing phone number
});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(10);
	options.Cookie.Name = ".MySampleMVCWeb.Session";
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
   
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePagesWithRedirects("/errors/{0}");
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
