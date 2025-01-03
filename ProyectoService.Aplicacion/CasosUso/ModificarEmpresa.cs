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
    public class ModificarEmpresa : IModificarEmpresa
    {
        private readonly IEmpresaRepositorio repo;
        public ModificarEmpresa(IEmpresaRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task<Empresa> Ejecutar(Empresa emp)
        {
            return await repo.ModificarEmpresa(emp);
        }
    }
}
