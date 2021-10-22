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
    [Route("api/")]
    public class ClassificationController : ControllerBase
    {
        private readonly ILogger<ClassificationController> _logger;

        public ClassificationController(ILogger<ClassificationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("classifyimage")]
        public IActionResult ClassifyImage(IFormFile imageFile)
        {
            // validate image file, handle exceptions
            string FileValidity = FileValidation.VerifyInputFile(imageFile);
            if (FileValidity != "ok")
            {
                _logger.LogInformation("Invalid file uploaded: " + FileValidity);
                return BadRequest(FileValidity);
            }

            // preprocess image
            // create a tensor
            // run model, get classifications
            // return response
            return Ok();
        }
    }
}
