namespace ProyectoService.ApiRest.DTOs
{
    public class ResponseEmpresaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }

        public string EmailPassword { get; set; }
        public string  Foto { get; set; }
    }
}
