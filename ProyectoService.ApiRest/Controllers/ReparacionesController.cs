﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoService.ApiRest.DTOs;
using ProyectoService.Aplicacion.CasosUso;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;
using System.Data;

namespace ProyectoService.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class ReparacionesController : ControllerBase
    {
        private readonly IEnviarEmail enviarEmailUc;
        private readonly IAgregarReparacion agregarReparacionUc;
        private readonly IObtenerTodasLasReparaciones obtenerTodasLasReparacionesUc;
        private readonly IObtenerReparacionPorId obtenerReparacionPorIdUc;
        private readonly IPresupuestarReparacion presupuestarReparacionUc;
        private readonly IAgregarClienteUC agregarClienteUc;
        private readonly IObtenerClientePorCI obtenerClientePorCiUc;
        private readonly IObtenerTecnicoPorId obtenerTecnicoPorIdUc;
        private readonly IAceptarPresupuesto aceptarPresupuestoUc;
        private readonly INoAceptarPresupuesto noAceptarPresupuestoUc;
        private readonly ITerminarReparacion terminarReparacionUc;
        private readonly IEntregarReparacion entregarReparacionUc;
        private readonly IModificarPresupuestoReparacion modificarPresupuestoReparacionUc;
        private readonly IModificarDatosReparacion modificarDatosReparacionUc;
        private readonly IConfiguration configuration;
        private readonly IEliminarMensajesReparacion eliminarMensajesReparacionUc;
        private readonly IObtenerHistoriaClinica obtenerHistoriaClinicaUc;
        private readonly IObtenerMontoTotalHistoriaClinica obtenerMontoTotalHistoriaClinicaUc;
        private readonly IObtenerProductoPorId obtenerProductoPorIdUc;
        private readonly IObtenerReparacionesPorCliente obtenerReparacionesPorClienteUc;
        private readonly IGenerarOrdenServicioEntrada generarOrdSrvEntradaUc;
        private readonly IObtenerEmpresaPorId obtenerEmpresaPorIdUc;
        private readonly IObtenerSucursalPorId obtenerSucursalPorIdUc;
        private readonly ITransferirReparacion transferirReparacionUc;
        private readonly IWebHostEnvironment _env;

        private Empresa emp;



        public ReparacionesController(IAgregarReparacion agregarReparacionUc, IObtenerTodasLasReparaciones obtenerTodasLasReparacionesUc,  IObtenerReparacionesPorTecnico obtenerReparacionesPorTecnicoUc, IPresupuestarReparacion presupuestarReparacionUc, IObtenerClientePorCI obtenerClientePorCiUc, IObtenerTecnicoPorId obtenerTecnicoPorIdUc,  IAceptarPresupuesto aceptarPresupuestoUc, INoAceptarPresupuesto noAceptarPresupuestoUc, ITerminarReparacion terminarReparacionUc, IEntregarReparacion entregarReparacionUc,IConfiguration configuration, IObtenerReparacionPorId obtenerReparacionPorIdUc,  IModificarPresupuestoReparacion modificarPresupuestoReparacionUc,IModificarDatosReparacion modificarDatosReparacionUc, IEliminarMensajesReparacion eliminarMensajesReparacionUc, IObtenerHistoriaClinica obtenerHistoriaClinicaUc, IObtenerMontoTotalHistoriaClinica obtenerMontoTotalHistoriaClinicaUc,IObtenerProductoPorId obtenerProductoPorIdUc, IObtenerReparacionesPorCliente obtenerReparacionesPorClienteUc, IGenerarOrdenServicioEntrada generarOrdSrvEntradaUc, IObtenerEmpresaPorId obtenerEmpresaPorIdUc, IWebHostEnvironment _env, IEnviarEmail enviarEmailUc, IObtenerSucursalPorId obtenerSucursalPorIdUc, ITransferirReparacion transferirReparacionUc)
        {
            this.agregarReparacionUc = agregarReparacionUc;
            this.obtenerTodasLasReparacionesUc = obtenerTodasLasReparacionesUc;
            this.obtenerReparacionPorIdUc = obtenerReparacionPorIdUc;
            this.presupuestarReparacionUc = presupuestarReparacionUc;
            this.obtenerClientePorCiUc = obtenerClientePorCiUc;
            this.obtenerTecnicoPorIdUc = obtenerTecnicoPorIdUc;
            this.aceptarPresupuestoUc = aceptarPresupuestoUc;
            this.noAceptarPresupuestoUc = noAceptarPresupuestoUc;
            this.terminarReparacionUc = terminarReparacionUc;
            this.entregarReparacionUc = entregarReparacionUc;
            this.modificarPresupuestoReparacionUc = modificarPresupuestoReparacionUc;
            this.modificarDatosReparacionUc = modificarDatosReparacionUc;
            this.eliminarMensajesReparacionUc = eliminarMensajesReparacionUc;
            this.obtenerMontoTotalHistoriaClinicaUc = obtenerMontoTotalHistoriaClinicaUc;
            this.obtenerHistoriaClinicaUc = obtenerHistoriaClinicaUc;
            this.obtenerProductoPorIdUc = obtenerProductoPorIdUc;
            this.obtenerReparacionesPorClienteUc = obtenerReparacionesPorClienteUc;
            this.configuration = configuration;
            this.generarOrdSrvEntradaUc = generarOrdSrvEntradaUc;
            this.obtenerEmpresaPorIdUc = obtenerEmpresaPorIdUc;
            this.enviarEmailUc = enviarEmailUc;
            this.obtenerSucursalPorIdUc = obtenerSucursalPorIdUc;
            this.transferirReparacionUc = transferirReparacionUc;
            this._env = _env; 
        }

        [HttpPost]

        public async Task<ActionResult> NuevaReparacion(NuevaReparacionDTO dto)
        {
            if (!ModelState.IsValid) throw new Exception("Debe ingresar datos de la reparacion");
            try
            {

                Tecnico tecnico = await obtenerTecnicoPorIdUc.Ejecutar(dto.IdTecnico);
                Empresa emp = await obtenerEmpresaPorIdUc.Ejecutar(dto.IdEmpresa);
                Sucursal suc = await obtenerSucursalPorIdUc.Ejecutar(dto.IdSucursal);
                if (tecnico == null ) throw new Exception("Tecnico no existe");
                Cliente cliente = await obtenerClientePorCiUc.Ejecutar(dto.CiCliente);
                Reparacion reparacion = new Reparacion()
                {
                    Tecnico = tecnico,
                    Cliente = cliente,
                    Producto = await obtenerProductoPorIdUc.Ejecutar(dto.IdProducto),
                    NumeroSerie = dto.NumeroSerie,
                    Descripcion = dto.Descripcion,
                    FechaPromesaPresupuesto=dto.FechaPromesaPresupuesto
                };
               
                Reparacion r= await agregarReparacionUc.Ejecutar(reparacion);
                var fotoUrl = Url.Content($"~{emp.Foto}"); // Genera una URL relativa
                var filePath = Path.Combine(_env.WebRootPath, fotoUrl.TrimStart('~', '/'));
                byte[] fotoBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                byte[] pdf = await generarOrdSrvEntradaUc.Ejecutar(r,emp,suc,fotoBytes);
                if (suc.avisosEmail)
                {
                    enviarEmailUc.Ejecutar(r, emp,suc, pdf);
                }
                //aca agregar a futuro implementacion para dar aviso por wsp
                 ReparacionEnTallerDTO repdto = new ReparacionEnTallerDTO()
                 {
                    Id = r.Id,
                    ClienteId = r.Cliente.Id,
                    ClienteNombre = r.Cliente.Nombre,
                    ClienteApellido = r.Cliente.Apellido,
                    ClienteTelefono = r.Cliente.Telefono,
                    ClienteDireccion = r.Cliente.Direccion,
                    ClienteEmail = r.Cliente.Email.Value,
                    ClienteCedula = r.Cliente.Ci,
                    TecnicoId = r.Tecnico.Id,
                    Producto = new ProductoDTO()
                    {
                        Id = r.Producto.Id,
                        Marca = r.Producto.Marca,
                        Modelo = r.Producto.Modelo,
                        Version = r.Producto.Version
                    },
                    NumeroSerie = r.NumeroSerie,
                    Descripcion = r.Descripcion,
                    Fecha = r.Fecha,
                    Estado = r.Estado,
                    DescripcionPresupuesto = r.DescripcionPresupuesto,
                    Costo = r.CostoFinal,
                    FechaPromesaPresupuesto = r.FechaPromesaPresupuesto,
                    FechaPromesaEntrega = r.FechaPromesaEntrega
                };
                return Ok(repdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            } 
        }

        [HttpPost("Presupuestar")]
        public async Task<ActionResult> PresupuestarReparacion(PresupuestarReparacionDTO dto)
        {
            try
            {
                if (dto.Id == 0) throw new Exception("No existe reparacion con ese id");
                if (dto.Descripcion == null) throw new Exception("Debe ingresar una descripcio");
                Reparacion rep=await presupuestarReparacionUc.Ejecutar(dto.Id,dto.ManoObra,dto.Descripcion,dto.FechaPromesaEntrega);
                return StatusCode(200);    
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AceptarPresupuesto")]
        public async Task<ActionResult>AceptarPresupuesto(int id)
        {
            try
            {
                if (id == 0) throw new Exception("Numero de orden incorrecto");
                await aceptarPresupuestoUc.Ejecutar(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("NoAceptarPresupuesto")]
        public async Task<ActionResult> NoAceptarPresupuesto(int id,double costo,string razon)
        {
            try
            {
                if (id == 0) throw new Exception("Numero de orden incorrecto");
                await noAceptarPresupuestoUc.Ejecutar(id,costo,razon);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("TerminarReparacion")]
        public async Task<ActionResult<Reparacion>> TerminarReparacion(int id, bool reparada)
        {
            try
            {
                if (id == 0) throw new Exception("Numero de orden incorrecto");
                Reparacion reparacion = await terminarReparacionUc.Ejecutar(id,reparada);
                if (reparacion == null) throw new Exception("No se pudo terminar esta reparacion");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message) ;

            }


        }

        [HttpPost("EntregarReparacion")]
        public async Task<ActionResult<Reparacion>> EntregarReparacion(int id)
        {
            try
            {
                if (id == 0) throw new Exception("Numero de orden incorrecto");
                Reparacion reparacion = await entregarReparacionUc.Ejecutar(id);
                if (reparacion == null) throw new Exception("No se pudo entregar esta reparacion");
                ResponseEntregarReparacionDTO response = new ResponseEntregarReparacionDTO()
                {
                    StatusCode = 200
                   

                };

                await eliminarMensajesReparacionUc.Ejecutar(reparacion.Id);


                return Ok(response);
            }
            catch (Exception ex)
            {    
                return BadRequest(ex.Message);

            }


        }
        [HttpGet("TodasLasReparaciones")]
        public async Task<ActionResult<ResponseReparacionesEnTallerDTO>> ObtenerTodasLasReparaciones()
        {
            try
            {
                var reparaciones = await obtenerTodasLasReparacionesUc.Ejecutar();
                IEnumerable<ReparacionEnTallerDTO> rep = reparaciones.Select(r => new ReparacionEnTallerDTO()
                {
                    Id = r.Id,
                    ClienteId = r.Cliente.Id,
                    ClienteNombre = r.Cliente.Nombre,
                    ClienteApellido = r.Cliente.Apellido,
                    ClienteTelefono = r.Cliente.Telefono,
                    ClienteDireccion = r.Cliente.Direccion,
                    ClienteEmail = r.Cliente.Email.Value,
                    ClienteCedula=r.Cliente.Ci,
                    TecnicoId = r.Tecnico.Id,
                    Producto = new ProductoDTO()
                    {
                        Id=r.Producto.Id,
                        Marca=r.Producto.Marca,
                        Modelo=r.Producto.Modelo,
                        Version=r.Producto.Version
                    },
                    NumeroSerie = r.NumeroSerie,
                    Descripcion = r.Descripcion,
                    Fecha = r.Fecha,
                    Estado=r.Estado,
                    DescripcionPresupuesto=r.DescripcionPresupuesto,
                    Costo=r.CostoFinal,
                    FechaPromesaPresupuesto=r.FechaPromesaPresupuesto,
                    FechaPromesaEntrega=r.FechaPromesaEntrega
                    
                    

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

        [HttpGet("ReparacionesDeClienteCedula")]

        public async Task<ActionResult> ObtenerReparacionesClientePorCedula(string cedula)
        {
            try
            {
                if (cedula == "") throw new Exception("Debe ingresar un email");
                List<Reparacion> reparacionesDeCliente = await obtenerReparacionesPorClienteUc.Ejecutar(cedula);
                if (!reparacionesDeCliente.Any())
                {
                    return Ok("No tiene reparaciones");

                }
                else
                {
                    IEnumerable<ReparacionEnTallerDTO> rep = reparacionesDeCliente.Select(r => new ReparacionEnTallerDTO()
                    {
                        Id = r.Id,
                        ClienteNombre = r.Cliente.Nombre,
                        ClienteApellido = r.Cliente.Apellido,
                        ClienteTelefono = r.Cliente.Telefono,
                        ClienteDireccion = r.Cliente.Direccion,
                        ClienteEmail = r.Cliente.Email.Value,
                        ClienteCedula = r.Cliente.Ci,
                        TecnicoId = r.Tecnico.Id,
                        Producto = new ProductoDTO()
                        {
                            Id = r.Producto.Id,
                            Marca = r.Producto.Marca,
                            Modelo = r.Producto.Modelo,
                            Version = r.Producto.Version
                        },
                        NumeroSerie = r.NumeroSerie,
                        Descripcion = r.Descripcion,
                        Fecha = r.Fecha,
                        Estado = r.Estado,
                        DescripcionPresupuesto = r.DescripcionPresupuesto,
                        Costo = r.CostoFinal,
                        FechaPromesaPresupuesto = r.FechaPromesaPresupuesto,
                        FechaPromesaEntrega = r.FechaPromesaEntrega



                    });
                    return Ok(rep);

                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GenerarOrdSrv")]
        public async Task<ActionResult>GenerarOrdSrv(int idReparacion,int idEmpresa,int idSucursal)
        {
            try
            {
                if (idReparacion == 0) throw new Exception("Numero de orden incorrecto");
                if (idEmpresa == 0) throw new Exception("Numero de empresa incorrecta");
                Empresa emp = await obtenerEmpresaPorIdUc.Ejecutar(idEmpresa);
                Sucursal suc = await obtenerSucursalPorIdUc.Ejecutar(idSucursal);
                var fotoUrl = Url.Content($"~{emp.Foto}"); // Genera una URL relativa
                var filePath = Path.Combine(_env.WebRootPath, fotoUrl.TrimStart('~', '/'));
                // Leer el archivo como byte[]
                byte[] fotoBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                Reparacion rep = await obtenerReparacionPorIdUc.Ejecutar(idReparacion);
                byte[]pdf= await generarOrdSrvEntradaUc.Ejecutar(rep, emp,suc, fotoBytes);
               
                return Ok(pdf); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("ModificarPresupuestoReparacion")]
        public async Task<ActionResult> ModificarPresupuestoReparacion(ModificarPresupuestoReparacionDTO dto)
        {
            try
            {
                if (dto.Id == 0) throw new Exception("Numero de orden de reparacion incorrecto");
                Reparacion rep =await modificarPresupuestoReparacionUc.Ejecutar(dto.Id, dto.Costo,dto.Descripcion);
                ReparacionEnTallerDTO reparacionDTO = new ReparacionEnTallerDTO()
                {
                    Id = rep.Id,
                    ClienteNombre = rep.Cliente.Nombre,
                    ClienteApellido = rep.Cliente.Apellido,
                    ClienteTelefono = rep.Cliente.Telefono,
                    ClienteDireccion = rep.Cliente.Direccion,
                    ClienteEmail = rep.Cliente.Email.Value,
                    ClienteCedula = rep.Cliente.Ci,
                    TecnicoId = rep.Tecnico.Id,
                    Producto = new ProductoDTO()
                    {
                        Id = rep.Producto.Id,
                        Marca = rep.Producto.Marca,
                        Modelo = rep.Producto.Modelo,
                        Version = rep.Producto.Version
                    },
                    NumeroSerie = rep.NumeroSerie,
                    Descripcion = rep.Descripcion,
                    Fecha = rep.Fecha,
                    Estado = rep.Estado,
                    DescripcionPresupuesto = rep.DescripcionPresupuesto,
                    Costo = rep.CostoFinal,
                    FechaPromesaPresupuesto = rep.FechaPromesaPresupuesto

                };
                return Ok(reparacionDTO);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("ModificarDatosReparacion")]
        public async Task<ActionResult> ModificarDatosReparacion(ModificarDatosReparacionDTO dto)
        {
            try
            {
                if (dto.Id == 0) throw new Exception("Numero de orden incorrecto");
                Reparacion rep=await modificarDatosReparacionUc.Ejecutar(dto.Id,dto.FechaPromesaPresupuesto,dto.NumeroSerie,dto.Descripcion);
                ReparacionEnTallerDTO reparacionDTO = new ReparacionEnTallerDTO()
                {
                    Id = rep.Id,
                    ClienteNombre = rep.Cliente.Nombre,
                    ClienteApellido = rep.Cliente.Apellido,
                    ClienteTelefono = rep.Cliente.Telefono,
                    ClienteDireccion = rep.Cliente.Direccion,
                    ClienteEmail = rep.Cliente.Email.Value,
                    ClienteCedula = rep.Cliente.Ci,
                    TecnicoId = rep.Tecnico.Id,
                    Producto = new ProductoDTO()
                    {
                        Id = rep.Producto.Id,
                        Marca = rep.Producto.Marca,
                        Modelo = rep.Producto.Modelo,
                        Version = rep.Producto.Version
                    },
                    NumeroSerie = rep.NumeroSerie,
                    Descripcion = rep.Descripcion,
                    Fecha = rep.Fecha,
                    Estado = rep.Estado,
                    DescripcionPresupuesto = rep.DescripcionPresupuesto,
                    Costo = rep.CostoFinal,
                    FechaPromesaPresupuesto = rep.FechaPromesaPresupuesto

                };
                return Ok(reparacionDTO);

            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("TransferirReparacion")]
        public async Task<ActionResult> TransferirReparacion(int idReparacion,int idTecnico)
        {
            try
            {
                Reparacion reparacion = await obtenerReparacionPorIdUc.Ejecutar(idReparacion);
                if (reparacion == null) throw new Exception("No existe reparacion");
                Tecnico tecnico = await obtenerTecnicoPorIdUc.Ejecutar(idTecnico);
                if (tecnico == null) throw new Exception("No existe tecnico");
                reparacion.Tecnico = tecnico;
                bool response = await transferirReparacionUc.Ejecutar(reparacion);
                if (response == false) throw new Exception("No se pudo transferir la reparacion");
                return Ok(response);

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            
            }
            


        }

        [HttpGet("HistoriaClinica")]

        public async Task<ActionResult>ObtenerHistoriaClinica(string numeroSerie)
        {
            try
            {
                if (numeroSerie == null || numeroSerie.Length == 0) throw new Exception("Numero de serie incorrecto");
                List<Reparacion> reparaciones = await obtenerHistoriaClinicaUc.Ejecutar(numeroSerie);
                if (reparaciones.Count == 0) throw new Exception("Sin reparaciones realizadas");
                double montoTotal = await obtenerMontoTotalHistoriaClinicaUc.Ejecutar(numeroSerie);
                
                ResponseHistoriaClinicaDTO historiaClinica = new ResponseHistoriaClinicaDTO()
                {
                    CantidadReparacionesRealizadas = reparaciones.Count(),
                    GastoTotalEnReparaciones = montoTotal,
                    NumeroSerie = numeroSerie,
                    reparacionesRealizadas = reparaciones.Select(r => new ReparacionHCDTO()
                    {
                        FechaEntregaReparacion=r.FechaEntrega,
                        DescripcionProblema=r.Descripcion,
                        DescripcionSolucion=r.DescripcionPresupuesto,
                        CostoReparacion=r.CostoFinal
                    })
                     

                };
                return Ok(historiaClinica);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        

        
    }
}
