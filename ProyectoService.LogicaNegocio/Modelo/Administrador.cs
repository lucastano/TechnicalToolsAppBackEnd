using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{
    [Table("Administrador")]
    public class Administrador:Usuario
    {
        public Empresa Empresa { get; set; }
        public Sucursal Sucursal { get; set; }
        public string Foto { get; set; } = string.Empty;
        //public string Foto { get; set; }
        public Administrador() 
        {
            this.Rol = "Administrador";
        }
    }
}
