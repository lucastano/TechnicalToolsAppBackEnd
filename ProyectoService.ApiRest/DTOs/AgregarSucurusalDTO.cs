using Microsoft.AspNetCore.Mvc;

namespace ProyectoService.ApiRest.DTOs
{
    public class AgregarSucurusalDTO
    {
        public string CodigoSucursal { get; set; } 
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public int IdEmpresa { get; set; }
    }
}
