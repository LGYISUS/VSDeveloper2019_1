using System;
using Chinook.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chinook.Data.Test
{
    [TestClass]
    public class ArtistDADapperTest
    {
        [TestMethod]
        public void GetCountTest()
        {
            var da = new ArtistDapperDA();

            Assert.IsTrue(da.GetCount() > 0);

        }



        [TestMethod]
        public void GetArtistByNameWithSPTest()
        {
            var da = new ArtistDapperDA();

            Assert.IsTrue(da.GetArtistsWithSP("a%").Count > 0);

        }


        [TestMethod]
        public void InsertArtistTest()
        {
            var da = new ArtistDapperDA();
            var nuevoArtista = da.InsertArtits(
                new Artist() { Name="Nuevo Artista"+Guid.NewGuid().ToString()});

            Assert.IsTrue(nuevoArtista > 0);

        }

        [TestMethod]
        public void InsertArtistWithOuputParamTest()
        {
            var da = new ArtistDapperDA();
            var nuevoArtista = da.InsertArtistWithOutput(
                new Artist() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() });

            Assert.IsTrue(nuevoArtista > 0);

        }

        [TestMethod]
        public void InsertArtitsWithTXTest()
        {
            var da = new ArtistDapperDA();
            var nuevoArtista = da.InsertArtitsWithTX(
                new Artist() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() });

            Assert.IsTrue(nuevoArtista > 0);

        }

        [TestMethod]
        public void InsertArtitsWithTXTestDist()
        {
            var da = new ArtistDapperDA();
            var nuevoArtista = da.InsertArtitsWithTXDist(
                new Artist() { Name = "Nuevo Artista" + Guid.NewGuid().ToString() });

            Assert.IsTrue(nuevoArtista > 0);

        }
    }
}
