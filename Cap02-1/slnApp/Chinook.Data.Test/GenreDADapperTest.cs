using System;
using Chinook.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chinook.Data.Test
{
    [TestClass]
    public class GenreDADapperTest
    {
        [TestMethod]
        public void GetCountTest()
        {
            var da = new GenreDapperDA();

            Assert.IsTrue(da.GetCount() > 0);

        }



        [TestMethod]
        public void GetGenreByNameTest()
        {
            var da = new GenreDapperDA();

            Assert.IsTrue(da.GetGenre("a%").Count > 0);

        }

        [TestMethod]
        public void GetGenreByNameWithSPTest()
        {
            var da = new GenreDapperDA();

            Assert.IsTrue(da.GetGenreWithSP("a%").Count > 0);

        }


        [TestMethod]
        public void InsertGenreTest()
        {
            var da = new GenreDapperDA();
            var nuevoGenero = da.InsertGenre(
                new Genre() { Name="Nuevo Genero"+Guid.NewGuid().ToString()});

            Assert.IsTrue(nuevoGenero > 0);

        }

        [TestMethod]
        public void InsertGenreWithOuputParamTest()
        {
            var da = new GenreDapperDA();
            var nuevoGenero = da.InsertGenreWithOutput(
                new Genre() { Name = "Nuevo Genero" + Guid.NewGuid().ToString() });

            Assert.IsTrue(nuevoGenero > 0);

        }

        [TestMethod]
        public void InsertGenreWithTXTest()
        {
            var da = new GenreDapperDA();
            var nuevoGenero = da.InsertGenreWithTX(
                new Genre() { Name = "Nuevo Genero" + Guid.NewGuid().ToString() });

            Assert.IsTrue(nuevoGenero > 0);

        }

        [TestMethod]
        public void InsertGenreWithTXTestDist()
        {
            var da = new GenreDapperDA();
            var nuevoGenero = da.InsertGenreWithTXDist(
                new Genre() { Name = "Nuevo Genero" + Guid.NewGuid().ToString() });

            Assert.IsTrue(nuevoGenero > 0);

        }
    }
}
