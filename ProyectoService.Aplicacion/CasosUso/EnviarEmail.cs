using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.Modelo;
using System.Net;
using System.Net.Mail;

namespace ProyectoService.Aplicacion.CasosUso
{
    public class EnviarEmail : IEnviarEmail
    {
        public async Task Ejecutar(Reparacion entity,Empresa empresa,Sucursal suc, byte[] pdf)
        {
           SmtpClient smtp = new SmtpClient("in.mailjet.com")
           {
               Port = 587,
               Credentials = new NetworkCredential(suc.apiKey, suc.secretKey),
               EnableSsl = true,
               DeliveryMethod = SmtpDeliveryMethod.Network,
               UseDefaultCredentials = false,
           };
            string fromName = empresa.NombreFantasia;
            string fromEmail = suc.Email;
            string toName = entity.Cliente.Nombre;
            string toEmail = entity.Cliente.Email.Value;
            string subject = "";
            string body = "";
            if(entity.Estado == "EnTaller") 
            {
                 subject = "Ingreso reparacion Nro: " + entity.Id;
                 body = $@"
                 <html>
                 <body>
                     <p>Se Ingreso el producto <strong>{entity.Producto.Marca}  {entity.Producto.Modelo} {entity.Producto.Version} </strong></p>
                     <p>Número de serie: <strong>{entity.NumeroSerie} </strong></p>
                     <p>Número de orden:<strong> {entity.Id}</strong></p>
                     <p>Problema reportado: <strong> {entity.Descripcion}</strong></p>
                     <p>Muchas gracias por confiar en nuestro servicio</p>
                 </body>
                 </html>";
            }
            if(entity.Estado == "Presupuestada")
            {
                 subject = "Ingreso reparacion Nro: " + entity.Id;
                 body = $@"
                 <html>
                 <body>
                     <p>Se Ingreso el producto <strong>{entity.Producto.Marca}  {entity.Producto.Modelo} {entity.Producto.Version} </strong></p>
                     <p>Número de serie: <strong>{entity.NumeroSerie} </strong></p>
                     <p>Número de orden:<strong> {entity.Id}</strong></p>
                     <p>Problema reportado: <strong> {entity.Descripcion}</strong></p>
                     <p>Presupuesto: <strong> {entity.DescripcionPresupuesto}</strong></p>
                     <p>Importe: <strong> {entity.CostoFinal}</strong></p>
                     <p>Por favor comuniquese con la empresa, Gracias</p>

                 </body>
                 </html>";
            }
            if(entity.Estado == "Entregada")
            {
                string reparada = "Reparada";
                if (!entity.Reparada)
                {
                    reparada = "NO Reparada";
                }
                subject = "Ingreso reparacion Nro: " + entity.Id;
                 body = $@"
                 <html>
                 <body>
                     <p>Se Ingresó el producto <strong>{entity.Producto.Marca}  {entity.Producto.Modelo} {entity.Producto.Version} </strong></p>
                     <p>Número de serie: <strong>{entity.NumeroSerie} </strong></p>
                     <p>Número de orden:<strong> {entity.Id}</strong></p>
                     <p>Problema reportado: <strong> {entity.Descripcion}</strong></p>
                     <p>Presupuesto: <strong> {entity.DescripcionPresupuesto}</strong></p>
                     <p>Estado de la reparación: <strong> {reparada}</strong></p>
                     <p>Fecha y hora de entrega: <strong> {entity.FechaEntrega}</strong></p>
                     <p>Importe abonado: <strong> {entity.CostoFinal}</strong></p>
                     <p>Muchas gracias por confiar en nuestro servicio</p>

                 </body>
                 </html>";
            }
            bool isHtml = true;
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };
            mailMessage.To.Add(new MailAddress(toEmail, toName));
            if (pdf != null)
            {
                using (MemoryStream stream = new MemoryStream(pdf))
                {
                    stream.Position = 0;
                    mailMessage.Attachments.Add(new Attachment(stream, "Orden_de_servicio_" + entity.Id + ".pdf", "application/pdf"));
                    await smtp.SendMailAsync(mailMessage);
                }
            }
            else
            {
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
