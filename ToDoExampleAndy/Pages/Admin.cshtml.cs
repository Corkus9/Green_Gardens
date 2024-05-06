using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using Microsoft.IdentityModel.Tokens;

namespace ToDoExampleAndy.Pages
{
    public class AdminModel : PageModel
    {

        private readonly AppDbContext _db;

        // A private field for logging. ILogger is used for logging different types of information.
        private readonly ILogger<AdminModel> _logger;

        // Public property for Username. This can be accessed and modified from outside the class.
        public string Username { get; set; }

        public ProductsModel Item { get; set; }

        public List<ProductModel> Products { get; set; }

      

        // Public property for a list of TaskModel items. This stores the list of tasks.
        public List<TaskModel> Items { get; set; }

        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;

        // Constructor for the IndexModel class.
        // It takes an ILogger and an instance of AppDbContext as parameters.
        public AdminModel(ILogger<AdminModel> logger, AppDbContext _db)
        {
            // Assign the logger instance to the private field _logger.
            _logger = logger;

            // Assign the database context instance to the private field _dbConnection.
            _dbConnection = _db;
        }

        // OnGet method that is called when the page is accessed.
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

            Products = _dbConnection.Products.ToList();


            // Retrieve all tasks from the database and assign them to the Items property.
            Items = _dbConnection.Tasks.ToList(); 
        
            
            // Set the Username property to "DefaultUser".
            // This could be replaced with dynamic data in a real application.
            Username = "DefaultUser";
        
        }

        public async void UpdateQuantity(int NewQuant, int Guid)

        {
            var itemToUpdate = await _dbConnection.Products.FindAsync(Guid);

            itemToUpdate.Quantity = NewQuant; 

            await _dbConnection.SaveChangesAsync();

     
        }
    }
}


