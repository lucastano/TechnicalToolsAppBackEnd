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

        public ActionResult<ResponseAgregarClienteDTO> AltaCliente(AgregarClienteDTO dto)
        {
            //TODO:VER VALIDAR PASSWORD
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos invalidos");
            }

            try
            {
                //passwordhash y salt no se crea con una password, ya que el cliente lo agrega el mismo tecnico, entonce se utiliza 
                // la cedula del cliente para autogenerar el passwordhash y salt
                //el cliente a futuro puede cambiar el password
                Seguridad.CrearPasswordHash(dto.Ci, out byte[] passwordHash, out byte[] passwordSalt);
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
                ResponseAgregarClienteDTO response = new ResponseAgregarClienteDTO()
                {
                    StatusCode = 201,
                    Cliente = dto
                };
                return response;


            }
            catch(Exception ex)
            {
                ResponseAgregarClienteDTO response = new ResponseAgregarClienteDTO()
                {
                    StatusCode = 500,
                    Cliente = null
                };
                return StatusCode(500);
            }
            
        }
        [HttpGet]

        public  ActionResult<ResponseGetClientesDTO> GetClientes()
        {
            try
            {
                var clientes =  obtenerClientesUC.Ejecutar();
                IEnumerable<ClienteDTO> cli = clientes.Select(c => new ClienteDTO()
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Telefono = c.Telefono,
                    Direccion = c.Direccion,
                    Email = c.Email,
                    Ci = c.Ci

                });
                ResponseGetClientesDTO response = new ResponseGetClientesDTO()
                {
                   StatusCode = 200,
                   Clientes=cli.ToList(),

                };
                return response;
            }
            catch (Exception ex)
            {
                ResponseGetClientesDTO response = new ResponseGetClientesDTO()
                {
                    StatusCode = 200,
                    Clientes = null

                };
                return StatusCode(500);
            }


        }
    }
}
