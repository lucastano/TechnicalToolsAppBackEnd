﻿using Microsoft.AspNetCore.Mvc;

namespace ProyectoService.ApiRest.DTOs
{
    public class AgregarEmpresaDTO
    {
        
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }

        [FromForm(Name = "Foto")]
        public IFormFile Foto { get; set; }
    }
}
