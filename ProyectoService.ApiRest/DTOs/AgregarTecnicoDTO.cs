﻿namespace ProyectoService.ApiRest.DTOs
{
    public class AgregarTecnicoDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int EmpresaId { get; set; }
        public int SucursalId { get; set; }
    }
}
