using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Data;
using MovieCatalog.Data.Migrations;
using MovieCatalog.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly MovieCatalogService service;

        public GenresController(MovieCatalogService service)
        {
            this.service = service;
        }

        [HttpGet]//XWSW5U
        public async Task<IActionResult> Get()
        {
            return Ok(await service.GetAllGenresAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var genre =  await service.GetGenreAsync(id);

            if (genre == null)
                return NotFound("Not found by Id :" + id);

            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name)
        {
            try
            {
                var genre = await service.CreateNewGenreAsync(name);
                return Created("/api/genres/" + genre.Id, genre);
            }
            catch(Exception e)
            {
                return Conflict("Category with name : " + name + " alerady exist!");

            }
        }
        //XWSW5U
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string name)
        {
            try
            {
                var updatedGenre = await service.InserOrUpdateGenreAsync(id, name);

            }
            catch(Exception e)
            {
                if (e is ObjectNotFoundException)
                {
                    return NotFound("Not found by id: " + id);
                }
                else
                {
                    return Conflict("Category with name : " + name + " alerady exist!");

                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteGenreAsync(id);
            return NoContent();
        }
    }
}
