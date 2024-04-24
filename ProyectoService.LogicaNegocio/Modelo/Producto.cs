using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{
    public class Producto
    {
        public int Id { get; set; }
        public string Marca { get; set; }

        public string  Modelo { get; set; }


        public Producto(string marca, string modelo) 
        {
            this.Marca = marca;
            this.Modelo = modelo;
        
        }





    }
}
