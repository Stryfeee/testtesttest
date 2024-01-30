using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Model;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> userManager;

    public IndexModel(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

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
            }
        }
    }
}
