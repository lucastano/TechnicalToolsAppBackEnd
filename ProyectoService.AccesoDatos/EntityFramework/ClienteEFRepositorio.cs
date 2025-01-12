﻿using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoService.LogicaNegocio.Excepciones;
using ProyectoService.LogicaNegocio.Validaciones;

namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class ClienteEFRepositorio : IClienteRepositorio
    {
        ProyectoServiceContext _context;
        
        public ClienteEFRepositorio(ProyectoServiceContext context)
        {
            _context = context;

        }

        //DEPRECADO
        public async Task Add(Cliente entity)
        {
            if (entity.Nombre == null) throw new ClienteException("Debe ingresar nombre de cliente");
            entity.Nombre=ValidacionesTexto.FormatearTexto(entity.Nombre); 
            if (entity.Apellido == null) throw new ClienteException("Debe ingresar apellido de cliente");
            entity.Apellido=ValidacionesTexto.FormatearTexto(entity.Apellido);
            if (entity.Telefono == null) throw new ClienteException("Debe ingresar telefono de cliente");
            if (entity.Ci == null)  throw new ClienteException("cedula no valida"); 
            if (!entity.validarCi())  throw new ClienteException("Número de documento inválido"); 
            Cliente cliBuscado = await GetClienteByCi(entity.Ci);
            if (cliBuscado != null)  throw new ClienteException("Cliente ya existe"); 
            await _context.Clientes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> Agregar(Cliente cliente)
        {
            if (cliente.Nombre == null) throw new ClienteException("Debe ingresar nombre de cliente");
            cliente.Nombre = ValidacionesTexto.FormatearTexto(cliente.Nombre);
            if (cliente.Apellido == null) throw new ClienteException("Debe ingresar apellido de cliente");
            cliente.Apellido = ValidacionesTexto.FormatearTexto(cliente.Apellido);
            if (cliente.Telefono == null) throw new ClienteException("Debe ingresar telefono de cliente");
            if (cliente.Ci == null) throw new ClienteException("cedula no valida");
            if (!cliente.validarCi()) throw new ClienteException("Número de documento inválido");
            Cliente cliBuscado = await GetClienteByCi(cliente.Ci);
            if (cliBuscado != null) throw new ClienteException("Cliente ya existe");
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task Delete(Cliente entity)
        {
            
            throw new NotImplementedException();
        }

        public async Task<List<Cliente>> getAll()
        {
            return  await _context.Clientes.ToListAsync();
            
        }

        
        public async Task<Cliente?> GetClienteByCi(string ci)
        {
            if (ci == null) throw new ClienteException("Debe ingresar una ci");
            
            return  await _context.Clientes.FirstOrDefaultAsync(c=>c.Ci==ci);
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            if (id == 0) throw new ClienteException("El id es incorrecto");
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}
