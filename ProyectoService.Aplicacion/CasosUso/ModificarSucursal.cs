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
    public class ModificarSucursal : IModificarSucursal
    {

        private readonly ISucursalRepositorio repo;
        public ModificarSucursal(ISucursalRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task<Sucursal> Ejecutar(Sucursal entity)
        {
            return await repo.Modificar(entity);
        }
    }
}
