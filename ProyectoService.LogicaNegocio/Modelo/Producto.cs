using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{
    [Table("Producto")]
    public class Producto
    {
        public int Id { get; set; }
        public string Marca { get; set; }

        public string  Modelo { get; set; }

        public string Version { get; set; }

        





    }
}
