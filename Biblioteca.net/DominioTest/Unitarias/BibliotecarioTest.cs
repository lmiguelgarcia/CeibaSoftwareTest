using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
using BibliotecaDominio.IRepositorio;
using BibliotecaDominio.ReglaDeNegocio;
using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DominioTest.Unitarias
{
    [TestClass]
    public class BibliotecarioTest
    {
        public const string ISBN_SIN_STOCK = "ABC234";
        public const string ISBN_PALINDROMO = "B6AA6B";

        public BibliotecarioTest()
        {

        }
        public Mock<IRepositorioLibro> repositorioLibro;
        public Mock<IRepositorioPrestamo> repositorioPrestamo;
        public Mock<IReglaPrestamo> reglaPrestamo;

        [TestInitialize]
        public void setup()
        {
            repositorioLibro = new Mock<IRepositorioLibro>();
            repositorioPrestamo = new Mock<IRepositorioPrestamo>();
            reglaPrestamo = new Mock<IReglaPrestamo>();
        }

        [TestMethod]
        public void EsPrestado()
        {
            // Arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();

            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Returns(libro);

            // Act
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object, reglaPrestamo.Object);
            var esprestado = bibliotecario.EsPrestado(libro.Isbn);

            // Assert
            Assert.IsTrue(esprestado);
        }

        [TestMethod]
        public void LibroNoPrestadoTest()
        {
            // Arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object, reglaPrestamo.Object);
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Equals(null);

            // Act
            var esprestado = bibliotecario.EsPrestado(libro.Isbn);

            // Assert
            Assert.IsFalse(esprestado);
        }

        [TestMethod]
        public void ValidarLibroSinStock()
        {
            // arrange
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object, reglaPrestamo.Object);
            repositorioLibro.Setup(r => r.ObtenerPorIsbn(ISBN_SIN_STOCK)).Equals(null);

            try
            {
                // Act
                bibliotecario.ValidarLibro(ISBN_SIN_STOCK);
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual(Bibliotecario.LIBRO_SIN_STOCK, err.Message);
            }
        }

        [TestMethod]
        public void ValidarLibroEnStock()
        {
            // Arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libroEnStock = libroTestDataBuilder.Build();

            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libroEnStock.Isbn)).Equals(null);
            repositorioLibro.Setup(r => r.ObtenerPorIsbn(libroEnStock.Isbn)).Returns(libroEnStock);

            // Act
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object, reglaPrestamo.Object);
            Libro resultado = bibliotecario.ValidarLibro(libroEnStock.Isbn);

            // Assert
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void ValidarLibroUsoEnBiblioteca()
        {
            // arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libroPalindromo = libroTestDataBuilder.ConIsbn(ISBN_PALINDROMO).Build();

            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object, reglaPrestamo.Object);
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libroPalindromo.Isbn)).Equals(null);
            repositorioLibro.Setup(r => r.ObtenerPorIsbn(libroPalindromo.Isbn)).Returns(libroPalindromo);

            try
            {
                // Act
                bibliotecario.ValidarLibro(libroPalindromo.Isbn);
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual(Bibliotecario.LIBRO_PALINDROME, err.Message);
            }
        }

        [TestMethod]
        public void ValidarLibroPrestado()
        {
            // arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();

            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object, reglaPrestamo.Object);
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Returns(libro);
            repositorioLibro.Setup(r => r.ObtenerPorIsbn(libro.Isbn)).Returns(libro);

            try
            {
                // Act
                bibliotecario.ValidarLibro(libro.Isbn);
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual(Bibliotecario.EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE, err.Message);
            }
        }

    }
}
