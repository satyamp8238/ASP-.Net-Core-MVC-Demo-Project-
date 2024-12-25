using DemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext moAppDbContext;
        public HomeController(ILogger<HomeController> logger, AppDbContext foAppDbContext)
        {
            _logger = logger;
            moAppDbContext = foAppDbContext;
        }

        public IActionResult Index()
        {
            Products loProducts = new Products(moAppDbContext);
            List<Product> loProductList = new List<Product>();
            loProductList = loProducts.getProducts(null).ToList();

            return View(loProductList);
        }

        public IActionResult AddOrEditProduct(int fiId)
        {
            Products loProductBAL = new Products(moAppDbContext);
            Product loProduct = new Product();

            if (fiId != 0)
                loProduct = loProductBAL.getProducts(fiId).FirstOrDefault();

            return View(loProduct);
        }

        [HttpPost]
        public IActionResult SaveProduct(Product foProduct)
        {
            int liResult = 0;
            Products loProductBAL = new Products(moAppDbContext);

            if (foProduct != null)
            {
                liResult = loProductBAL.saveProduct(foProduct);

                if (liResult == 101)
                    TempData["SuccessMessage"] = "Product saved successfully!";
                else if (liResult == 102)
                    TempData["SuccessMessage"] = "Product updated successfully!";
                else
                    TempData["ErrorMessage"] = "An error occurred while saving the product.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int fiId)
        {
            int liResult = 0;
            Products loProductBAL = new Products(moAppDbContext);

            if (fiId != 0)
                liResult = loProductBAL.deleteProduct(fiId);

            if (liResult == 201)
                TempData["SuccessMessage"] = "Product deleted successfully!";
            else
                TempData["ErrorMessage"] = "An error occurred while delete the product.";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
