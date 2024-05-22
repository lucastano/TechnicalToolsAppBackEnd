using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.Aplicacion.CasosUso
{
    public class ObtenerTecnicoPorEmail : IObtenerTecnicoPorEmail
    {
        private readonly ITecnicoRepositorio repo;

        public ObtenerTecnicoPorEmail(ITecnicoRepositorio repo)
        {
            this.repo = repo;
        }

        public Tecnico Ejecutar(string email)
        {
            return repo.ObtenerTecnicoPorEmail(email);
        }
    }
}
