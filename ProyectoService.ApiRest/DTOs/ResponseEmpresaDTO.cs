namespace ProyectoService.ApiRest.DTOs
{
    public class ResponseEmpresaDTO
    {
        public int Id { get; set; }
        public string NombreFantasia { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroRUT { get; set; }
        public string Foto { get; set; }
        public string? PoliticasEmpresa { get; set; }
    }
}
