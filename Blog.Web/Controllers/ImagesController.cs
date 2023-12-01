/* 108. Create API controller */
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        /* 109. Create UploadAsync action method (POST) */
        [HttpPost]
        public Task<IActionResult> UploadAsync(IFormFile file)
        {

        }

    }
}
