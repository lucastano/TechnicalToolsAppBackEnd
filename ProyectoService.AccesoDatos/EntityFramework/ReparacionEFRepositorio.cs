using Microsoft.EntityFrameworkCore;
using ProyectoService.LogicaNegocio.Excepciones;
using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class ReparacionEFRepositorio : IReparacionRepositorio
    {
        ProyectoServiceContext _context;
        public ReparacionEFRepositorio(ProyectoServiceContext context)
        {
            _context = context;
        }
        public async Task Add(Reparacion entity)
        {
            if (entity == null) throw new ReparacionException("Debe ingresar una reparacion");
            if (entity.Cliente == null) throw new ReparacionException("Debe ingresar un cliente");
            if (entity.Descripcion == null) throw new ReparacionException("Debe ingresar una descripcion");
            if (entity.Producto == null) throw new ReparacionException("Debe ingresar un producto");
            //if (entity.Fecha.) throw new ReparacionException("Debe ingresar una fecha "); ver como comparar con fecha vacia
            if (entity.NumeroSerie == null) throw new ReparacionException("Debe ingresar numero de serie");
            await _context.Reparaciones.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(Reparacion entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reparacion>> getAll()
        {
            return await _context.Reparaciones.Include(r=>r.Tecnico).Include(r=>r.Cliente).ToListAsync();
        }

        public async Task Presupuestar(int id, double ManoObra, string Descripcion)
        {
            Reparacion reparacion = await ObtenerReparacionPorId(id);
            if (reparacion == null) throw new ReparacionException("Reparacion no existe");
            if (Descripcion == null) throw new ReparacionException("Debe ingresar una descripcion");
            if (ManoObra == 0) throw new ReparacionException("Debe ingresar un valor de mano de obra"); 
            reparacion.Presupuestar(ManoObra, Descripcion);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Reparacion>> ObtenerReparacionesPorCliente(string Ci)
        {
            //TODO:CHEQUEAR OBTENERREPARACIONESPORCLIENTE
            List<Reparacion> reparaciones = await getAll();
            return reparaciones.Where(r => r.Cliente.Ci == Ci).ToList();
        }

        public async Task<List<Reparacion>> ObtenerReparacionesPorTecnico(string EmailTecnico)
        {
            //TODO:CHEQUEAR ObtenerReparacionesPorTecnico
            List<Reparacion> reparaciones = await getAll();
            return reparaciones.Where(r=>r.Tecnico.Email.Value.Equals(EmailTecnico)).ToList();
        }

        //esta devuelve la reparacion por su id, involuntariamente de su estado
        public async Task<Reparacion> ObtenerReparacionPorId(int id)
        {
            var reparaciones = await _context.Reparaciones.ToListAsync();
            return reparaciones.FirstOrDefault(r => r.Id == id);
        }

        public Task Update(Reparacion entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reparacion>> ObtenerReparacionesPresupuestadas()
        {
            var reparaciones = await getAll();
            return reparaciones.Where(r => r.Estado == "Presupuestada").ToList();
        }

        public async Task<List<Reparacion>> ObtenerReparacionesPresupuestadasPorCliente(string ci)
        {
            if (ci == null || ci.Any()) throw new ReparacionException("Esta ci no existe o es invalida");
            var reparaciones = await getAll();
            return reparaciones.Where(r => r.Estado == "Presupuestada" && r.Cliente.Ci==ci).ToList();

        }

        public async Task<List<Reparacion>> ObtenerReparacionesPresupuestadasPorTecnico(string EmailTecnico)
        {
            if (EmailTecnico == null || EmailTecnico.Any()) throw new ReparacionException("Este email no existe o es invalida");
            var reparaciones = await  getAll();
            return reparaciones.Where(r => r.Estado == "Presupuestada" && r.Tecnico.Email.Equals(EmailTecnico)).ToList();
        }

        public async Task<List<Reparacion>> ObtenerReparacionesEnTaller()
        {
            
             var reparaciones = await getAll();
            return reparaciones.Where(r => r.Estado == "EnTaller").ToList();
        }

        public async Task<List<Reparacion>> ObtenerReparacionesEnTallerPorCliente(string ci)
        {
            if (ci == null || ci.Any()) throw new ReparacionException("Esta ci no existe o es invalida");
            var reparaciones = await getAll();
            return reparaciones.Where(r => r.Estado == "EnTaller" && r.Cliente.Ci == ci).ToList();
        }

        public async Task<List<Reparacion>> ObtenerReparacionesEnTallerPorTecnico(string EmailTecnico)
        {
            if (EmailTecnico == null || EmailTecnico.Any()) throw new ReparacionException("Este email no existe o es invalida");
            var reparaciones = await getAll();
            return reparaciones.Where(r => r.Estado == "EnTaller" && r.Tecnico.Email.Equals(EmailTecnico)).ToList();
        }

        public async Task Entregar(int id, bool reparada)
        {
            if (id == 0) throw new ReparacionException("reparacion no existe");
            Reparacion reparacion = await ObtenerReparacionPorId(id);
            reparacion.Entregar();
            await _context.SaveChangesAsync();

        }

        public async Task<List<Reparacion>> ObtenerReparacionesEntregadas()
        {
            var reparaciones = await getAll();
            return reparaciones.Where(r => r.Estado == "Entregada").ToList();
        }

        public async Task<List<Reparacion>> ObtenerReparacionesEntregadasPorCliente(string ci)
        {
            if (ci == null || ci.Any()) throw new ReparacionException("Esta ci no existe o es invalida");
            var reparaciones = await getAll();
            return reparaciones.Where(r => r.Estado == "Entregada" && r.Cliente.Ci == ci).ToList();
        }

        public async Task<List<Reparacion>> ObtenerReparacionesEntregadasPorTecnico(string EmailTecnico)
        {
            if (EmailTecnico == null || EmailTecnico.Any()) throw new ReparacionException("Este email no existe o es invalida");
            var reparaciones = await getAll();
            return reparaciones.Where(r => r.Estado == "Entregada" && r.Tecnico.Email.Equals(EmailTecnico)).ToList();
        }

        public Task<List<Reparacion>> ObtenerReparacionesTerminadas()
        {
            throw new NotImplementedException();
        }

        public Task<List<Reparacion>> ObtenerReparacionesTerminadasPorCliente(string ci)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reparacion>> ObtenerReparacionesTerminadasPorTecnico(string EmailTecnico)
        {
            throw new NotImplementedException();
        }
    }
}
