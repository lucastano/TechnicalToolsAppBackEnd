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
    public class SucursalEFRepositorio : ISucursalRepositorio
    {
        ProyectoServiceContext _context;
        public SucursalEFRepositorio(ProyectoServiceContext context)
        {
            _context = context;
        }

        //deprecado
        public Task Add(Sucursal entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Sucursal> Agregar(Sucursal suc)
        {
            if (suc == null) throw new Exception("No existe sucursal");
            if (suc.CodigoSucursal == null) throw new Exception("Debe ingresar un nombre");
            if(suc.Direccion == null) throw new Exception("Debe ingresar direccion");
            await _context.Sucursales.AddAsync(suc);
            await _context.SaveChangesAsync();
            return suc;
        }

        public Task Delete(Sucursal entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sucursal>> getAll()
        {
            return await _context.Sucursales.ToListAsync();
        }

        public async Task<Sucursal> Modificar(Sucursal entity)
        {
            Sucursal sucursalAModificar = await _context.Sucursales.FirstOrDefaultAsync(s => s.Id == entity.Id);
            if (sucursalAModificar == null) throw new Exception("Sucursal no existe");
            sucursalAModificar.CodigoSucursal = entity.CodigoSucursal;
            sucursalAModificar.Direccion = entity.Direccion;
            sucursalAModificar.Telefono = entity.Telefono;
            sucursalAModificar.Email = entity.Email;
            await _context.SaveChangesAsync();
            return sucursalAModificar;
        }

        public async Task<Sucursal?> ObtenerPorId(int id)
        {
            return await _context.Sucursales.Include(s=>s.Empresa).FirstOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<List<Sucursal>> obtenerSucursalesPorEmpresa(int idEmpresa)
        {
            return await _context.Sucursales.Include(s => s.Empresa).Where(s => s.Empresa.Id == idEmpresa).ToListAsync();
        }

        public async Task Update(Sucursal entity)
        {
            
            throw new NotImplementedException();
        }
    }
}
