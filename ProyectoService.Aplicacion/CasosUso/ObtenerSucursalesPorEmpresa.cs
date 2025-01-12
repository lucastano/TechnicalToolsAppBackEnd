using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.Aplicacion.CasosUso
{
    public class ObtenerSucursalesPorEmpresa : IObtenerSucursalesPorEmpresa
    {
        private readonly ISucursalRepositorio repo;
        public ObtenerSucursalesPorEmpresa(ISucursalRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task<List<Sucursal>> Ejecutar(int id)
        {
            return await repo.obtenerSucursalesPorEmpresa(id);
        }
    }
}
