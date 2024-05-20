using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class ClienteEFRepositorio : IClienteRepositorio
    {
        ProyectoServiceContext _context;
        
        public ClienteEFRepositorio(ProyectoServiceContext context)
        {
            _context = context;

        }
        public void Add(Cliente entity)
        {
            //VALIDACIONES
            //TODO:EXCEPCIONES CLIENTE
            
            if (entity.validarCi == null) { throw new Exception("cedula no valida"); }
            Cliente cliBuscado = GetClienteByCi(entity.Ci);
            if (cliBuscado != null) { throw new Exception("Cliente ya existe"); }
            _context.Clientes.Add(entity);
            _context.SaveChanges();

        }

        public void Delete(Cliente entity)
        {
            //Eliminar cliente no se hasta donde es valido, ya que el cliente va a tener historicos de servicios 
            throw new NotImplementedException();
        }

        public List<Cliente> getAll()
        {
            return  _context.Clientes.ToList();
            
        }

        //PUEDE DEVOLVER NULL
        public Cliente? GetClienteByCi(string ci)
        {
            return _context.Clientes.FirstOrDefault(c => c.Ci == ci);
        }

        public void Update(Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}
