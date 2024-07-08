using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.IRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.Aplicacion.CasosUso
{
    public class ModificarCostoReparacion : IModificarCostoReparacion
    {
        private readonly IReparacionRepositorio repo;
        public ModificarCostoReparacion(IReparacionRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task Ejecutar(int id, double costo)
        {
            await repo.ModificarCostoReparacion(id, costo);
        }
    }
}
