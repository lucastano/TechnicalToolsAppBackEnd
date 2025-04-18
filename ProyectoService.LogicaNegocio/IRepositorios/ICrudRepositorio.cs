﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IRepositorios
{
    public interface ICrudRepositorio <T>
    {
        //CRUD
         Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task <List<T>> getAll();

    }
}
