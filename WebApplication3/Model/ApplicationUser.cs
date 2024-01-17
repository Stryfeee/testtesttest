using Microsoft.AspNetCore.Identity;
namespace WebApplication3.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreditCard {  get; set; }
        public int MobileNo { get; set; }
        public string BillingAddr { get; set; }
        public string ShippingAddr { get; set; }
        public string Email { get; set; }
      
    }
}
