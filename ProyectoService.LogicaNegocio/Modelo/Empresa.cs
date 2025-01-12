using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{

    public class Empresa
    {
        public int Id { get; set; }
        public string NombreFantasia { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroRUT { get; set; }
        public string? Foto { get; set; }
        public string? PoliticasEmpresa { get; set; }

    }
}
