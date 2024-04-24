using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{
    public class Cliente:Usuario
    {
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Ci { get; set; }

        
    }
}
