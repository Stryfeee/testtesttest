using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http; // Add this namespace for handling file uploads
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
	public class RegisterModel : PageModel
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly ICaptchaValidator captchaValidator;

		[BindProperty]
		public Register RModel { get; set; }



		public RegisterModel(UserManager<ApplicationUser> userManager,
							  SignInManager<ApplicationUser> signInManager,
							  ICaptchaValidator captchaValidator)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.captchaValidator = captchaValidator;
		}

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync(string captcha)
		{
			if (!await captchaValidator.IsCaptchaPassedAsync(captcha))
			{
				ModelState.AddModelError("captcha", "Captcha validation failed");
			}
			if (ModelState.IsValid)
			{
				var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
				var protector = dataProtectionProvider.CreateProtector("mysecretkey");
				

				var user = new ApplicationUser()
				{
					UserName = RModel.Email,
					Email = RModel.Email,
					FirstName = RModel.FirstName,
					LastName = RModel.LastName,
					CreditCard = protector.Protect(RModel.CreditCard),
					BillingAddr = RModel.BillingAddr,
					ShippingAddr = RModel.ShippingAddr,
					PhoneNumber = RModel.PhoneNumber,
					pfp = RModel.pfp
				};

				
				var result = await userManager.CreateAsync(user, RModel.Password);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, false);
					return RedirectToPage("Index");
				}
				foreach (var error in result.Errors)
				{
					Console.WriteLine("hereeeeeeeeeee");
					ModelState.AddModelError("", error.Description);
				}
			}
			return Page();
		}
	}
}
