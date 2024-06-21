using Microsoft.EntityFrameworkCore;
using Moq;
using ProyectoService.AccesoDatos;
using ProyectoService.AccesoDatos.EntityFramework;
using ProyectoService.Aplicacion.CasosUso;
using ProyectoService.LogicaNegocio.Excepciones;
using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;
using ProyectoService.LogicaNegocio.Modelo.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService.Test
{
    [TestFixture]
    public class TestReparacion
    {
        private ProyectoServiceContext _context;
        private ReparacionEFRepositorio _reparacionRepositorio;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProyectoServiceContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ProyectoServiceContext(options);
            SeedData();
            _reparacionRepositorio = new ReparacionEFRepositorio(_context);
        }

        private void SeedData()
        {
            
            Tecnico tecnico = new Tecnico()
            {
                Nombre = "Nestor",
                Email = EmailVO.Crear("Nespime@gmail.com"),
                Rol = "Tecnico"
            };
            Cliente cliente = new Cliente()
            {
                Nombre = "Cliente 1"
            };

            Reparacion reparacion = new Reparacion()
            {
                Tecnico = tecnico,
                Cliente = cliente,
                Producto = "ps5",
                NumeroSerie = "21312312",
                Descripcion = "no prende"

            };

            _context.Reparaciones.Add(reparacion);
            _context.SaveChanges();
        }



        [Test]
        public void Add_ShouldAddReparacion()
        {
            // Arrange
            Tecnico tecnico = new Tecnico()
            {
                Nombre = "Nestor",
                Email = EmailVO.Crear("Nespime@gmail.com"),
                Rol = "Tecnico"
            };
            Cliente cliente = new Cliente()
            {
                Nombre = "Cliente 1"
            };

            Reparacion reparacion = new Reparacion()
            {
                Tecnico = tecnico,
                Cliente = cliente,
                Producto = "ps5",
                NumeroSerie = "21312312",
                Descripcion = "no prende"

            };

            // Act
            _reparacionRepositorio.Add(reparacion);

            // Assert
            var reparacionAgregada = _context.Tecnicos.FirstOrDefault(c => c.Email == tecnico.Email);
            Assert.IsNotNull(reparacionAgregada);
        }


        [Test]
        public async Task Add_ShouldThrowException_WhenInvalidEmail()
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                Tecnico tecnico = new Tecnico
                {
                    Nombre = "Roberto",
                    Apellido = "Gonzalez",
                    Email = EmailVO.Crear("robertoexample.com"), // email invalido
                    PasswordHash = ObtenerHash("password", ObtenerSalt()),
                    PasswordSalt = ObtenerSalt(),
                    Rol = "Tecnico"
                };
            });

            Assert.AreEqual("Email no valido", ex.Message);
        }





        private byte[] ObtenerSalt()
        {
            var salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] ObtenerHash(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                return pbkdf2.GetBytes(32);
            }
        }
    }
}

