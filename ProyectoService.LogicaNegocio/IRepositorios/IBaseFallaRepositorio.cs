﻿using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IRepositorios
{
    public interface IBaseFallaRepositorio:ICrudRepositorio<BaseFalla>
    {
        Task<List<BaseFalla>> ObtenerSegunDescripcion(string Descripcion);
        
    }
}
