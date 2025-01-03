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
    public class ObtenerEmpresa : IObtenerEmpresa
    {
        private readonly IEmpresaRepositorio repo;
        public ObtenerEmpresa(IEmpresaRepositorio repo)
        {
            this.repo = repo;
        }
        public Task<Empresa?> Ejecutar()
        {
            return repo.getEmpresa();
        }
    }
}
