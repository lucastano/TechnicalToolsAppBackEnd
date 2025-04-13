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
    public class TransferirReparacion : ITransferirReparacion
    {
        private readonly IReparacionRepositorio repo;
        public TransferirReparacion(IReparacionRepositorio repo) { this.repo = repo; }
        public Task<bool> Ejecutar(Reparacion reparacion)
        {
            return repo.TransferirReparacion(reparacion);
        }
    }
}
