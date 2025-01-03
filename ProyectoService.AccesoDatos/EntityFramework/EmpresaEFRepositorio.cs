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
    public class EmpresaEFRepositorio : IEmpresaRepositorio
    {
        ProyectoServiceContext _context;
        public EmpresaEFRepositorio(ProyectoServiceContext context)
        {
            _context = context;
        }

        public async Task<Empresa> AgregarEmpresa(Empresa emp)
        {
            await _context.Empresas.AddAsync(emp);
            await _context.SaveChangesAsync();
            return emp;
        }

        public async Task<Empresa?> getEmpresa()
        {
            return await _context.Empresas.SingleOrDefaultAsync();
        }

        public async Task<Empresa> ModificarEmpresa(Empresa emp)
        {
            Empresa empresa = await ObtenerEmpresaPorId(emp.Id);
            empresa.Direccion = emp.Direccion;
            empresa.Telefono = emp.Telefono;
            empresa.Foto = emp.Foto;
            empresa.Email = emp.Email;
            await _context.SaveChangesAsync();
            return empresa;
        }

        public async Task<Empresa?> ObtenerEmpresaPorId(int id)
        {
            return await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
