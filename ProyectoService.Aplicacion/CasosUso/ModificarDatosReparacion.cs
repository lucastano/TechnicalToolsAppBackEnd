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
    public class ModificarDatosReparacion : IModificarDatosReparacion
    {
        private readonly IReparacionRepositorio repo;
        public ModificarDatosReparacion(IReparacionRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task Ejecutar(int id,DateTime fechaPromesaPresupuesto,string numeroSerie,string descripcion )
        {
            await repo.ModificarDatosReparacion(id,fechaPromesaPresupuesto,numeroSerie,descripcion);
        }
    }
}
