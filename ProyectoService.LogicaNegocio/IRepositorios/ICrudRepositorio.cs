using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.IRepositorios
{
    public interface ICrudRepositorio <T>
    {
        //CRUD
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        List<T> getAll();

    }
}
