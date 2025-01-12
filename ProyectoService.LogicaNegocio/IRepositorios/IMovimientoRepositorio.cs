using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IRepositorios
{
    public interface IMovimientoRepositorio
    {
        Task<Movimiento> NuevoMovimiento(Movimiento movimiento);
        Task<Movimiento> DeshacerMovimiento(Movimiento movimiento);
        Task<List<Movimiento>> ObtenerMovimientos();
        Task<List<Movimiento>> obtenerMovimientosPorReparacion(Reparacion reparacion);
        Task<List<Movimiento>> obtenerMovimientosPorTecnico(int idTecnico);
    }
}
