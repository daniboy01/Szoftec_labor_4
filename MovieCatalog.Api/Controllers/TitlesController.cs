using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Service.Dto;
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
        public async Task<IActionResult> Post([FromBody] TitleDto dto)
        {
            try
            {
                var title = await service.SaveOrUptdateTitleAsync(null, dto);
                return Created("/api/titles/"+ title.Id, title);
            }
            catch(Exception e)
            {
                return Conflict("Title already exist by name: " + dto.PrimaryTitle);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TitleDto dto)
        {
            try
            {
                var title = await service.SaveOrUptdateTitleAsync(id, dto);
                return NoContent();
            }
            catch(Exception e)
            {
                return NotFound("Title not found id: " + id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteTitleAsync(id);
            return NoContent();
        }
    }
}
