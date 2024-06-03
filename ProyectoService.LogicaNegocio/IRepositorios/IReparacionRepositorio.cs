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
        Task Presupuestar(int id,double ManoObra,string Descripcion);
        Task<Reparacion>ObtenerReparacionPorId(int id);
        Task<List<Reparacion>> ObtenerReparacionesPorCliente(string Ci);
        Task<List<Reparacion>> ObtenerReparacionesPorTecnico(string EmailTecnico);
    }
}
