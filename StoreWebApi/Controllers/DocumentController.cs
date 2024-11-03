using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Services.Helper;

namespace StoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> UploadFile(IFormFile file,string folderName)
            => DocumentSettings.UploadFile(file, folderName);

        [HttpPost]
        public ActionResult<bool>Delete(string imageUrl,string folderName)
            =>DocumentSettings.DeleteFile(imageUrl, folderName);

         
    }
}
