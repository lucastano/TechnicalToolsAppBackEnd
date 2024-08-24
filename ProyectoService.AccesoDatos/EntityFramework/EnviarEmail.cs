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
using Microsoft.Extensions.Configuration;
using Mailjet.Client;
using Newtonsoft.Json.Linq;
using Mailjet.Client.Resources;
using Mailjet.Client;


namespace ProyectoService.AccesoDatos.EntityFramework
{
    public class EnviarEmail : IEnviarEmail
    {
       
        private readonly IConfiguration configuration;
        SmtpClient smtpClient;
        private Empresa empresa;
        
        
        
        public  EnviarEmail( IConfiguration configuration)
        {
            this.configuration = configuration;
            var configNombreEmpresa = configuration.GetSection("EmpresaSettings:NombreEmpresa").Value!;
            var configDireccionEmpresa = configuration.GetSection("EmpresaSettings:DireccionEmpresa").Value!;
            var configTelefonoEmpresa = configuration.GetSection("EmpresaSettings:TelefonoEmpresa").Value!;
            var configEmail = configuration.GetSection("EmpresaSettings:Email").Value!;
            var configPassword = configuration.GetSection("EmpresaSettings:EmailPassword").Value!;
            var configPoliticasEmpresa = configuration.GetSection("EmpresaSettings:PoliticasEmpresa").Value!;
            var emailServer = configuration.GetSection("EmpresaSettings:EmailServer").Value!;
            var apiKey= configuration.GetSection("EmpresaSettings:APIKEY").Value!;
            var secretKey = configuration.GetSection("EmpresaSettings:SECRETKEY").Value!;

            Empresa empresaConfig = new Empresa()
            {
                Nombre = configNombreEmpresa,
                Direccion = configDireccionEmpresa,
                Telefono = configTelefonoEmpresa,
                Email = configEmail,
                EmailPassword = configPassword,
                PoliticasEmpresa = configPoliticasEmpresa
            };
            this.empresa = empresaConfig;
           
            smtpClient = new SmtpClient("in.mailjet.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(apiKey, secretKey),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                

            };

            

        }

        public async Task AvisoCambioPassword(Usuario usu,string password)
        {
            string fromName = this.empresa.Nombre;
            string fromEmail = this.empresa.Email;
            string toName = usu.Nombre;
            string toEmail = usu.Email.Value;
            string subject = "RECUPERACION DE CONTRASEÑA: " + usu.Email.Value;
            //string body = "Nuevo Password generado para inicio de session "
            //            + "Para mantener la proteccion de su usuario, por favor cambie el password luego de iniciar"
            //            + ": " + password.ToString();
            string body = $@"
             <html>
             <body>
                 <p>Nueva contraseña generado para inicio de sesión.</p>
                 <p>Para mantener la proteccion de su usuario, por favor cambie la contraseña luego de iniciar.</p>
                 <p><strong>Contraseña:</strong> <strong>{password}</strong></p>
             </body>
             </html>";

            bool isHtml = true;

            // Configura el mensaje de correo electrónico
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml

            };
            mailMessage.To.Add(new MailAddress(toEmail, toName));
            await smtpClient.SendMailAsync(mailMessage);

        }

