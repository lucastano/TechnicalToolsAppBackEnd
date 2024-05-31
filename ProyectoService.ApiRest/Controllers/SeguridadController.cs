using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoService.ApiRest.DTOs;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SeguridadController : ControllerBase
    {
        private readonly IObtenerUsuario ucObtenerUsuarioCU;
        private readonly IConfiguration configuration;
        public SeguridadController(IObtenerUsuario ucObtenerUsuarioCU, IConfiguration configuration)
        {
            this.ucObtenerUsuarioCU = ucObtenerUsuarioCU;
            this.configuration = configuration;
        }


        [HttpPost]

        public ActionResult<ResponseLoginDTO> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(400);
            }
            try
            {
                Usuario usuarioModel = ucObtenerUsuarioCU.Ejecutar(dto.Email);
                if (usuarioModel == null)
                {
                    return Unauthorized("Email incorrecto");
                }

                if (!Seguridad.VerificarPasswordHash(dto.Password, usuarioModel.PasswordHash, usuarioModel.PasswordSalt))
                {
                    return BadRequest("Las credenciales no son validas");
                }
                string token = Seguridad.CrearToken(usuarioModel, configuration);
                UsuarioLogeadoDTO usuarioLogeado = new UsuarioLogeadoDTO();
                if (usuarioModel is Cliente)
                {
                    Cliente cliente= (Cliente)usuarioModel;
                    usuarioLogeado.Nombre= cliente.Nombre;
                    usuarioLogeado.Apellido= cliente.Apellido;
                    usuarioLogeado.Direccion = cliente.Direccion;
                    usuarioLogeado.Telefono= cliente.Telefono;
                    usuarioLogeado.Ci=cliente.Ci;
                    usuarioLogeado.Email= cliente.Email.Value;
                    usuarioLogeado.Rol = cliente.Rol;



                }
                else if (usuarioModel is Tecnico)
                {
                    Tecnico tecnico =(Tecnico)usuarioModel;
                    usuarioLogeado.Nombre = tecnico.Nombre;
                    usuarioLogeado.Apellido = tecnico.Apellido;
                    usuarioLogeado.Email = tecnico.Email.Value;
                    usuarioLogeado.Rol = tecnico.Rol;

                }
                else
                {
                    Administrador administrador = (Administrador)usuarioModel;
                    usuarioLogeado.Nombre = administrador.Nombre;
                    usuarioLogeado.Apellido = administrador.Apellido;
                    usuarioLogeado.Email = administrador.Email.Value;
                    usuarioLogeado.Rol = administrador.Rol;

                }

                ResponseLoginDTO response = new ResponseLoginDTO()
                {
                    StatusCode = 200,
                    Token = token,
                    Usuario = usuarioLogeado
                };

                return response;

               
            }
            catch(Exception ex)
            {
                return StatusCode(500);

            }

        }
    }
}
