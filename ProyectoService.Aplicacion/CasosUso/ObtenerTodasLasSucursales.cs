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
    public class ObtenerTodasLasSucursales : IObtenerTodasLasSucursales
    {
        private readonly ISucursalRepositorio repo;
        public ObtenerTodasLasSucursales(ISucursalRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task<List<Sucursal>> Ejecutar()
        {
            return await repo.getAll();
        }
    }
}
