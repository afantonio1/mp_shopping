using MpShopping.Web.Utils;
using MpShopping.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MpShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace MpShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {            
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var model = await _productService.FindProductById(id);
            if (model != null)
                return View(model);

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(model);
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var model = await _productService.FindProductById(id);
            if (model != null)
                return View(model);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel model)
        {
            var response = await _productService.DeleteProductById(model.Id);
            if (response) return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
    }
}
