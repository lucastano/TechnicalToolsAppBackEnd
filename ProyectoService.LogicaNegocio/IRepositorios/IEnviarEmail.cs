using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IRepositorios
{
    public interface IEnviarEmail
    {
        Task<byte[]> EnviarEmailNuevaReparacion(Reparacion entity, Empresa emp);
        Task EnviarEmailNuevoPresupuesto(Reparacion entity,Empresa emp);
        Task EnviarEmailAvisoTerminada(Reparacion entity, Empresa emp);
        Task EnviarEmailAvisoEntrega(Reparacion entity, Empresa emp);

    }
}
