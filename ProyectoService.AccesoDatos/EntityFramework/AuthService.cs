using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class AuthService : IAuthService
    {
        ProyectoServiceContext _context;

        public AuthService(ProyectoServiceContext context)
        {
            _context = context;
        }

        public Usuario Login(string email)
        { 
        //validar formato de email, o deberia ser en el front ? 
            //valido que no se le pase un email vacio
            if(email==null) throw new Exception("Debe ingresar email");
            
            Usuario usuario = _context.Clientes.FirstOrDefault(c => c.Email.Value.Equals(email));
            if (usuario == null)
            {
                usuario = _context.Tecnicos.FirstOrDefault(c => c.Email.Value.Equals(email));
            }

            if (usuario == null)
            {
                usuario = _context.Administradores.FirstOrDefault(c => c.Email.Value.Equals(email));
            }

            return usuario;


        }
    }
}
