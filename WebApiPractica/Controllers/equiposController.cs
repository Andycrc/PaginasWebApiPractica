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
            //comentario
        
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

         [HttpGet]
         [Route("getbyid")]
         public IActionResult Get(int id)
         {
            equipos? equipo = _equiposContexto.equipos.Find(id);
            if(equipo == null) { return NotFound(); }
            return Ok(equipo);

         }
        
      

        //
        [HttpGet]
        [Route("find")]
        public IActionResult buscar(string filtro){
            List<equipos> equiposList = (from e in _equiposContexto.equipos
                                            where e.nombre.Contains(filtro)
                                            || e.descripcion.Contains(filtro)
                                            select e).ToList();

            if(equiposList.Any())
            {
                return Ok(equiposList);
            }
            return NotFound();
            
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] equipos equiposNuevo)
        {
            try
            {
                _equiposContexto.equipos.Add(equiposNuevo);
                _equiposContexto.SaveChanges(); 

                return Ok(equiposNuevo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody]equipos equiposModificar)
        {
            equipos? equiposExiste = (from e in _equiposContexto.equipos
                                      where e.id_equipos == id
                                      select e).FirstOrDefault();
            if(equiposExiste == null)
                return NotFound();

            equiposExiste.nombre = equiposModificar.nombre;
            equiposExiste.descripcion = equiposModificar.descripcion;

            _equiposContexto.Entry(equiposExiste).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(equiposExiste);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarEquipos(int id)
        {
            equipos? equiposExiste = (from e in _equiposContexto.equipos
                                      where e.id_equipos == id
                                      select e).FirstOrDefault();

            if (equiposExiste == null) return NotFound();
            equiposExiste.estado = "E";
            _equiposContexto.Entry(equiposExiste).State = EntityState.Modified; 

            //_equiposContexto.equipos.Attach(equiposExiste);
            //_equiposContexto.equipos.Remove(equiposExiste);
            _equiposContexto.SaveChanges();

            return Ok(equiposExiste);
        }
    }

   

}
