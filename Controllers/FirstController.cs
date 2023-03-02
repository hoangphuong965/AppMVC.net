using AppMvc.Net.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

namespace AppMvc.Net.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger _logger;
        private readonly ProductService _productService;

        public FirstController(ILogger<FirstController> logger, ProductService productService) 
        {
            _logger= logger;
            _productService= productService;    
        }

        public string Index()
        {
            // this.HttpContext
            // this.Request
            // this.Response
            // this.RouteData

            // this.User
            // this.ModelState
            // this.ViewData
            // this.ViewBag
            // this.Url
            // this TempData

            _logger.LogInformation("index action");
            _logger.LogWarning("");
            _logger.LogInformation("");
            return "Toi la FirstController index";
        }

        public void Nothing()
        {
            _logger.LogInformation("Nothing Action");
            Response.Headers.Add("hi", "Xin chao cac ban");
        }

        public IActionResult Brid()
        {
            string FilePath = Path.Combine(Startup.ContentRootPath, "FIles", "b.jpg");
            var bytes = System.IO.File.ReadAllBytes(FilePath);

            return File(bytes, "image/jpg");
        }
        
        public IActionResult IphonePrice()
        {
            return Json(
                new
                {
                    productName = "Iphone X",
                    Price = 1000 
                }
            );
        }

        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy", "Home");
            return LocalRedirect(url);
        }

        public IActionResult Google()
        {
            var url = "https://www.google.com/";
            return Redirect(url);
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(product => product.Id == id).FirstOrDefault();
            if (product == null)
            {
                //TempData["StatusMessage"] = "San pham ban yeu cau khong co";
                StatusMessage = "San pham ban yeu cau khong co";
                return Redirect(Url.Action("Index", "Home"));
            }

            // cách 1: truyền dữ liệu sang view bằng cách sử dụng model: thiết lập model cho view
            //return View(product);

            // cách 2: dùng viewdata
            //ViewData["product"] = product;
            //ViewData["Title"] = product.Name;
            //return View("ViewProduct2");

            //TempData["Thongbao"] = ""; cai nay su dung session

            ViewBag.Product = product;
            return View("ViewProduct3");
        }
    }

    
}
