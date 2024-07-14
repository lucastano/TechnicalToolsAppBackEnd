using Microsoft.EntityFrameworkCore;
using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class MensajeriaEFRepositorio : IMensajeriaRepositorio
    {
        ProyectoServiceContext _context;
        public MensajeriaEFRepositorio(ProyectoServiceContext context)
        {
            _context = context;
        }

        public async Task EliminarMensajesReparacion(int idReparacion)
        {
            if (idReparacion == 0) throw new Exception("Id incorrecto");
            List<Mensaje>mensajes= await ObtenerMensajesDeReparacion(idReparacion);
            if (mensajes.Count > 0)
            {
                _context.Mensajes.RemoveRange(mensajes);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task NuevoMensaje(Mensaje msg)
        {
                if (msg == null) throw new Exception("Debe ingresar un mensaje");
                if (msg.Texto == null) throw new Exception("Falta el cuerpo del mensaje");

                if (msg.EmisorId == 0) throw new Exception("Falta emisor del mensaje");
                if (msg.DestinatarioId == 0) throw new Exception("Falta el destinatario del mensaje");
                await _context.Mensajes.AddAsync(msg);
                await _context.SaveChangesAsync();

        }

        public async Task<List<Mensaje>> ObtenerMensajesDeReparacion(int idReparacion)
        {
            List<Mensaje>retorno = await _context.Mensajes.Include(m => m.Destinatario).Include(m => m.Emisor).Include(m => m.Reparacion).Where(m => m.ReparacionId == idReparacion).ToListAsync();
            return retorno;
        }
    }
}
