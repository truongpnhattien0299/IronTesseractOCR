using System;
using System.IO;
using System.Threading.Tasks;
using IronOcr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoogleOcr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpPost]
        public IActionResult Index(IFormFile File)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", File.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    File.CopyTo(stream);
                }
                var Ocr = new IronTesseract();
                Ocr.Language = OcrLanguage.Vietnamese;
                using (var input = new OcrInput())
                {
                    input.AddPdf("wwwroot/uploads/" + File.FileName);
                    var Result = Ocr.Read(input);
                    Result.SaveAsTextFile("wwwroot/outputs/" + File.FileName.Split(".")[0] + ".txt");
                    return Ok(Result);
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }



        // public IActionResult NoName(string base64)
        // {
        //     var fileParts = base64.Split(',').ToList<string>();
        //     byte[] fileBytes = Convert.FromBase64String(fileParts[1]);
        //     using (var client = new WebClient())
        //     {
        //         MainRequests mainRequests = new MainRequests()
        //         {
        //             requests = new List<requests>()
        //             {
        //                 new requests()
        //                 {
        //                     image = new image()
        //                     {
        //                         content = fileParts[1]
        //                     },
        //                     features = new List<features>()
        //                     {
        //                         new features()
        //                         {
        //                             type = "LABEL_DETECTION",
        //                         }
        //                     }
        //                 }
        //             }
        //         };
        //         var api_key = "Google Cloud Vision API";
        //         var uri = "https://vision.googleapis.com/v1/images:annotate?key=" + api_key;
        //         client.Headers.Add("Content-Type:application/json");
        //         client.Headers.Add("Accept:application/json");
        //         var response = client.UploadString(uri, JsonConvert.SerializeObject(mainRequests));
        //         return Json(data: response);
        //     }
        // }
    }
}
