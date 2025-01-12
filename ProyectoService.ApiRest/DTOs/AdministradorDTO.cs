namespace ProyectoService.ApiRest.DTOs
{
    public class AdministradorDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }

    }
}
