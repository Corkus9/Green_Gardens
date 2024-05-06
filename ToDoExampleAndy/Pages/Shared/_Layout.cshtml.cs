using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using Microsoft.IdentityModel.Tokens;

namespace ToDoExampleAndy.Pages
{
    public class LayoutModel: PageModel
    {
        public void OnGet()
        {
            Claim[] DataGet = HttpContext.User.Claims.ToArray();
            if (/*DataGet.IsNullOrEmpty() ||*/ DataGet[3].Value != "Admin")
            {
                Response.Redirect("/");
                return;
            }

            ViewData["FName"] = DataGet[0].Value;
            ViewData["Admin"] = DataGet[3].Value;
        }
    }
}