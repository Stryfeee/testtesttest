using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApplication3.ViewModels;
using WebApplication3.Model;
using static System.Net.WebRequestMethods;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Login LModel { get; set; }
		private readonly SignInManager<ApplicationUser> signInManager;
		public LoginModel(SignInManager<ApplicationUser> signInManager)
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
				LModel.RememberMe, true);
				if (identityResult.Succeeded)
				{
					
					return RedirectToPage("Index");
				}
                if (identityResult.IsLockedOut)
                {
                    // Handle locked-out user
                    // You can redirect to a page informing the user about the lockout
                    return RedirectToPage("Lockout");
                }
        
                // Invalid login attempt
                ModelState.AddModelError("", "Username or Password incorrect");
				foreach (var modelState in ModelState.Values)
				{
					foreach (var error in modelState.Errors)
					{
						Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
					}
				}



			}
			return Page();
		}
	}
}
