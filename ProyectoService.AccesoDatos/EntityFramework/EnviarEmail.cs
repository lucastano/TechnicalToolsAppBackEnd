using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class EnviarEmail : IEnviarEmail
    {
        private readonly MailService _mailService;

        public EnviarEmail()
        {
          
            //ver como obtener estos datos de algun archivo de texto
            _mailService = new MailService("smtp.office365.com", 587, "pruebaemial.net@outlook.es", "Lu3472759");
        }

        public Task EnviarEmailAvisoEntrega(Reparacion entity)
        {
            throw new NotImplementedException();
        }

        public Task EnviarEmailAvisoTerminada(Reparacion entity)
        {
            throw new NotImplementedException();
        }

        public async Task EnviarEmailNuevaReparacion(Reparacion entity)
        {
            //ver los datos de la empresa de donde obtenerlos, al igual que el email de envio
            await _mailService.SendEmailAsync(
                fromName: "CompanyName",
                fromEmail: "pruebaemial.net@outlook.es",
                toName: entity.Cliente.Nombre,
                toEmail: entity.Cliente.Email.Value,
                subject: "ORDEN DE SERVICIO REPARACION Nro: " + entity.Id,
                body: "Se dejo para service el aparato" + entity.Producto + " Numero de serie: " + entity.NumeroSerie + " Su numero de orden es: " + entity.Id,
                isHtml: true


                ) ;
        }

        public async Task EnviarEmailNuevoPresupuesto(Reparacion entity)
        {
            await _mailService.SendEmailAsync(
                fromName: "CompanyName",
                fromEmail: "pruebaemial.net@outlook.es",
                toName: entity.Cliente.Nombre,
                toEmail: entity.Cliente.Email.Value,
                subject: "NUEVO PRESUPUESTO REPARACION Nro: " + entity.Id,
                body: "Nuevo presupuesto para" + entity.Producto + " Numero de serie: " + entity.NumeroSerie + " numero de orden: " + entity.Id+" : "+entity.DescripcionPresupuesto+ "con un costo de: "+entity.CostoFinal,
                isHtml: true
                ) ;
        }

        
    }
}
