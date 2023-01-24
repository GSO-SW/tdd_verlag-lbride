using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Verlag;

namespace VerlagTests
{
    [TestClass]
    public class ISBNTests
    {
        [TestMethod]
        public void ISBN13_KannHinzugefuegtWerden()
        {
            //Arrange
            string isbn = "123-4567891234";
            Buch b = new("Autor von Goethe", "Epischer Titel");

            //Act
            b.ISBN13 = isbn;

            //Assert
            Assert.AreEqual(isbn, b.ISBN13);
        }

        [TestMethod]
        public void ISBN13_BerechnetPruefziffer()
        {
            //Arrange
            string isbn = "978-377043614";
            Buch b = new("Autor von Goethe", "Epischer Titel");

            //Act
            b.ISBN13 = isbn;

            //Assert
            Assert.AreEqual("978-3770436149", b.ISBN13);
        }

        [TestMethod]
        public void ISBN10_KannBerechnetWerden()
        {
            //Arrange
            string isbn = "978-3770436064";
            Buch b = new("Autor von Goethe", "Epischer Titel");

            //Act
            b.ISBN13 = isbn;

            //Assert
            Assert.AreEqual("3770436067", b.ISBN10);
        }
    }
}
