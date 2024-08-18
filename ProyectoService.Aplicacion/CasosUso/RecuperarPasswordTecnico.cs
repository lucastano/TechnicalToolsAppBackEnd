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
    public class RecuperarPasswordTecnico : IRecuperarPasswordTecnico
    {
        private readonly ITecnicoRepositorio _repositorio;
        public RecuperarPasswordTecnico(ITecnicoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Ejecutar(Tecnico tecnico)
        {
           return await _repositorio.RecuperarPassword(tecnico);
        }
    }
}
