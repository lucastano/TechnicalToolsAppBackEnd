using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoService.LogicaNegocio.Excepciones;

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
            
            try
            {

                if (entity.Nombre == null) throw new ClienteException("Debe ingresar nombre de cliente");
                if (entity.Apellido == null) throw new ClienteException("Debe ingresar apellido de cliente");
                if (entity.Telefono == null) throw new ClienteException("Debe ingresar telefono de cliente");
                if (entity.Ci == null) { throw new ClienteException("cedula no valida"); }
                if (!entity.validarCi()) { throw new ClienteException("Número de documento inválido"); }
                Cliente cliBuscado = GetClienteByCi(entity.Ci);
                if (cliBuscado != null) { throw new ClienteException("Cliente ya existe"); }
                _context.Clientes.Add(entity);
                _context.SaveChanges();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
           

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
            return _context.Clientes.AsEnumerable().FirstOrDefault(c => c.Ci == ci);
        }

        public void Update(Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}
