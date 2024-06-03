using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoService.ApiRest.DTOs;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly IAgregarReparacion agregarReparacionUc;
        private readonly IObtenerTodasLasReparaciones obtenerTodasLasReparacionesUc;
        private readonly IObtenerReparacionesPorCliente obtenerReparacionesPorClienteUc;
        private readonly IObtenerReparacionesPorCliente obtenerReparacionesPorTecnicoUc;
        private readonly IPresupuestarReparacion presupuestarReparacionUc;
        private readonly IAgregarClienteUC agregarClienteUc;
        private readonly IObtenerClientePorCI obtenerClientePorCiUc;
        private readonly IObtenerTecnicoPorId obtenerTecnicoPorIdUc;


        public ReparacionesController(IAgregarReparacion agregarReparacionUc, IObtenerTodasLasReparaciones obtenerTodasLasReparacionesUc, IObtenerReparacionesPorCliente obtenerReparacionesPorClienteUc, IObtenerReparacionesPorCliente obtenerReparacionesPorTecnicoUc, IPresupuestarReparacion presupuestarReparacionUc, IObtenerClientePorCI obtenerClientePorCiUc, IObtenerTecnicoPorId obtenerTecnicoPorIdUc)
        {
            this.agregarReparacionUc = agregarReparacionUc;
            this.obtenerTodasLasReparacionesUc = obtenerTodasLasReparacionesUc;
            this.obtenerReparacionesPorClienteUc = obtenerReparacionesPorClienteUc;
            this.obtenerReparacionesPorTecnicoUc = obtenerReparacionesPorTecnicoUc;
            this.presupuestarReparacionUc = presupuestarReparacionUc;
            this.obtenerClientePorCiUc = obtenerClientePorCiUc;
            this.obtenerTecnicoPorIdUc = obtenerTecnicoPorIdUc;
        }

        [HttpPost]

        public async Task<ActionResult> NuevaReparacion(NuevaReparacionDTO dto)
        {
            if (!ModelState.IsValid) throw new Exception("Debe ingresar datos de la reparacion");
            try
            {
                //PARA OBTENER EL TECNICO UTILIZO DATOS DE LA SESSION
                Tecnico tecnico = await obtenerTecnicoPorIdUc.Ejecutar(dto.IdTecnico);
                if (tecnico == null) throw new Exception("Tecnico no existe");
                //el ccliente debe existir, en la realidad, no se van a agregar clientes individualmente,
                //sino que con cada servicio, se ingresan los datos del cliente y ahi se agrega
                // la idea es al crear un nuevo servicio, ingresar LA CI DEL CLIENTE, lo busca por ci, si encuentra, retorna los datos del cliente.
                // si no, pide que ingrese los datos, TODO ESO LO MANEJA EL FRONT.
                // ESTE ENDPOINT REQUIERE QUE EL FRONT, LE DE LA CI DEL CLIENTE EXISTENTE.  
                Cliente cliente = await obtenerClientePorCiUc.Ejecutar(dto.CiCliente);
                Reparacion reparacion = new Reparacion()
                {
                    Tecnico = tecnico,
                    Cliente = cliente,
                    Producto = dto.Producto,
                    NumeroSerie = dto.NumeroSerie,
                    Descripcion = dto.Descripcion,

                };

                await agregarReparacionUc.Ejecutar(reparacion);

                return Ok();




            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            
        }
        [HttpGet]
        public async Task<ActionResult<ResponseReparacionesEnTallerDTO>> ObtenerReparacionesEnTaller()
        {
            try
            {
                var reparaciones = await obtenerTodasLasReparacionesUc.Ejecutar();
                IEnumerable<ReparacionEnTallerDTO> rep = reparaciones.Select(r => new ReparacionEnTallerDTO()
                {
                    Id = r.Id,
                    ClienteNombre = r.Cliente.Nombre,
                    ClienteApellido = r.Cliente.Apellido,
                    ClienteTelefono = r.Cliente.Telefono,
                    ClienteDireccion = r.Cliente.Direccion,
                    ClienteEmail = r.Cliente.Email.Value,
                    ClienteCedula=r.Cliente.Ci,
                    Producto = r.Producto,
                    NumeroSerie = r.NumeroSerie,
                    Descripcion = r.Descripcion,
                    Fecha = r.Fecha
                    

                });
                ResponseReparacionesEnTallerDTO response = new ResponseReparacionesEnTallerDTO()
                {
                    StatusCode = 200,
                    reparaciones = rep.ToList()

                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseReparacionesEnTallerDTO response = new ResponseReparacionesEnTallerDTO()
                {
                    StatusCode = 500,
                    reparaciones = null

                };
                return BadRequest(response);
            }


        }
    }
}