        public async Task<byte[]> EnviarEmailAvisoEntrega(Reparacion entity)
        {
            
            string reparada = "Reparada";
            if (!entity.Reparada)
            {
                reparada = "NO Reparada";
            }
            byte[] pdfContent = entity.GenerarPdfOrdenServicioEntregada(this.empresa);

            // Verifica si el PDF se generó correctamente
            if (pdfContent != null && pdfContent.Length > 0)
            {
                // Ver los datos de la empresa de donde obtenerlos, al igual que el email de envio
                string fromName = this.empresa.Nombre;
                string fromEmail = this.empresa.Email;
                string toName = entity.Cliente.Nombre;
                string toEmail = entity.Cliente.Email.Value;
                string subject = "REPARACION Entregada Nro: " + entity.Id;
                string body = "Se entrego el producto " + entity.Producto.Marca + " " + entity.Producto.Modelo + " " + entity.Producto.Version + "\n"
                            +"Número de serie: " + entity.NumeroSerie +"\n"
                            +"Número de orden es: " + entity.Id+"\n"
                            +"Problema reportado: "+entity.Descripcion+"\n"
                            +"Presupuesto: "+entity.DescripcionPresupuesto+"\n"
                            +"Estado de la reparacion: "+reparada+"\n"
                            +"Fecha y hora de entrega: "+entity.FechaEntrega+"\n"
                            +"Importe abonado: "+entity.CostoFinal+"\n"
                            +"Muchas gracias por confiar en nuestro servicio.";
                bool isHtml = false;

                // Configura el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml

                 };
                mailMessage.To.Add(new MailAddress(toEmail, toName));


            // Adjunta el PDF al correo electrónico
            using (MemoryStream stream = new MemoryStream(pdfContent))
                {
                    mailMessage.Attachments.Add(new Attachment(stream, "orden_de_servicio_"+entity.Id+".pdf", "application/pdf"));

                    // Envía el correo electrónico
                    
                        try
                        {
                            await smtpClient.SendMailAsync(mailMessage);

                        }
                        catch (SmtpException ex)
                        {

                        }
                    
                }
            }
            return pdfContent;


        }

        public async Task EnviarEmailAvisoTerminada(Reparacion entity)
        {
           

            // Verifica si el PDF se generó correctamente
            
                // Ver los datos de la empresa de donde obtenerlos, al igual que el email de envio
                string fromName = this.empresa.Nombre;
                string fromEmail = this.empresa.Email;
                string toName = entity.Cliente.Nombre;
                string toEmail = entity.Cliente.Email.Value;
                string subject = "REPARACION TERMINADA ORDEN DE SERVICIO Nro: " + entity.Id;
                string body = "La reparacion de " + entity.Producto.Marca + " " + entity.Producto.Modelo + " " + entity.Producto.Version + ". Número de serie: " + entity.NumeroSerie + ". Su número de orden es: " + entity.Id+" fue terminada y esta lista para ser retirada, Muchas gracias.";
                bool isHtml = false;

            // Configura el mensaje de correo electrónico
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml

            };
            mailMessage.To.Add(new MailAddress(toEmail, toName));
            // Envía el correo electrónico

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);

                }
                catch (SmtpException ex)
                {

                }
        }

        public async Task<byte[]> EnviarEmailNuevaReparacion(Reparacion entity)
        {

            byte[] pdfContent = entity.GenerarPdfOrdenServicioEntrada(this.empresa);
            

            // Verifica si el PDF se generó correctamente
            if (pdfContent != null && pdfContent.Length > 0)
            {
                // Ver los datos de la empresa de donde obtenerlos, al igual que el email de envío
                string fromName = empresa.Nombre;
                string fromEmail = empresa.Email;
                string toName = entity.Cliente.Nombre;
                string toEmail = entity.Cliente.Email.Value;
                string subject = "ORDEN DE SERVICIO REPARACION Nro: " + entity.Id;
                string body = "Se dejó para service el aparato " +entity.Producto.Marca+" " +entity.Producto.Modelo +" "+entity.Producto.Version+" "+ ". Número de serie: " + entity.NumeroSerie + "\n"
                    + "Número de orden: " + entity.Id + "\n"
                    + "Fecha aproximada del presupuesto: " + entity.FechaPromesaPresupuesto;
                bool isHtml = false;
                // Configura el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml,
                    
                };
                mailMessage.To.Add(new MailAddress(toEmail, toName));

                //mailMessage.From = new MailAddress(fromEmail, fromName);
                //mailMessage.To.Add(new MailAddress(toEmail, toName));
                //mailMessage.Subject = subject;
                //mailMessage.Body = body;
                //mailMessage.IsBodyHtml = isHtml;
                mailMessage.ReplyToList.Add(new MailAddress("soporte@tudominio.com", "Soporte Técnico")); // Agrega un Reply-To
                mailMessage.Headers.Add("X-Mailer", "Microsoft Outlook 16.0"); // Cabecera para ayudar a la clasificación
                mailMessage.Headers.Add("X-Priority", "3");
                // Adjunta el PDF al correo electrónico
                using (MemoryStream stream = new MemoryStream(pdfContent))
                {
                    mailMessage.Attachments.Add(new Attachment(stream, "orden_de_servicio_" + entity.Id + ".pdf", "application/pdf"));
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);

                    }
                    catch (SmtpException ex)
                    {

                    }
   
                }

            }
            return pdfContent;

        }

        public async Task EnviarEmailNuevoPresupuesto(Reparacion entity)
        {
            byte[] pdfContent = entity.GenerarPdfOrdenServicioPresupuestada(this.empresa);
           
            // Verifica si el PDF se generó correctamente
            if (pdfContent != null && pdfContent.Length > 0)
            {
                // Ver los datos de la empresa de donde obtenerlos, al igual que el email de envio
                string fromName = this.empresa.Nombre;
                string fromEmail = this.empresa.Email;
                string toName = entity.Cliente.Nombre;
                string toEmail = entity.Cliente.Email.Value;
                string subject = "PRESUPUESTO ORDEN DE SERVICIO REPARACION Nro: " + entity.Id;
                string body = "El presupuesto para" + entity.Producto.Marca + " " + entity.Producto.Modelo + " " + entity.Producto.Version + " Número de serie: " + entity.NumeroSerie + " Con número de orden  " + entity.Id+"\n"
                               +"Descripcion del problema: "+entity.Descripcion+"\n"
                               +"Presupuesto: "+entity.DescripcionPresupuesto+"\n"
                               +"Costo: "+entity.CostoFinal+"\n"
                               +"Fecha prometida de entrega aproximada: "+entity.FechaPromesaEntrega+"\n"
                               +"Para aceptar el presupuesto comuniquese con la empresa. Gracias";
                bool isHtml = false;

                // Configura el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml

                };
                
                mailMessage.To.Add(new MailAddress(toEmail, toName));
               

                // Adjunta el PDF al correo electrónico
                using (MemoryStream stream = new MemoryStream(pdfContent))
                {
                    mailMessage.Attachments.Add(new Attachment(stream, "orden_de_servicio_" + entity.Id + ".pdf", "application/pdf"));

                    // Envía el correo electrónico
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);


                    }
                    catch (SmtpException ex)
                    {

                    }
                }
            }
            
        }
        




    }
}
