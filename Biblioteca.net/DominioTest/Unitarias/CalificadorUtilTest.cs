using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
namespace DominioTest.Unitarias
{
    [TestClass]
    public class CalificadorUtilTest
    {

        public CalificadorUtilTest()
        {

        }

        [TestMethod]
        public void ValidarNoEsDomingo()
        {
            //arrange
            DateTime date = new DateTime(2019, 7, 2);// Martes 2 de Julio de 2019

            //act
            bool resultado = CalificadorUtil.NoEsDomingo(date);

            // assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ValidarEsDomingo()
        {
            //arrange
            DateTime date = new DateTime(2019, 7, 7);// Domingo 7 de Julio de 2019

            //act
            bool resultado = CalificadorUtil.NoEsDomingo(date);

            // assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ValidarDisminuirDiasNoEsdomingo()
        {
            //arrange
            DateTime date = new DateTime(2019, 7, 3);// Miercoles 3 de Julio de 2019

            //act
            int resultado = CalificadorUtil.DisminuirDiasNoEsdomingo(date, 5);

            // assert
            Assert.AreEqual(4, resultado);
        }

        [TestMethod]
        public void ValidarDisminuirDiasEsdomingo()
        {
            //arrange
            DateTime date = new DateTime(2019, 7, 7);// Domingo 7 de Julio de 2019

            //act
            int resultado = CalificadorUtil.DisminuirDiasNoEsdomingo(date, 5);

            // assert
            Assert.AreEqual(5, resultado);
        }

        [TestMethod]
        public void ValidarSumarOnceDiasSinContarDomingo()
        {
            //arrange
            DateTime date = new DateTime(2019, 7, 1);// Lunes 1 de Julio de 2019
            DateTime dateMas11Dias = new DateTime(2019, 7, 12);// Viernes 12 de Julio de 2019

            //act
            DateTime resultado = CalificadorUtil.SumarDiasSinContarDomingo(date, 11);

            // assert
            Assert.AreEqual(dateMas11Dias, resultado);
        }

    }
}
