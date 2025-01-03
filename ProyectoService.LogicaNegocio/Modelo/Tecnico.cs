using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoService.LogicaNegocio.Modelo
{
    [Table("tecnicos")]
    public class Tecnico:Usuario
    {
        public Empresa Empresa { get; set; }
        //public string Foto { get; set; }

        public Tecnico()
        {
            this.Rol = "Tecnico";
        }

    }
}
