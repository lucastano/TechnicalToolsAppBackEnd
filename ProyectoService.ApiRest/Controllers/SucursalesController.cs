using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoService.ApiRest.DTOs;
using ProyectoService.Aplicacion.CasosUso;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly IAgregarSucursal agregarSucursaleUc;
        private readonly IObtenerSucursalPorId obtenerSucursalPorIdUc;
        private readonly IObtenerTodasLasSucursales obtenerTodasLasSucurusalesUc;
        private readonly IWebHostEnvironment _env;
        private readonly IObtenerEmpresaPorId obtenerEmpresaPorIdUc;
        private readonly IModificarSucursal modificarSucursalUc;
        private readonly IObtenerSucursalesPorEmpresa obtenerSucursalesPorEmpresaUc;
        public SucursalesController(IAgregarSucursal agregarSucursaleUc, IObtenerSucursalPorId obtenerSucursalPorIdUc, IObtenerTodasLasSucursales obtenerTodasLasSucurusalesUc, IWebHostEnvironment env, IObtenerEmpresaPorId obtenerEmpresaPorIdUc, IModificarSucursal modificarSucursalUc, IObtenerSucursalesPorEmpresa obtenerSucursalesPorEmpresaUc)
        {
            this.agregarSucursaleUc = agregarSucursaleUc;
            this.obtenerSucursalPorIdUc = obtenerSucursalPorIdUc;
            this.obtenerTodasLasSucurusalesUc = obtenerTodasLasSucurusalesUc;
            _env = env;
            this.obtenerEmpresaPorIdUc = obtenerEmpresaPorIdUc;
            this.modificarSucursalUc = modificarSucursalUc;
            this.obtenerSucursalesPorEmpresaUc = obtenerSucursalesPorEmpresaUc;
        }

        [HttpPost]
      
        public async Task<ActionResult> Agregar( AgregarSucurusalDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(400);
            }

            try
            {
               
                Empresa emp = await obtenerEmpresaPorIdUc.Ejecutar(dto.IdEmpresa);
                Sucursal sucursal = new Sucursal()
                {
                    CodigoSucursal = dto.CodigoSucursal,
                    Telefono = dto.Telefono,
                    Direccion = dto.Direccion,
                    Email = dto.Email,
                    Empresa = emp,
                };
                sucursal = await agregarSucursaleUc.Ejecutar(sucursal);
                return Ok(sucursal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);


            }
        }

        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerSucursalPorId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(400);
            }

            try
            {
                Sucursal suc = await obtenerSucursalPorIdUc.Ejecutar(id);
               
                ResponseSucursalDTO sucursal = new ResponseSucursalDTO
                {
                    Id = suc.Id,
                    CodigoSucursal = suc.CodigoSucursal,
                    Direccion = suc.Direccion,
                    Telefono = suc.Telefono,
                    Email = suc.Email,
                    IdEmpresa = suc.Empresa.Id

                };

                return Ok(sucursal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);


            }
        }

        [HttpGet("SucursalesPorEmpresa")]
        public async Task<ActionResult> ObtenerSucursalesPorEmpresa(int id)
        {
            try
            {
                List<Sucursal> sucursales = await obtenerSucursalesPorEmpresaUc.Ejecutar(id);
                IEnumerable<ResponseSucursalDTO> suc = sucursales.Select(s => new ResponseSucursalDTO()
                {
                    Id = s.Id,
                    CodigoSucursal = s.CodigoSucursal,
                    Direccion = s.Direccion,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    IdEmpresa= s.Empresa.Id

                });

                return Ok(suc);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
       
        public async Task<ActionResult> ModificarSucursal( ModificarSucursalDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(400);
            }

            try
            {
                // Buscar la empresa existente
                Sucursal sucursalExistente = await obtenerSucursalPorIdUc.Ejecutar(dto.Id);
                if (sucursalExistente == null)
                {
                    return NotFound($"No se encontró la sucursal con ID {dto.Id}");
                }

                // Eliminar la foto anterior si se envía una nueva
               

                // Actualizar los demás campos
                sucursalExistente.CodigoSucursal = dto.CodigoSucursal ?? sucursalExistente.CodigoSucursal;
                sucursalExistente.Telefono = dto.Telefono ?? sucursalExistente.Telefono;
                sucursalExistente.Direccion = dto.Direccion ?? sucursalExistente.Direccion;
                sucursalExistente.Email = dto.Email ?? sucursalExistente.Email;

                // Guardar los cambios
                Sucursal sucursalActualizada = await modificarSucursalUc.Ejecutar(sucursalExistente);
                return Ok(sucursalActualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
