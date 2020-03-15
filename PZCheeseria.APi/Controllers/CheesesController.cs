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
using PZCheeseria.BusinessLogic.Cheeses.Commands;
using PZCheeseria.BusinessLogic.Cheeses.Queries;
using PZCheeseria.BusinessLogic.Exceptions;

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
        /// 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheeseApiModel>>> Get()
        {
            var interimResult = await _mediator.Send(new GetAllCheesesQuery());
            
            //we are assuming here that image file is already there. In production level code, we would be checking it
            // and not really storing images in resources.
            var prependUrl = $"{Request.Scheme}://{Request.Host}/resources/images/";
            var result = interimResult.Select(m => CheeseApiModel.ConvertFrom(m, prependUrl)).ToList();
            return Ok(result);
        }

        /// <summary>
        /// Get cheese by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CheeseApiModel>> GetById(int id)
        {
            var interimResult = await _mediator.Send(new GetCheeseByIdQuery() {Id = id});
            var prependUrl = $"{Request.Scheme}://{Request.Host}/resources/images/";
            var result = CheeseApiModel.ConvertFrom(interimResult, prependUrl);
            return Ok(result);
        }

        /// <summary>
        /// Add new cheese item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] AddCheeseApiModel model)
        {
            //null model check could be built into a global filter
            if (model == null)
            {
                return BadRequest();
            }

            var file = model.Image;
            var folderName = Path.Combine("Resources", "images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            ValidateFile(file);

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString();

            var command = model.ToAddCheeseCommand();
             var id =await _mediator.Send(command);

            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return CreatedAtAction(nameof(GetById),new{id=id},string.Empty);
        }

        /// <summary>
        /// This method validates file. In Production, it should be doing lot more such as checking file extensions,
        /// virus scanning.
        /// If given time, I would like to add functional approach to it and return ExceptionResult or the file and let caller decide what to do
        /// </summary>
        /// <param name="file"></param>
        /// <exception cref="UnProcessableEntityException"></exception>
        private void ValidateFile(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new UnProcessableEntityException("Please see Errors and Property Errors for more information")
                {
                    ModelStateErrors = new[]
                    {
                        new ModelStateError
                        {
                            PropertyName = nameof(AddCheeseApiModel.Image),
                            Error = "Must provide an image file"
                        }
                    }
                };
            }
        }
    }
}