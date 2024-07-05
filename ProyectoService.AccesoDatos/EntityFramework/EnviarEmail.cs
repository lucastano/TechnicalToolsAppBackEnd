using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.IO;


namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class EnviarEmail : IEnviarEmail
    {
        private  MailService _mailService;
        private readonly IEmpresaRepositorio empresaRepo;
        
        public  EnviarEmail( IEmpresaRepositorio empresaRepo)
        {
            this.empresaRepo = empresaRepo;
           

            //ver como obtener estos datos de algun archivo de texto

            //_mailService = new MailService("smtp.office365.com", 587, "pruebaemial.net@outlook.es", "Lu3472759");
        }
        
        public async Task EnviarEmailAvisoEntrega(Reparacion entity)
        {
            Empresa emp = await empresaRepo.GetEmpresa();
            string reparada = "Reparada";
            if (!entity.Reparada)
            {
                reparada = "NO Reparada";
            }
            byte[] pdfContent = entity.GenerarPdfOrdenServicioEntregada(emp);

            // Verifica si el PDF se generó correctamente
            if (pdfContent != null && pdfContent.Length > 0)
            {
                // Ver los datos de la empresa de donde obtenerlos, al igual que el email de envio
                string fromName = emp.Nombre;
                string fromEmail = emp.Email;
                string toName = entity.Cliente.Nombre;
                string toEmail = entity.Cliente.Email.Value;
                string subject = "REPARACION Entregada Nro: " + entity.Id;
                string body = "Se entrego el producto " + entity.Producto +"\n"
                            +"Número de serie: " + entity.NumeroSerie +"\n"
                            +"Número de orden es: " + entity.Id+"\n"
                            +"Problema reportado: "+entity.Descripcion+"\n"
                            +"Presupuesto: "+entity.DescripcionPresupuesto+"\n"
                            +"Estado de la reparacion: "+reparada+"\n"
                            +"Fecha y hora de entrega: "+entity.FechaEntrega+"\n"
                            +"Importe abonado: "+entity.CostoFinal+"\n"
                            +"Muchas gracias por confiar en nuestro servicio.";
                bool isHtml = true;

                // Configura el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail, fromName);
                mailMessage.To.Add(new MailAddress(toEmail, toName));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;

                // Adjunta el PDF al correo electrónico
                using (MemoryStream stream = new MemoryStream(pdfContent))
                {
                    mailMessage.Attachments.Add(new Attachment(stream, "orden_de_servicio_"+entity.Id+".pdf", "application/pdf"));

                    // Envía el correo electrónico
                    using (SmtpClient smtpClient = new SmtpClient("smtp.outlook.com", 587))
                    {
                        smtpClient.Credentials = new NetworkCredential(fromEmail, emp.EmailPassword);
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            
        }

        public async Task EnviarEmailAvisoTerminada(Reparacion entity)
        {
           

            // Verifica si el PDF se generó correctamente
            Empresa emp = await empresaRepo.GetEmpresa();
                // Ver los datos de la empresa de donde obtenerlos, al igual que el email de envio
                string fromName = emp.Nombre;
                string fromEmail = emp.Email;
                string toName = entity.Cliente.Nombre;
                string toEmail = entity.Cliente.Email.Value;
                string subject = "REPARACION TERMINADA ORDEN DE SERVICIO Nro: " + entity.Id;
                string body = "La reparacion de " + entity.Producto + ". Número de serie: " + entity.NumeroSerie + ". Su número de orden es: " + entity.Id+" fue terminada y esta lista para ser retirada, Muchas gracias.";
                bool isHtml = true;

                // Configura el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail, fromName);
                mailMessage.To.Add(new MailAddress(toEmail, toName));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;
            // Envía el correo electrónico
            using (SmtpClient smtpClient = new SmtpClient("smtp.outlook.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential(fromEmail, emp.EmailPassword);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }







        }

        public async Task EnviarEmailNuevaReparacion(Reparacion entity)
        {
            Empresa emp = await empresaRepo.GetEmpresa();
            _mailService = new MailService("smtp.office365.com", 587, emp.Email, emp.EmailPassword);
            byte[] pdfContent = entity.GenerarPdfOrdenServicioEntrada(emp);
            
            // Verifica si el PDF se generó correctamente
            if (pdfContent != null && pdfContent.Length > 0)
            {
                // Ver los datos de la empresa de donde obtenerlos, al igual que el email de envio
                string fromName = emp.Nombre;
                string fromEmail = emp.Email;
                string toName = entity.Cliente.Nombre;
                string toEmail = entity.Cliente.Email.Value;
                string subject = "ORDEN DE SERVICIO REPARACION Nro: " + entity.Id;
                string body = "Se dejó para service el aparato " + entity.Producto + ". Número de serie: " + entity.NumeroSerie +"\n" 
                    +"Número de orden: " + entity.Id+"\n"
                    +"Fecha aproximada del presupuesto: "+entity.FechaPromesaPresupuesto;
                bool isHtml = true;

                // Configura el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail, fromName);
                mailMessage.To.Add(new MailAddress(toEmail, toName));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;

                // Adjunta el PDF al correo electrónico
                using (MemoryStream stream = new MemoryStream(pdfContent))
                {
                    mailMessage.Attachments.Add(new Attachment(stream, "orden_de_servicio_" + entity.Id + ".pdf", "application/pdf"));

                    // Envía el correo electrónico
                    using (SmtpClient smtpClient = new SmtpClient("smtp.outlook.com", 587))
                    {
                        smtpClient.Credentials = new NetworkCredential(fromEmail, emp.EmailPassword);
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
           

        }

        public async Task EnviarEmailNuevoPresupuesto(Reparacion entity)
        {
            byte[] pdfContent = entity.GenerarPdfOrdenServicioPresupuestada();
            Empresa emp = await empresaRepo.GetEmpresa();
            // Verifica si el PDF se generó correctamente
            if (pdfContent != null && pdfContent.Length > 0)
            {
                // Ver los datos de la empresa de donde obtenerlos, al igual que el email de envio
                string fromName = emp.Nombre;
                string fromEmail = emp.Email;
                string toName = entity.Cliente.Nombre;
                string toEmail = entity.Cliente.Email.Value;
                string subject = "PRESUPUESTO ORDEN DE SERVICIO REPARACION Nro: " + entity.Id;
                string body = "El presupuesto para" + entity.Producto + " Número de serie: " + entity.NumeroSerie + " Con número de orden  " + entity.Id+"\n"
                               +"Descripcion del problema: "+entity.Descripcion+"\n"
                               +"Presupuesto: "+entity.DescripcionPresupuesto+"\n"
                               +"Costo: "+entity.CostoFinal+"\n"
                               +"Fecha prometida de entrega aproximada: "+entity.FechaPromesaEntrega+"\n"
                               +"Para aceptar el presupuesto comuniquese con la empresa. Gracias";
                bool isHtml = true;

                // Configura el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail, fromName);
                mailMessage.To.Add(new MailAddress(toEmail, toName));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;

                // Adjunta el PDF al correo electrónico
                using (MemoryStream stream = new MemoryStream(pdfContent))
                {
                    mailMessage.Attachments.Add(new Attachment(stream, "orden_de_servicio_" + entity.Id + ".pdf", "application/pdf"));

                    // Envía el correo electrónico
                    using (SmtpClient smtpClient = new SmtpClient("smtp.outlook.com", 587))
                    {
                        smtpClient.Credentials = new NetworkCredential(fromEmail, emp.EmailPassword);
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            
        }
        




    }
}
