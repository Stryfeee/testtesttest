using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

public class ShowUserDetailsModel : PageModel
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IDataProtectionProvider dataProtectionProvider;

    public ShowUserDetailsModel(UserManager<ApplicationUser> userManager, IDataProtectionProvider dataProtectionProvider)
    {
        this.userManager = userManager;
        this.dataProtectionProvider = dataProtectionProvider;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CreditCard { get; set; }
    public string PhoneNumber { get; set; }
    public string BillingAddr { get; set; }
    public string ShippingAddr { get; set; }
    public string Email { get; set; }

    public string pfp { get; set; }

    public void OnGet()
    {
        var userId = userManager.GetUserId(User);
        if (userId != null)
        {
            var user = userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                FirstName = user.FirstName;
                LastName = user.LastName;

                // Decrypt credit card details ask in class
                /*var dataProtector = dataProtectionProvider.CreateProtector("mysecretkey");
                CreditCard = dataProtector.Unprotect(user.CreditCard);*/
                CreditCard = user.CreditCard;
				PhoneNumber = user.PhoneNumber;
                BillingAddr = user.BillingAddr;
                ShippingAddr = user.ShippingAddr;
                Email = user.Email;
                pfp = user.pfp;
            }
        }
    }
}
