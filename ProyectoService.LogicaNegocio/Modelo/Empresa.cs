using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{
    [Table("Empresa")]
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion {  get; set; }
        public string Email { get; set; }
        //HAY QUE CIFRARLO, POR EL MOMENTO LO DEJAMOS ASI
        public string EmailPassword { get; set; }
        public byte []? Foto { get; set; }


        
        

    }
}
