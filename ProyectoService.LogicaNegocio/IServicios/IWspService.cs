﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IServicios
{
    public interface IWspService
    {
        Task<string> EnviarWsp(string numero, string mensaje);
    }
}
