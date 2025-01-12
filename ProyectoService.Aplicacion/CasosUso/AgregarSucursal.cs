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
    public class AgregarSucursal : IAgregarSucursal
    {
        private readonly ISucursalRepositorio repo;
        public AgregarSucursal(ISucursalRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task<Sucursal> Ejecutar(Sucursal suc)
        {
           return await repo.Agregar(suc);
        }
    }
}
