using Microsoft.EntityFrameworkCore;
using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;


namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class AuthService : IAuthService
    {
        ProyectoServiceContext _context;

        public AuthService(ProyectoServiceContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Login(string emailRecibido,string rol)
        { 
            string email=emailRecibido.ToLower();
            if(email==null) throw new Exception("Debe ingresar email");
            if (rol == null || (rol!="Tecnico" && rol !="Administrador" && rol !="Cliente")) throw new Exception("debe ingresar un rol de usuario valido");
            Usuario user = null;
            if (rol == "Administrador")
            {
                var listaAdministradores = await _context.Administradores.Include(a=> a.Empresa).ToListAsync();
                
                Administrador admin = listaAdministradores.FirstOrDefault(c => c.Email.Value.Equals(email));

                if (admin == null) throw new Exception("No existe usuario para Administrador con ese email");
                user = admin;

            }
            if (rol == "Tecnico")
            {
                var listaTecnicos = await _context.Tecnicos.Include(t=>t.Empresa).ToListAsync();
                Tecnico tecnico = listaTecnicos.FirstOrDefault(c => c.Email.Value.Equals(email));
                if (tecnico == null) throw new Exception("No existe usuario para Tecnico con ese email");
                user= tecnico;
            }

            if (rol == "Cliente")
            {
                var listaClientes= await _context.Clientes.ToListAsync();
                Cliente cliente = listaClientes.FirstOrDefault(c => c.Email.Value.Equals(email));
                if (cliente == null) throw new Exception("No existe usuario para Cliente con ese email");
                user= cliente;
            }

            if (user == null) throw new Exception("Usuario no existe o sus credenciales no coinciden");

            return user;
            
           


        }

       
    }
}
