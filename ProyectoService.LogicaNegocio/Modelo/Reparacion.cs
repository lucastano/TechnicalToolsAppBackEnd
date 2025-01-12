
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;



namespace ProyectoService.LogicaNegocio.Modelo
{
	[Table("reparaciones")]
	public class Reparacion
	{

		public int Id { get; set; }
		public DateTime Fecha { get; set; } = DateTime.Now;
		public Tecnico Tecnico { get; set; }
		public Cliente Cliente { get; set; }
		public Producto Producto { get; set; }
		public string NumeroSerie { get; set; }
		public string Descripcion { get; set; }
		public DateTime FechaPromesaPresupuesto { get; set; }
		public string Estado { get; set; }
		public string DescripcionPresupuesto { get; set; }
		public string RazonNoAceptada { get; set; }
		public DateTime FechaPresupuesto { get; set; }
		public DateTime FechaPromesaEntrega { get; set; }
		public double ManoDeObra { get; set; }
		public double CostoFinal { get; set; }
		public DateTime FechaEntrega { get; set; }
		public bool Reparada { get; set; }
		public ICollection<Mensaje>Mensajes { get; set; }
		
		public Reparacion()
		{

			this.Estado = "EnTaller";
			this.DescripcionPresupuesto = string.Empty;
			this.ManoDeObra = 0;
			this.CostoFinal = 0;
			this.RazonNoAceptada = string.Empty;
			//QuestPDF.Settings.License = LicenseType.Community;
		}


		public void Presupuestar(double costo, string descripcion, DateTime fechaPromesaEntrega)
		{
			this.ManoDeObra = costo;
			this.CostoFinal = ManoDeObra;
			this.Estado = "Presupuestada";
			this.DescripcionPresupuesto = descripcion;
			this.FechaPresupuesto = DateTime.Now;
			this.FechaPromesaEntrega = fechaPromesaEntrega;
		}

		public void AceptarPresupuesto()
		{
			this.Estado = "PresupuestoAceptado";
		}
		public void NoAceptarPresupuesto(double costo, string razon)
		{
			this.Estado = "PresupuestoNoAceptado";
			this.RazonNoAceptada = razon;
			this.CostoFinal = costo;
		}
		public void Terminar(bool reparada)
		{
			
			this.Estado = "Terminada";
			this.Reparada = reparada;

		}

		public void Entregar()
		{
			this.FechaEntrega = DateTime.Now;
			this.Estado = "Entregada";
		}
		
    }










}
