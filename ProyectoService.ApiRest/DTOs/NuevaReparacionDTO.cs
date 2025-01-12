
using ProyectoService.LogicaNegocio.Modelo;
using System.ComponentModel.DataAnnotations;

namespace ProyectoService.ApiRest.DTOs
{
    public class NuevaReparacionDTO
    {

        [Required]
        public string CiCliente { get; set; }
        [Required]
        public int IdTecnico { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdProducto { get; set; }
        [Required]
        public string NumeroSerie { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public DateTime FechaPromesaPresupuesto { get; set; }
        
      
       
    }
}
