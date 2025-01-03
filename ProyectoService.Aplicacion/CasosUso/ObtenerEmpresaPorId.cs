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
    public class ObtenerEmpresaPorId : IObtenerEmpresaPorId
    {
        private readonly IEmpresaRepositorio repo;
        public ObtenerEmpresaPorId(IEmpresaRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task<Empresa?> Ejecutar(int id)
        {
            return await repo.ObtenerEmpresaPorId(id);
        }
    }
}
