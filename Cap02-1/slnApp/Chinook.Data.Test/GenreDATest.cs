using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chinook.Data.Test
{
    [TestClass]
    public class GenreDATest
    {
        [TestMethod]
        public void InsertGenreTest()
        {
            var da = new GenreDA();
            var nuevoGenre = da.InsertGenre(
                new Entities.Genre() { Name = "Nuevo Genero" + Guid.NewGuid().ToString() });

            Assert.IsTrue(nuevoGenre > 0);

        }
    }
}
