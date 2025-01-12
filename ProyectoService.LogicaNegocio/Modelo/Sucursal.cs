using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string CodigoSucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Empresa Empresa { get; set; }
        public string? EmailServer { get; set; }
        public string? apiKey { get; set; }
        public string? secretKey { get; set; }
        public bool avisosEmail { get; set; }
        public bool avisosWsp { get; set; }

        public Sucursal() 
        {
            this.avisosEmail = true;
            this.avisosWsp = false;
        }
    }
}
