using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

//Add-Migration AddProductTable
//Update-Database
namespace ToDoExampleAndy.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<ProductModel> Products { get; set; }

        public ProductsModel (AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Products = _db.Products.ToList();

            Claim[] DataGet = HttpContext.User.Claims.ToArray();
            if(DataGet[0] != null)
            {
                ViewData["Fname"] = DataGet[0].Value;
                ViewData["Admin"] = DataGet[3].Value;
            }
            else
            {
                Response.Redirect("/Login");
                return;
            }
        }
    }
}
