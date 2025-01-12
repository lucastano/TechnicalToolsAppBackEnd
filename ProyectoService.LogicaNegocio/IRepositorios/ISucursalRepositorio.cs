using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IRepositorios
{
    public interface ISucursalRepositorio:ICrudRepositorio<Sucursal>
    {
        Task<Sucursal> Agregar(Sucursal suc);
        Task<Sucursal?> ObtenerPorId(int id);
        Task<Sucursal> Modificar(Sucursal entity);
        Task<List<Sucursal>> obtenerSucursalesPorEmpresa(int idEmpresa);
        
    }
}
