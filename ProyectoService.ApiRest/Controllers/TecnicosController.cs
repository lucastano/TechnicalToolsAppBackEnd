using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoService.ApiRest.DTOs;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicosController : ControllerBase
    {
        private readonly IAgregarTecnico agregarTecnicoUc;
        private readonly IObtenerTodosLosTecnicos obtenerTodosLosTecnicosUc;
        private readonly IObtenerTecnicoPorEmail obtenerTecnicoPorEmailUc;

        public TecnicosController(IAgregarTecnico agregarTecnicoUc, IObtenerTodosLosTecnicos obtenerTodosLosTecnicosUc, IObtenerTecnicoPorEmail obtenerTecnicoPorEmailUc)
        {
            this.agregarTecnicoUc = agregarTecnicoUc;
            this.obtenerTodosLosTecnicosUc = obtenerTodosLosTecnicosUc;
            this.obtenerTecnicoPorEmailUc = obtenerTecnicoPorEmailUc;
        }

        [HttpPost]

        public ActionResult<ResponseAgregarTecnicoDTO> AgregarTecnico(AgregarTecnicoDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(400);
            }
            try
            {
                Seguridad.CrearPasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                Tecnico tecnico = new Tecnico()
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Email = dto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                agregarTecnicoUc.Ejecutar(tecnico);
                ResponseAgregarTecnicoDTO response = new ResponseAgregarTecnicoDTO()
                {
                    statusCode = 201,
                    Tecnico = dto

                };

                return response;

            }
            catch (Exception ex)
            {
                return StatusCode(500);

            }

        }

        [HttpGet]

        public ActionResult ObtenerClientes()
        {

        }
    }
}
