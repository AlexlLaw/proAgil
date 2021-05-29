using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.Repository;
using ProAgil.Domain;

namespace ProAgil.API3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ProAgilContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,  ProAgilContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Get api/WeatherForecast
        [HttpGet]
        public async Task<IActionResult> Get()
        {
          try {
              var results = await _context.Eventos.ToListAsync();

              return Ok(results);
          }
          catch (System.Exception)
          {
              return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
          }
        }

       [HttpGet("{id}")]
       public async Task<IActionResult> Get(int id)
       {
          try {
             var results = await _context.Eventos.FirstOrDefaultAsync(x => x.Id == id);;

             return Ok(results);
          }
          catch (System.Exception)
          {
              return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
          }
       }
    }
}
