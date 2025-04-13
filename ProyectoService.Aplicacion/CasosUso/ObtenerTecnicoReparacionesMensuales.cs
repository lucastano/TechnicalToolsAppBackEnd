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
    public class ObtenerTecnicoReparacionesMensuales : IObtenerTecnicoReparacionesMensuales
    {
        private readonly IReparacionRepositorio repo;
        public ObtenerTecnicoReparacionesMensuales(IReparacionRepositorio repo)
        {
            this.repo = repo;
        }
        public Task<List<ObjetoTecnicoReparacionesMensuales>> Ejecutar(int mes)
        {
            return repo.obtenerReparacionesMensualesPorTecnico(mes);
        }
    }
}
