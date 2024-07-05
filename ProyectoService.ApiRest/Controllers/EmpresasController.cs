using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoService.ApiRest.DTOs;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IAgregarEmpresa agregarEmpresaUc;
        private readonly IObtenerEmpresa obtenerEmpresaUc;
        public EmpresasController(IAgregarEmpresa agregarEmpresaUc, IObtenerEmpresa obtenerEmpresaUc)
        {
            this.agregarEmpresaUc = agregarEmpresaUc;
            this.obtenerEmpresaUc = obtenerEmpresaUc;
        }


        [HttpPost]
        public async Task<IActionResult> Agregar([FromForm] AgregarEmpresaDTO dto)
        {
            try
            {
                byte[] foto = null;
                if (dto.Foto != null)
                {
                    using var memoryStream = new MemoryStream();
                    await dto.Foto.CopyToAsync(memoryStream);
                    foto = memoryStream.ToArray();
                }
                Empresa emp = new Empresa()
                {
                    Nombre = dto.Nombre,
                    Telefono = dto.Telefono,
                    Direccion = dto.Direccion,
                    Email = dto.Email,
                    EmailPassword = dto.EmailPassword,
                    Foto = foto

                };
                await agregarEmpresaUc.Ejecutar(emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEmpresa()
        {
            try
            {
                Empresa emp = await obtenerEmpresaUc.Ejecutar();
                EmpresaDTO empresa = new EmpresaDTO()
                {
                    Nombre = emp.Nombre,
                    Telefono = emp.Telefono,
                    Direccion = emp.Direccion,
                    Email = emp.Email,
                    EmailPassword = emp.EmailPassword,
                    Foto = emp.Foto
                };

                return Ok(empresa);


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
