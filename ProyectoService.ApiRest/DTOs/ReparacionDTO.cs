using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.ApiRest.DTOs
{
    public class ReparacionDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } 
        public string Producto { get; set; }//esto va a pasar a ser un objeto
        public string NumeroSerie { get; set; }
        public string Descripcion { get; set; }
       
        public string Estado { get; set; }//ver de hacer un enum
        
    }
}
