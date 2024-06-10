using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.LogicaNegocio.Modelo
{
    public class Reparacion
    {

        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public Tecnico Tecnico { get; set; }
        public Cliente Cliente { get; set; }
        public string Producto { get; set; }//esto va a pasar a ser un objeto
        public string NumeroSerie { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }//ver de hacer un enum
        public string DescripcionPresupuesto { get; set; }
        public double ManoDeObra { get; set; }
        //public List<string> Repuestos { get; set; } = new List<String>({""};// va a pasar a ser una lista de objetos 
        public double CostoFinal { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool Reparada {  get; set; }
        

        //TODO: DEBERIA SETEAR LOS DATOS QUE NO VOY A UTILIZAR CUANDO CREO EL OBJETO(COSTO,MANOOBRA,DESCRIPCIONPRESUPUESTO,REPUESTOS)????
        public Reparacion()
        {

            this.Estado = "EnTaller";
            this.DescripcionPresupuesto = string.Empty;
            this.ManoDeObra = 0;
            this.CostoFinal = 0;
        }
        

        public void Presupuestar(double costo,string descripcion)
        {
            this.ManoDeObra = costo;
            this.CostoFinal = ManoDeObra;
            this.Estado = "Presupuestada";
            this.DescripcionPresupuesto=descripcion;
            //TODO: para calcular el costo final, este metodo debe recibir la lista de repuestos a utilizar, se recorre esa lista y por cada repuesto se toma el costo
            //ese costo se suma en una variable que al finalizar vamos a utilizar para sumarle  a la mano de obra y ahi obtenemos el costo final
            //TODO: ACA TENEMOS QUE MANEJAR EL ENVIO DEL EMAIL
            
        }
        public void Terminar(bool reparada)
        {
            if (!reparada)
            {
                //si no se realizo la reparacion, el costo final es 0, se mantiene la mano de obra para tener esa info
                this.CostoFinal = 0;
            }
            this.Reparada = reparada;

        }

        public void Entregar()
        {   
            this.FechaEntrega=DateTime.Now;  
            this.Estado = "Entregada";     
        }


 




    }
}
