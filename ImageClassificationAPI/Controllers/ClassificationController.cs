using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageClassificationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassificationController : ControllerBase
    {
        private readonly ILogger<ClassificationController> _logger;

        public ClassificationController(ILogger<ClassificationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult ClassifyImage(IFormFile imageFile)
        {
            // validate image file, handle exceptions
            // preprocess image
            // create a tensor
            // run model, get classifications
            // return response
            return Ok();
        }
    }
}
