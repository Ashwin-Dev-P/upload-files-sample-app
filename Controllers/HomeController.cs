using FileUploadSampleApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileUploadSampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        [HttpGet]
        public IActionResult FileUpload()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult FileUpload(List<IFormFile> files)
        {
            ViewBag.error = "";
            ViewBag.success = "";
            try
            {
                
                if (files.Count == 0)
                {
                    ViewBag.error = "Please select a file";
                    return View();
                }
                var fileName = this.Request.Form.Files[0].FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\", fileName);
                
                using (var stream = System.IO.File.Create(filePath))
                {
                     this.Request.Form.Files[0].CopyTo(stream);
                }
                ViewBag.success = "File uploaded";
               
            }
            catch (Exception exception)
            {
                Console.WriteLine( exception.Message);
                ViewBag.error = "Error uploading the file";
                
            }
            return View();

           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}