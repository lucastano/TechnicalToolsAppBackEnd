using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IRepositorios
{
    public interface IEmpresaRepositorio
    {
        Task<Empresa> AgregarEmpresa(Empresa emp);
        Task<Empresa?> ObtenerEmpresaPorId(int id);
        Task<Empresa> ModificarEmpresa(Empresa emp);
        Task<Empresa?> getEmpresa();
    }
}
