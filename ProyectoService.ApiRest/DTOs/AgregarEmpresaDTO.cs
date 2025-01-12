using Microsoft.AspNetCore.Mvc;

namespace ProyectoService.ApiRest.DTOs
{
    public class AgregarEmpresaDTO
    {
        public string NombreFantasia { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroRUT { get; set; }
        [FromForm(Name = "Foto")]
        public IFormFile Foto { get; set; }
        public string? PoliticasEmpresa { get; set; }
    }
}
