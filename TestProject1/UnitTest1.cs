using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaceship__Server;
using System;

namespace TestProject1
{
    [TestClass]
    public class Entity_Test
    {

        [TestMethod]
        public void EntityCreation_Test()
        {
            //arrange
            Spaceship TestUnit = new Spaceship(new double[] { 12, 5, 0 }, new double[] { -7, 3, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            double[] expected = new double[] { 5, 8, 0 };

            MH.Move(TestUnit);

            double[] actual = TestUnit.Position;

            for (var i = 0; i < 3; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }

        }
    }
    [TestClass]
    public class UnknownPosition_Test
    {
        public class SpaceshipT 
        {
            public double[] Speed { set; get; }

            private double[] Position;

            public SpaceshipT()
            {
                Speed = new double[] { 0, 0, 0 };
                Position = new double[] { 0, 0, 0 };
            }
            public SpaceshipT(double[] spd, double[] pos)
            {
                Speed = spd;
                Position = pos;
            }
        }
        [TestMethod]
        public void Planet_Test()
        {

            //arrange
            SpaceshipT TestUnit = new SpaceshipT(new double[] { 12, 5, 0 }, new double[] { -7, 3, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            double[] expected = new double[] { 5, 8, 0 };

            Assert.ThrowsException<Exception>(() => MH.Move(TestUnit));
        }
    }
    [TestClass]
    public class UnknownSpeed_Test
    {
        public class SpaceshipT
        {
            private double[] Speed;

            public double[] Position { set; get; }

            public SpaceshipT()
            {
                Speed = new double[] { 0, 0, 0 };
                Position = new double[] { 0, 0, 0 };
            }
            public SpaceshipT(double[] spd, double[] pos)
            {
                Speed = spd;
                Position = pos;
            }
        }
        [TestMethod]
        public void Planet_Test()
        {

            //arrange
            SpaceshipT TestUnit = new SpaceshipT(new double[] { 12, 5, 0 }, new double[] { -7, 3, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            double[] expected = new double[] { 5, 8, 0 };

            Assert.ThrowsException<Exception>(() => MH.Move(TestUnit));
        }
    }

    [TestClass]
    public class Immovable_Test
    {
        public class SpaceshipT
        {
            public double[] Speed { set; get; }

            public double[] Position { get; }

            public SpaceshipT()
            {
                Speed = new double[] { 0, 0, 0 };
                Position = new double[] { 0, 0, 0 };
            }
            public SpaceshipT(double[] spd, double[] pos)
            {
                Speed = spd;
                Position = pos;
            }
        }
        [TestMethod]
        public void Planet_Test()
        {

            //arrange
            SpaceshipT TestUnit = new SpaceshipT(new double[] { 12, 5, 0 }, new double[] { -7, 3, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            double[] expected = new double[] { 5, 8, 0 };

            Assert.ThrowsException<Exception>(() => MH.Move(TestUnit));
        }
    }

    [TestClass]
    public class NonInterfaceClass_Test
    {

        [TestMethod]
        public void Planet_Test()
        {
            //arrange
            Planet TestUnit = new Planet(new double[] { 12, 5, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            double[] expected = new double[] { 5, 8, 0 };

            Assert.ThrowsException<Exception>(() => MH.Move(TestUnit));
        }
    }
}
