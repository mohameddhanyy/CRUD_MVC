using System;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class ProductsController : Controller
    {

        public IActionResult GetProductDetails(int productId)
        {
            //ContentResult result = new ContentResult();
            //result.Content = $"Content of Product {productId} is bla bla bla";
            return Content($"Content of Product {productId} is bla bla bla");
        }

        public IActionResult RedirectToURL()
        {
            //RedirectResult result =new RedirectResult("https://localhost:44332/Products/GetProductDetails");
            return Redirect("https://localhost:44332/Products/GetProductDetails");
        }

        public IActionResult RedirectToAction()
        {
            return RedirectToAction(nameof(ProductsController.GetProductDetails), new { productId = 9 });
        }

        public IActionResult getproduct(int id, string name, Employyee e)
        {
            return Content("hello");
        }
    }
}
