using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class TecnicoEFRepositorio : ITecnicoRepositorio
    {
        ProyectoServiceContext _context;
        public TecnicoEFRepositorio(ProyectoServiceContext context)
        {
            _context = context;
        }

        public void Add(Tecnico entity)
        {
            if (entity == null) throw new Exception("No ingreso los datos del tecnico");
           //TODO: ver validaciones para tecnico.
           _context.Tecnicos.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Tecnico entity)
        {
            throw new NotImplementedException();
        }

        public List<Tecnico> getAll()
        {
            return _context.Tecnicos.ToList();
        }

        public Tecnico? ObtenerTecnicoPorEmail(string email)
        {
            return _context.Tecnicos.FirstOrDefault(t => t.Email == email);
        }

        public void Update(Tecnico entity)
        {
            throw new NotImplementedException();
        }
    }
}
