using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IConfiguration? _configuration;

        public CategoriasController(IConfiguration configuration)
        {
                _configuration = configuration;
        }

    }
}
