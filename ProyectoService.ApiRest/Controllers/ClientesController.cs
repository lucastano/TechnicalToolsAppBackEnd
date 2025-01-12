using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoService.ApiRest.DTOs;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;
using ProyectoService.LogicaNegocio.Modelo.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoService.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]


    public class ClientesController : ControllerBase
    {
        private readonly IAgregarClienteUC agregarClienteUC;
        private readonly IObtenerTodosLosClientesUC obtenerClientesUC;
        private readonly IObtenerClientePorCI obtenerClientePorCiUC;
        public ClientesController(IAgregarClienteUC agregarClienteUC, IObtenerTodosLosClientesUC obtenerClientesUC, IObtenerClientePorCI obtenerClientePorCiUC)
        {
            this.agregarClienteUC = agregarClienteUC;
            this.obtenerClientesUC = obtenerClientesUC;
            this.obtenerClientePorCiUC = obtenerClientePorCiUC;
        }


        [HttpPost]

        public async Task<ActionResult<ResponseAgregarClienteDTO>> AltaCliente(AgregarClienteDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception("Datos ingresados no validos");
                Seguridad.CrearPasswordHash(dto.Ci, out byte[] passwordHash, out byte[] passwordSalt);
                Cliente cliPost = new Cliente()
                {
                    Nombre=dto.Nombre,
                    Apellido=dto.Apellido,
                    Telefono=dto.Telefono,
                    Email=EmailVO.Crear(dto.Email),
                    Direccion=dto.Direccion,
                    Ci=dto.Ci,
                    PasswordHash=passwordHash,
                    PasswordSalt=passwordSalt

                };
                Cliente cli = await agregarClienteUC.Ejecutar(cliPost);
                if (cli == null) throw new Exception("No se pudo agregar cliente");
                ResponseAgregarClienteDTO response = new ResponseAgregarClienteDTO()
                {
                    Id = cli.Id,
                    Nombre = cli.Nombre,
                    Apellido=cli.Apellido,
                    Telefono=cli.Telefono,
                    Direccion = cli.Direccion,
                    Email = cli.Email.Value,
                    Ci = cli.Ci

                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                
                return BadRequest(new { success = false, message = ex.Message });
            }
            
        }
        [HttpGet]

        public async Task <ActionResult<ResponseGetClientesDTO>> GetClientes()
        {
            try
            {
                var clientes = await obtenerClientesUC.Ejecutar();
                IEnumerable<ClienteDTO> cli = clientes.Select(c => new ClienteDTO()
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Telefono = c.Telefono,
                    Direccion = c.Direccion,
                    Email = c.Email.Value,
                    Rol=c.Rol,
                    Ci = c.Ci

                });
                ResponseGetClientesDTO response = new ResponseGetClientesDTO()
                {
                   
                   Clientes=cli.ToList(),

                };
                return response;
            }
            catch (Exception ex)
            {
                ResponseGetClientesDTO response = new ResponseGetClientesDTO()
                {
                    
                    Clientes = null

                };
                return BadRequest(response);
            }


        }

        [HttpGet("ObtenerClientePorCi")]

        public async Task<ActionResult<ResponseObtenerClientePorCiDTO>> ObtenerClientePorCi(string ci)
        {
            try
            {
                Cliente cliente = await obtenerClientePorCiUC.Ejecutar(ci);
                ClienteDTO clienteDTO = new ClienteDTO()
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Telefono = cliente.Telefono,
                    Direccion = cliente.Direccion,
                    Email = cliente.Email.Value,
                    Rol = cliente.Rol,
                    Ci = cliente.Ci

                };
               
                ResponseObtenerClientePorCiDTO response = new ResponseObtenerClientePorCiDTO()
                {
                    StatusCode = 200,
                    cliente = clienteDTO,
                    Error=""

                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObtenerClientePorCiDTO response = new ResponseObtenerClientePorCiDTO()
                {
                    StatusCode = 500,
                    cliente = null,
                    Error=ex.Message

                };
                return BadRequest(response);
            }


        }
    }
}
