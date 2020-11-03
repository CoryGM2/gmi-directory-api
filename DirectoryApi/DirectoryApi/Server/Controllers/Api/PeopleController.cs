using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DirectoryApi.DataAccess;
using DirectoryApi.Shared;

namespace DirectoryApi.Server.Controllers.Api
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private string _baseRoute = "api/people";
        private readonly IRepository _repository;
        private readonly ILogger _logger; 

        public PeopleController(IRepository repository, ILogger<PeopleController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Person>> GetAsync(int? id)
        {
            var entity = default(Person);

            try
            {
                entity = await _repository.GetAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var baseMsg = $"Error in GET: {_baseRoute}/{{id}}. ";
                _logger.LogError(ex, baseMsg);
                return StatusCode(500, baseMsg + "Check the log for details.");
            }

            if (entity == null)
                return NotFound(entity);
            else
                return Ok(entity);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllAsync()
        {
            var entities = default(IEnumerable<Person>);

            try
            {
                entities = await _repository.GetAllAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var baseMsg = $"Error in GET: {_baseRoute}/. ";
                _logger.LogError(ex, baseMsg);
                return StatusCode(500, baseMsg + "Check the log for details.");
            }

            if (entities == null)
                entities = new List<Person>();

            return Ok(entities);
        }
    }
}
