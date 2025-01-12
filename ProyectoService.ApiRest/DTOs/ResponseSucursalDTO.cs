using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.ApiRest.DTOs
{
    public class ResponseSucursalDTO
    {
        public int Id { get; set; }
        public string CodigoSucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int IdEmpresa { get; set; }
    }
}
