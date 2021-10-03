using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MovieCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly MovieCatalogService service;

        public TitlesController(MovieCatalogService service)
        {
            this.service = service;
        }
        //XWSW5U
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var title = await service.GetTitleById(id);
                return Ok(title);
            }
            catch(Exception e)
            {
                return NotFound($"Not found title by id: {id}");

            }

        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
