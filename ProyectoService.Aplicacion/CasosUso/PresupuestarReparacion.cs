using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.IRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.Aplicacion.CasosUso
{
    public class PresupuestarReparacion : IPresupuestarReparacion
    {
        private readonly IReparacionRepositorio repo;
        public PresupuestarReparacion(IReparacionRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task Ejecutar(int id, double ManoObra, string descripcion)
        {
            await repo.Presupuestar(id, ManoObra, descripcion);
        }
    }
}
