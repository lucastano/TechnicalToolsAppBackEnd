using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoService.ApiRest.DTOs;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IAgregarClienteUC agregarClienteUC;
        private readonly IObtenerTodosLosClientesUC obtenerClientesUC;
        public ClientesController(IAgregarClienteUC agregarClienteUC, IObtenerTodosLosClientesUC obtenerClientesUC)
        {
            this.agregarClienteUC = agregarClienteUC;
            this.obtenerClientesUC = obtenerClientesUC;

        }


        [HttpPost]

        public IActionResult AltaCliente(AgregarClienteDTO dto)
        {
            //TODO:VER VALIDAR PASSWORD
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos invalidos");
            }

            try
            {
                Seguridad.CrearPasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                //TODO: ver controles necesarios en crear el cliente
                Cliente cli = new Cliente()
                {
                    Nombre=dto.Nombre,
                    Apellido=dto.Apellido,
                    Telefono=dto.Telefono,
                    Email=dto.Email,
                    Direccion=dto.Direccion,
                    Ci=dto.Ci,
                    PasswordHash=passwordHash,
                    PasswordSalt=passwordSalt

                };
                agregarClienteUC.Ejecutar(cli);
                return Ok();


            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
            
        }
        [HttpGet]

        public IEnumerable<ClienteDTO> GetClientes()
        {
            var clientes = obtenerClientesUC.Ejecutar();
            return clientes.Select(c => new ClienteDTO()
            {
                Id=c.Id,
                Nombre= c.Nombre,
                Apellido= c.Apellido,
                Telefono= c.Telefono,
                Direccion = c.Direccion,
                Email= c.Email,
                Ci= c.Ci

            });

        }
    }
}
