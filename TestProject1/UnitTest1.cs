using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaceship__Server;
using System;
using System.Collections.Generic;

namespace TestProject1
{
    [TestClass]
    public class Entity_Test
    {

        [TestMethod]
        public void EntityCreation_Test()
        {
            //arrange
            Spaceship TestUnit = new Spaceship(new List<int> { 12, 5, 0 }, new List<int> { -7, 3, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            double[] expected = new double[] { 5, 8, 0 };

            MH.Move(TestUnit);

            List<int> actual = TestUnit.Position;

            for (var i = 0; i < 3; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }

        }
    }

    [TestClass]
    public class Move_Test
    {

        [TestMethod]
        public void MoveCommand_Test()
        {
            //arrange
            Spaceship TestUnit = new Spaceship(new List<int> { 12, 5, 0 }, new List<int> { -7, 3, 0 });
            //act
            MoveCommand mc = new MoveCommand(TestUnit);
            //assert
            List<int> expected = new List<int> { 5, 8, 0 };

            mc.Execute();

            List<int> actual = TestUnit.Position;

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
            public List<int> Speed { set; get; }

            private List<int> Position;

            public SpaceshipT()
            {
                Speed = new List<int> { 0, 0, 0 };
                Position = new List<int> { 0, 0, 0 };
            }
            public SpaceshipT(List<int> spd, List<int> pos)
            {
                Speed = spd;
                Position = pos;
            }
        }
        [TestMethod]
        public void Planet_Test()
        {

            //arrange
            SpaceshipT TestUnit = new SpaceshipT(new List<int> { 12, 5, 0 }, new List<int> { -7, 3, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            List<int> expected = new List<int> { 5, 8, 0 };

            Assert.ThrowsException<Exception>(() => MH.Move(TestUnit));
        }
    }
    [TestClass]
    public class UnknownSpeed_Test
    {
        public class SpaceshipT
        {
            private List<int> Speed;

            public List<int> Position { set; get; }

            public SpaceshipT()
            {
                Speed = new List<int> { 0, 0, 0 };
                Position = new List<int> { 0, 0, 0 };
            }
            public SpaceshipT(List<int> spd, List<int> pos)
            {
                Speed = spd;
                Position = pos;
            }
        }
        [TestMethod]
        public void Planet_Test()
        {

            //arrange
            SpaceshipT TestUnit = new SpaceshipT(new List<int> { 12, 5, 0 }, new List<int> { -7, 3, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            List<int> expected = new List<int> { 5, 8, 0 };

            Assert.ThrowsException<Exception>(() => MH.Move(TestUnit));
        }
    }

    [TestClass]
    public class Immovable_Test
    {
        public class SpaceshipT
        {
            public List<int> Speed { set; get; }

            public List<int> Position { get; }

            public SpaceshipT()
            {
                Speed = new List<int> { 0, 0, 0 };
                Position = new List<int> { 0, 0, 0 };
            }
            public SpaceshipT(List<int> spd, List<int> pos)
            {
                Speed = spd;
                Position = pos;
            }
        }
        [TestMethod]
        public void Planet_Test()
        {

            //arrange
            SpaceshipT TestUnit = new SpaceshipT(new List<int> { 12, 5, 0 }, new List<int> { -7, 3, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            List<int> expected = new List<int> { 5, 8, 0 };

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
            Planet TestUnit = new Planet(new List<int> { 12, 5, 0 });
            //act
            MovablesHandler MH = new MovablesHandler();
            //assert
            List<int> expected = new List<int> { 5, 8, 0 };

            Assert.ThrowsException<Exception>(() => MH.Move(TestUnit));
        }
    }
}
