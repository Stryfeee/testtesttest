using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApplication3.ViewModels;
using static System.Net.WebRequestMethods;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Login LModel { get; set; }
		private readonly SignInManager<IdentityUser> signInManager;
		public LoginModel(SignInManager<IdentityUser> signInManager)
		{
			this.signInManager = signInManager;
		}
		public void OnGet()
        {
        }
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid) //if true, user have entered the stuff properly, according to annotations in the parent file
			{
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
				LModel.RememberMe, false);
				if (identityResult.Succeeded)
				{
					//Create the security context
					var claims = new List <Claim> {
						new Claim(ClaimTypes.Name, "c@c.com" ),
						new Claim(ClaimTypes.Email, "c@c.com" ),

                        new Claim("Department", "HR")

                        };
					var i = new ClaimsIdentity(claims, " MyCookieAuth");
					ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
					await HttpContext.SignInAsync(" MyCookieAuth", claimsPrincipal);
					return RedirectToPage("Index");
				}
                if (identityResult.IsLockedOut)
                {
                    // Handle locked-out user
                    // You can redirect to a page informing the user about the lockout
                    return RedirectToPage("Lockout");
                }
                else
                {
                    // Invalid login attempt
                    ModelState.AddModelError("", "Username or Password incorrect");
                    return Page();
                }

                
			}
			return Page();
		}
	}
}
