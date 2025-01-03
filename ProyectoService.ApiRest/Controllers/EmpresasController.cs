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
    [Authorize]
    public class EmpresasController : ControllerBase
    {
        private readonly IAgregarEmpresa agregarEmpresaUc;
        private readonly IObtenerEmpresaPorId obtenerEmpresaPorIdUc;
        private readonly IObtenerEmpresa obtenerEmpresaUc;
        private readonly IWebHostEnvironment _env;
        private readonly IModificarEmpresa modificarEmpresaUc;
        public EmpresasController(IAgregarEmpresa agregarEmpresaUc, IObtenerEmpresaPorId obtenerEmpresaPorIdUc, IWebHostEnvironment env, IObtenerEmpresa obtenerEmpresaUc, IModificarEmpresa modificarEmpresaUc)
        {
            this.agregarEmpresaUc = agregarEmpresaUc;
            this.obtenerEmpresaPorIdUc = obtenerEmpresaPorIdUc;
            this.obtenerEmpresaUc = obtenerEmpresaUc;
            _env = env;
            this.modificarEmpresaUc = modificarEmpresaUc;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AgregarEmpresa([FromForm] AgregarEmpresaDTO dto)
        {
         
            if (!ModelState.IsValid)
            {
                return BadRequest(400);
            }

            try
            {
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsPath);
                var fileName = $"{Guid.NewGuid()}_{dto.Foto.FileName}";
                var filePath = Path.Combine(uploadsPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Foto.CopyToAsync(stream);
                }
                Empresa empresa = new Empresa
                {
                    Nombre = dto.Nombre,
                    Telefono = dto.Telefono,
                    Direccion = dto.Direccion,
                    Email = dto.Email,
                    EmailPassword = dto.EmailPassword,
                    Foto = $"/uploads/{fileName}",
                    PoliticasEmpresa = "empty"

                };
                Empresa emp = await agregarEmpresaUc.Ejecutar(empresa);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);


            }
        }

        [HttpGet("ObtenerEmpresaPorId")]

        public async Task<ActionResult> ObtenerEmpresaPorId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(400);
            }

            try
            {
                Empresa emp = await obtenerEmpresaPorIdUc.Ejecutar(id);
                var fotoUrl = Url.Content($"~{emp.Foto}"); // Genera una URL relativa

                ResponseEmpresaDTO empresa = new ResponseEmpresaDTO
                {
                    Id = emp.Id,
                    Nombre = emp.Nombre,
                    Telefono = emp.Telefono,
                    Direccion = emp.Direccion,
                    Email = emp.Email,
                    EmailPassword = emp.EmailPassword,
                    Foto = fotoUrl

                };
               
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);


            }
        }

        [HttpGet("ObtenerEmpresa")]
        public async Task<ActionResult> ObtenerEmpresa()
        {
            try
            {
                Empresa emp = await obtenerEmpresaUc.Ejecutar();
                var fotoUrl = Url.Content($"~{emp.Foto}"); // Genera una URL relativa
                ResponseEmpresaDTO empresa = new ResponseEmpresaDTO
                {
                    Id = emp.Id,
                    Nombre = emp.Nombre,
                    Telefono = emp.Telefono,
                    Direccion = emp.Direccion,
                    Email = emp.Email,
                    EmailPassword = emp.EmailPassword,
                    Foto = fotoUrl

                };

                return Ok(empresa);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> ModificarEmpresa([FromForm]ModificarEmpresaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(400);
            }

            try
            {
                // Buscar la empresa existente
                Empresa empresaExistente = await obtenerEmpresaPorIdUc.Ejecutar(dto.Id);
                if (empresaExistente == null)
                {
                    return NotFound($"No se encontró la empresa con ID {dto.Id}");
                }

                // Eliminar la foto anterior si se envía una nueva
                if (dto.Foto != null)
                {
                    var fotoAnteriorPath = Path.Combine(_env.WebRootPath, empresaExistente.Foto.TrimStart('/'));
                    if (System.IO.File.Exists(fotoAnteriorPath))
                    {
                        System.IO.File.Delete(fotoAnteriorPath);
                    }

                    // Guardar la nueva foto
                    var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsPath);
                    var fileName = $"{Guid.NewGuid()}_{dto.Foto.FileName}";
                    var filePath = Path.Combine(uploadsPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Foto.CopyToAsync(stream);
                    }
                    empresaExistente.Foto = $"/uploads/{fileName}";
                }

                // Actualizar los demás campos
                empresaExistente.Nombre = dto.Nombre ?? empresaExistente.Nombre;
                empresaExistente.Telefono = dto.Telefono ?? empresaExistente.Telefono;
                empresaExistente.Direccion = dto.Direccion ?? empresaExistente.Direccion;
                empresaExistente.Email = dto.Email ?? empresaExistente.Email;

                // Guardar los cambios
                Empresa empresaActualizada = await modificarEmpresaUc.Ejecutar(empresaExistente);
                return Ok(empresaActualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
