using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using System.Runtime.InteropServices;

namespace WebApplication3.Pages
{
    [Authorize(Policy = " MustBelongToHRDepartment ", AuthenticationSchemes = " MyCookieAuth")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
