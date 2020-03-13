using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PZCheeseria.Api.Models;
using PZCheeseria.BusinessLogic.Cheeses.Queries;

namespace PZCheeseria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheesesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CheesesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all cheeses
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheeseApiModel>>> Get()
        {
            var interimResult = await _mediator.Send(new GetAllCheesesQuery());
            var prependUrl = $"{Request.Scheme}://{Request.Host}/resources/images/";
            var result = interimResult.Select(m => CheeseApiModel.ConvertFrom(m, prependUrl)).ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]AddCheeseModel model)
        {
            try
            {
                var file = model.Image;
                var folderName = Path.Combine("Resources", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString();
                   
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
 
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
 
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return Problem("Could not add record");
            }
        }

        public class AddCheeseModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal PricePerKilo { get; set; }
            public string Colour { get; set; }
            public IFormFile Image { get; set; }
        }
    }
}