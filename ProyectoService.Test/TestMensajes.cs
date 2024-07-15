using Microsoft.EntityFrameworkCore;
using ProyectoService.AccesoDatos.EntityFramework;
using ProyectoService.AccesoDatos;
using ProyectoService.LogicaNegocio.Modelo.ValueObjects;
using ProyectoService.LogicaNegocio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.Test
{

    [TestFixture]
    public class TestMensajes
    {
        private ProyectoServiceContext _context;
        private MensajeriaEFRepositorio _mensajesRepositorio;
        

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProyectoServiceContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ProyectoServiceContext(options);
            SeedData();
            _mensajesRepositorio = new MensajeriaEFRepositorio(_context);
            
        }

        private void SeedData()
        {

            var mensaje = new Mensaje
            {
               ReparacionId=1,
               DestinatarioId=1,
               EmisorId=2,
               Texto="Hola",
               FechaHoraEnvio=DateTime.Now,

            };
            _context.Mensajes.Add(mensaje);
            _context.SaveChanges();
        }



        [Test]
        public async Task Add_ShouldAddReparacion()
        {
            //// Arrange
            ////BUSCO EL TECNICO POR EMAIL
            //string email = "juan@example.com";
            //Tecnico tecnico = await _tecnicoEFRepositorio.ObtenerTecnicoPorEmail(email);

            ////BUSCO EL CLIENTE POR CI 
            //string ciCliente = "48161123";
            //Cliente cliente = await _clienteEFRepositorio.GetClienteByCi(ciCliente);

            //Reparacion reparacion = new Reparacion()
            //{
            //    Tecnico = tecnico,
            //    Cliente = cliente,
            //    Producto = "ps5",
            //    NumeroSerie = "21312312",
            //    Descripcion = "no prende",
            //    FechaPromesaPresupuesto = new DateTime(2024, 07, 10)


            //};

            //// Act
            //Reparacion reparacionRet = await _reparacionRepositorio.AddAlternativo(reparacion);

            //// Assert

            //Assert.IsNotNull(reparacionRet);
        }
    }
}
