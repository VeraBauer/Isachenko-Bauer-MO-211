using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Spaceship_Server.Tests
{
    [TestClass]
    public class Entity_Test
    {
        [TestMethod]
        public void main()
        {
            double[] cord = new double[3] { 1, 2, 3 };
            Entity ent = new Entity(cord);
            cord = new double[3] { 0, 0, 0 };
            double[] actual = ent.position;
            double[] expected = new double[3] { 1, 2, 3 };
            Assert.AreEqual(expected, actual);

        }
    }
}
