using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto) 
        {
            _equiposContexto= equiposContexto;
            //comentario hola
        
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerEquipos()
        {
            List<equipos> ListadoEquipos = (from e in _equiposContexto.equipos
                                            select e).ToList();

            if (ListadoEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoEquipos);
        }

    }

   

}
