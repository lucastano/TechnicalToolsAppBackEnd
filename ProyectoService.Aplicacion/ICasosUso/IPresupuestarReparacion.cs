using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.Aplicacion.ICasosUso
{
    public interface IPresupuestarReparacion
    {
        Task Ejecutar(int id,double ManoObra,string descripcion);
    }
}
