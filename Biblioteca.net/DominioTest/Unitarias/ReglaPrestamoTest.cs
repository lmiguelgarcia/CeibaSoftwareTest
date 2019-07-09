using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
namespace DominioTest.Unitarias
{
    [TestClass]
    public class ReglaPrestamoTest
    {
        public const string ISBN_MAS_DE30 = "9M9M8P89";

        public const string ISBN_MENOS_DE30 = "9M9M2P";

        public ReglaPrestamoTest()
        {

        }

        [TestMethod]
        public void ValidarFechaPrestamoConISBN30()
        {
            // Arrange
            ReglaPrestamo reglaPrestamo = new ReglaPrestamo();

            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().
                ConIsbn(ISBN_MAS_DE30);


            // Act
            Libro libro = libroTestBuilder.Build();

            DateTime fecha = reglaPrestamo.FechaPrestamo(libro);

            // Assert
            Assert.AreNotEqual(DateTime.MinValue, fecha);

        }

        [TestMethod]
        public void ValidarFechaPrestamosSinISBN30()
        {
            // Arrange
            ReglaPrestamo reglaPrestamo = new ReglaPrestamo();

            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().
                ConIsbn(ISBN_MENOS_DE30);

            // Act
            Libro libro = libroTestBuilder.Build();

            DateTime fecha = reglaPrestamo.FechaPrestamo(libro);

            // Assert
            Assert.AreEqual(DateTime.MinValue, fecha);

        }


    }
}
