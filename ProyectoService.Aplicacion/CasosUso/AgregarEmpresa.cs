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
    public class AgregarEmpresa : IAgregarEmpresa
    {
        private readonly IEmpresaRepositorio repo;
        public AgregarEmpresa(IEmpresaRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task Ejecutar(Empresa empresa)
        {
            await repo.Add(empresa);
        }
    }
}
