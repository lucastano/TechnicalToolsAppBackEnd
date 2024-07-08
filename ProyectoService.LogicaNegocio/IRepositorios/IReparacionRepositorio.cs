using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IRepositorios
{
    public interface IReparacionRepositorio:ICrudRepositorio<Reparacion>
    {
        Task<Reparacion> Presupuestar(int id,double ManoObra,string Descripcion,DateTime fechaPromesaEntrega);
        Task<Reparacion> Entregar(int id);
        Task AceptarPresupuesto(int id);
        Task NoAceptarPresupuesto(int id,double costo,string razon);
        Task<Reparacion> Terminar(int id, bool reparada);

        //estas reparaciones son todas las reparaciones sin ver su estado
        Task<Reparacion>ObtenerReparacionPorId(int id);
        Task<List<Reparacion>> ObtenerReparacionesPorCliente(string Ci);
        Task<List<Reparacion>> ObtenerReparacionesPorTecnico(string EmailTecnico);
        byte[] GenerarOrdenDeServicio(Reparacion rep,Empresa emp);
        Task<Reparacion> AddAlternativo(Reparacion entity);
        Task ModificarCostoReparacion(int id, double costo);
        Task ModificarDatosReparacion(int id, DateTime fechaPromesaPresupuesto,string numeroSerie,string descripcion);


    }
}
