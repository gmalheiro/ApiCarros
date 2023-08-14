using CarrosAPI.EntityRepository;
using CarrosAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CarrosController(AppDbContext context)
        {
                _context = context;
        }

        [HttpGet]
        [Route("/ListarCarros")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarroModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CarroModel))]
        public IActionResult ListarCarrosEntity()
        {
            try
            {
                var carros = _context.Carros.AsNoTracking().ToList();

                if (carros is null)
                {
                    return NotFound("Carros não encontrados");
                }
                else
                {
                    return Ok(carros);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Ocorreu um erro ao tratar a sua solicitação");
            } 
        }


    }
}
