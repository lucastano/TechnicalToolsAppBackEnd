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
    public class ReparacionesController : ControllerBase
    {
        private readonly IAgregarReparacion agregarReparacionUc;
        private readonly IObtenerTodasLasReparaciones obtenerTodasLasReparacionesUc;
        private readonly IObtenerReparacionPorId obtenerReparacionPorIdUc;
        private readonly IPresupuestarReparacion presupuestarReparacionUc;
        private readonly IAgregarClienteUC agregarClienteUc;
        private readonly IObtenerClientePorCI obtenerClientePorCiUc;
        private readonly IObtenerTecnicoPorId obtenerTecnicoPorIdUc;
        private readonly IAvisoNuevaReparacion avisoNuevaReparacionUc;
        private readonly IAvisoNuevoPresupuesto avisoNuevoPresupuestoUc;
        private readonly IAvisoEntregaReparacion avisoEntregarReparacionUc;
        private readonly IAvisoReparacionTerminada avisoReparacionTerminadaUc;
        private readonly IAceptarPresupuesto aceptarPresupuestoUc;
        private readonly INoAceptarPresupuesto noAceptarPresupuestoUc;
        private readonly ITerminarReparacion terminarReparacionUc;
        private readonly IEntregarReparacion entregarReparacionUc;
        private readonly IModificarPresupuestoReparacion modificarPresupuestoReparacionUc;
        private readonly IModificarDatosReparacion modificarDatosReparacionUc;
        private readonly IGenerarOrdenDeServicio generarOrdenDeServicioUc;
        private readonly IConfiguration configuration;
        private readonly IEliminarMensajesReparacion eliminarMensajesReparacionUc;
        private readonly IObtenerHistoriaClinica obtenerHistoriaClinicaUc;
        private readonly IObtenerMontoTotalHistoriaClinica obtenerMontoTotalHistoriaClinicaUc;
        private Empresa emp;



        public ReparacionesController(IAgregarReparacion agregarReparacionUc, IObtenerTodasLasReparaciones obtenerTodasLasReparacionesUc, IObtenerReparacionesPorCliente obtenerReparacionesPorClienteUc, IObtenerReparacionesPorTecnico obtenerReparacionesPorTecnicoUc, IPresupuestarReparacion presupuestarReparacionUc, IObtenerClientePorCI obtenerClientePorCiUc, IObtenerTecnicoPorId obtenerTecnicoPorIdUc, IAvisoNuevaReparacion avisoNuevaReparacionUc, IAvisoNuevoPresupuesto avisoNuevoPresupuestoUc, IAceptarPresupuesto aceptarPresupuestoUc, INoAceptarPresupuesto noAceptarPresupuestoUc, ITerminarReparacion terminarReparacionUc, IEntregarReparacion entregarReparacionUc, IAvisoEntregaReparacion avisoEntregarReparacionUc, IAvisoReparacionTerminada avisoReparacionTerminadaUc,  IConfiguration configuration, IObtenerReparacionPorId obtenerReparacionPorIdUc, IGenerarOrdenDeServicio generarOrdenDeServicioUc, IModificarPresupuestoReparacion modificarPresupuestoReparacionUc,IModificarDatosReparacion modificarDatosReparacionUc, IEliminarMensajesReparacion eliminarMensajesReparacionUc, IObtenerHistoriaClinica obtenerHistoriaClinicaUc, IObtenerMontoTotalHistoriaClinica obtenerMontoTotalHistoriaClinicaUc)
        {
            this.agregarReparacionUc = agregarReparacionUc;
            this.obtenerTodasLasReparacionesUc = obtenerTodasLasReparacionesUc;
            this.obtenerReparacionPorIdUc = obtenerReparacionPorIdUc;
            this.presupuestarReparacionUc = presupuestarReparacionUc;
            this.obtenerClientePorCiUc = obtenerClientePorCiUc;
            this.obtenerTecnicoPorIdUc = obtenerTecnicoPorIdUc;
            this.avisoNuevaReparacionUc = avisoNuevaReparacionUc;
            this.avisoNuevoPresupuestoUc = avisoNuevoPresupuestoUc;
            this.aceptarPresupuestoUc = aceptarPresupuestoUc;
            this.noAceptarPresupuestoUc = noAceptarPresupuestoUc;
            this.terminarReparacionUc = terminarReparacionUc;
            this.entregarReparacionUc = entregarReparacionUc;
            this.avisoEntregarReparacionUc = avisoEntregarReparacionUc;
            this.avisoReparacionTerminadaUc = avisoReparacionTerminadaUc;
            this.modificarPresupuestoReparacionUc = modificarPresupuestoReparacionUc;
            this.modificarDatosReparacionUc = modificarDatosReparacionUc;
            this.eliminarMensajesReparacionUc = eliminarMensajesReparacionUc;
            this.obtenerMontoTotalHistoriaClinicaUc = obtenerMontoTotalHistoriaClinicaUc;
            this.obtenerHistoriaClinicaUc = obtenerHistoriaClinicaUc;
            
            this.configuration = configuration;
            //CONFIGURACION ENTIDAD EMPRESA
            var configNombreEmpresa = configuration.GetSection("EmpresaSettings:NombreEmpresa").Value!;
            var configDireccionEmpresa = configuration.GetSection("EmpresaSettings:DireccionEmpresa").Value!;
            var configTelefonoEmpresa = configuration.GetSection("EmpresaSettings:TelefonoEmpresa").Value!;
            var configEmail = configuration.GetSection("EmpresaSettings:Email").Value!;
            var configPassword = configuration.GetSection("EmpresaSettings:EmailPassword").Value!;
            var configPoliticasEmpresa = configuration.GetSection("EmpresaSettings:PoliticasEmpresa").Value!;
            Empresa empresaConfig = new Empresa()
            {
                Nombre = configNombreEmpresa,
                Direccion = configDireccionEmpresa,
                Telefono = configTelefonoEmpresa,
                Email = configEmail,
                EmailPassword = configPassword,
                PoliticasEmpresa= configPoliticasEmpresa
            };
            this.emp = empresaConfig;
            this.generarOrdenDeServicioUc = generarOrdenDeServicioUc;
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
                    FechaPromesaPresupuesto=dto.FechaPromesaPresupuesto

                };

               Reparacion rep= await agregarReparacionUc.Ejecutar(reparacion);
               byte []pdf=  await avisoNuevaReparacionUc.Ejecutar(rep,emp);
                //ACA TENGO QUE RESOLVER QUE RETORNAR
                ResponseNuevaReparacionDTO response = new ResponseNuevaReparacionDTO()
                {
                    StatusCode = 200,
                    OrdenDeServicio=pdf
                };
                return Ok(response);




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
                await avisoNuevoPresupuestoUc.Ejecutar(rep,emp);//caso de uso 
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
                await avisoReparacionTerminadaUc.Ejecutar(reparacion,emp);
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
                byte[] pdf=await avisoEntregarReparacionUc.Ejecutar(reparacion, emp);
                ResponseEntregarReparacionDTO response = new ResponseEntregarReparacionDTO()
                {
                    StatusCode = 200,
                    OrdenDeServicio=pdf

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
                    ClienteNombre = r.Cliente.Nombre,
                    ClienteApellido = r.Cliente.Apellido,
                    ClienteTelefono = r.Cliente.Telefono,
                    ClienteDireccion = r.Cliente.Direccion,
                    ClienteEmail = r.Cliente.Email.Value,
                    ClienteCedula=r.Cliente.Ci,
                    TecnicoId = r.Tecnico.Id,
                    Producto = r.Producto,
                    NumeroSerie = r.NumeroSerie,
                    Descripcion = r.Descripcion,
                    Fecha = r.Fecha,
                    Estado=r.Estado,
                    DescripcionPresupuesto=r.DescripcionPresupuesto,
                    Costo=r.CostoFinal,
                    FechaPromesaPresupuesto=r.FechaPromesaPresupuesto
                    
                    

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

        [HttpGet("GenerarOrdenDeServicio")]
        public async Task<ActionResult>GenerarOrdenDeServicio(int id)
        {
            try
            {
                if (id == 0) throw new Exception("Numero de orden incorrecto");
                Reparacion rep = await obtenerReparacionPorIdUc.Ejecutar(id);
                byte[]pdf= generarOrdenDeServicioUc.Ejecutar(rep,emp);

                ResponseGenerarOrdenDeServicioDTO response = new ResponseGenerarOrdenDeServicioDTO()
                {
                    statusCode = 200,
                    OrdenDeServicio = pdf,
                };
                return Ok(response); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("ModificarPresupuestoReparacion")]
        public async Task<ActionResult> ModificarPresupuestoReparacion(int id,double costo,string descripcion)
        {
            try
            {
                if (id == 0) throw new Exception("Numero de orden de reparacion incorrecto");
                await modificarPresupuestoReparacionUc.Ejecutar(id, costo,descripcion);
                return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("ModificarDatosReparacion")]
        public async Task<ActionResult> ModificarDatosReparacion(int id,DateTime fechaPromesaPresupuesto, string numeroSerie,string descripcion)
        {
            try
            {
                if (id == 0) throw new Exception("Numero de orden incorrecto");
                await modificarDatosReparacionUc.Ejecutar(id,fechaPromesaPresupuesto,numeroSerie,descripcion);
                return Ok();

            }
            catch( Exception ex)
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
                Reparacion reparacion = reparaciones[1];
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
