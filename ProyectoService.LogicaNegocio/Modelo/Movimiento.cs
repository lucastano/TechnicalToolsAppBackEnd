using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{
    public class Movimiento
    {
        public int Id { get; set; }
        public MotivoMovimiento Motivo { get; set; }
        public Reparacion Reparacion { get; set; }
        public Tecnico TecnicoAsigando { get; set; }
        public Tecnico TecnicoDesasignado { get; set; }
        public DateTime Fecha { get; set; }

    }
}
