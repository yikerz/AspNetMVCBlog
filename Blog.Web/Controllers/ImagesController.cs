using Blog.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        /* 119. Create constructor taking IImageRepo */
        private readonly IImageRepository imageRepo;
        public ImagesController(IImageRepository imageRepo)
        {
            this.imageRepo = imageRepo;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            /* 120. Upload file */
            var imageURL = await imageRepo.UploadAsync(file);
            /* 121. Return Json object or Problem */
            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult( new { link = imageURL });
        }

    }
}
