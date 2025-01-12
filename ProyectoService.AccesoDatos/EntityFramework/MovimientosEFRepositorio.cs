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
    public class MovimientosEFRepositorio : IMovimientoRepositorio
    {
        ProyectoServiceContext _context;
        public MovimientosEFRepositorio(ProyectoServiceContext context)
        {
            _context = context;
        }

        public async  Task<Movimiento> DeshacerMovimiento(Movimiento movimiento)
        {
            Tecnico tecnicoAsignado = movimiento.TecnicoAsigando;
            Tecnico tecnicoDesasignado = movimiento.TecnicoDesasignado;
            movimiento.TecnicoAsigando = tecnicoDesasignado;
            return movimiento;
            //AUN NO QUEDO, VER 
        }

        public async Task<Movimiento> NuevoMovimiento(Movimiento movimiento)
        {
            if (movimiento == null) throw new Exception("Debe ingresar un movimiento");
            if (movimiento.TecnicoDesasignado == null) throw new Exception("Debe ingresar un tecnico desasignado");
            if (movimiento.TecnicoAsigando == null) throw new Exception("Debe ingresar un tecnico asignado");
            if (movimiento.Motivo == null) throw new Exception("Debe ingresar un motivo");
            await _context.Movimientos.AddAsync(movimiento);
            await _context.SaveChangesAsync();
            return movimiento;
        }

        public async Task<List<Movimiento>> ObtenerMovimientos()
        {
            return await _context.Movimientos.Include(m=>m.TecnicoDesasignado).Include(m=>m.TecnicoAsigando).Include(m=>m.Motivo).ToListAsync();
        }

        public async Task<List<Movimiento>> obtenerMovimientosPorReparacion(Reparacion reparacion)
        {
            return await _context.Movimientos.Include(m => m.TecnicoDesasignado).Include(m => m.TecnicoAsigando).Include(m => m.Motivo).Where(m => m.Reparacion.Id == reparacion.Id).ToListAsync();
        }

        public async Task<List<Movimiento>> obtenerMovimientosPorTecnico(int idTecnico)
        {
            return await _context.Movimientos.Include(m => m.TecnicoDesasignado).Include(m => m.TecnicoAsigando).Include(m => m.Motivo).Where(m => m.TecnicoAsigando.Id == idTecnico).ToListAsync();
        }
    }
}
