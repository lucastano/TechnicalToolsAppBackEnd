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
    public class AvisoNuevoPresupuesto : IAvisoNuevoPresupuesto
    {
        private readonly IEnviarEmail repoEmail;
        public AvisoNuevoPresupuesto(IEnviarEmail repoEmail)
        {
            this.repoEmail = repoEmail;
        }

        public async Task Ejecutar(Reparacion entity, Empresa emp)
        {
            await repoEmail.EnviarEmailNuevoPresupuesto(entity,emp);
        }
    }
}
