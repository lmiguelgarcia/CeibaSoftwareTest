using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
namespace DominioTest.Unitarias
{
    [TestClass]
    public class LibroTest
    {
        private const int ANIO = 2012;
        private const string TITULO = "Cien años de soledad";
        private const string ISBN = "1234";

        private const string ISBN_PALINDROMO = "AB11BA";
        private const string ISBN_SUMA_CINCO = "L2M1C2";
        public LibroTest()
        {

        }

        [TestMethod]
        public void CrearLibroTest()
        {
            // Arrange
            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().ConTitulo(TITULO).
                ConAnio(ANIO).ConIsbn(ISBN);


            // Act
            Libro libro = libroTestBuilder.Build();

            // Assert
            Assert.AreEqual(TITULO, libro.Titulo);
            Assert.AreEqual(ISBN, libro.Isbn);
            Assert.AreEqual(ANIO, libro.Anio);
        }

        [TestMethod]
        public void ValidarIsbnEsPalindromo()
        {
            // arrange
            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().ConTitulo(TITULO).
                     ConAnio(ANIO).ConIsbn(ISBN_PALINDROMO);

            // act
            Libro libro = libroTestBuilder.Build();

            // assert
            Assert.IsTrue(libro.EsPalindromo());
        }


        [TestMethod]
        public void ValidarIsbnNoEsPalindromo()
        {
            // arrange
            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().ConTitulo(TITULO).
                ConAnio(ANIO).ConIsbn(ISBN);

            // act
            Libro libro = libroTestBuilder.Build();

            // assert
            Assert.IsFalse(libro.EsPalindromo());
        }

        [TestMethod]
        public void ValidarSumaIsbnEsCinco()
        {
            // arrange
            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().ConTitulo(TITULO).
                     ConAnio(ANIO).ConIsbn(ISBN_SUMA_CINCO);

            // act
            Libro libro = libroTestBuilder.Build();

            // assert
            Assert.AreEqual(5, libro.ObtenerSumaIsbn());
        }
    }
}
