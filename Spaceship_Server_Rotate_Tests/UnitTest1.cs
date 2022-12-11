using System;
using Xunit;
using Spaceship__Server;
using Moq;


namespace Spaceship_Server_Rotate_Tests
{
    public class FractionTest
    {
        [Fact]
        public void RightSum()
        {
            Fraction a = new Fraction(1, 5);
            Fraction b = new Fraction(1, 6);
            Fraction c = a + b;

            Assert.Equal(c, new Fraction(11, 30));

        }
    }

    public class RotateTest
    {
        [Fact]
        public void MoqRotate()
        {
            Mock<IRotatable> Spaceship = new();
            Spaceship.SetupGet<Fraction>(m => m.angle[0]).Returns(new Fraction(1, 4)).Verifiable();
            Spaceship.SetupGet<Fraction>(m => m.angle_velocity[0]).Returns(new Fraction(1, 2)).Verifiable();
            RotateCommand rc = new RotateCommand(Spaceship.Object);
            rc.Execute();
            Assert.Equal(Spaceship.Object.angle[0], new Fraction(3, 4));


        }
        [Fact]
        public void CantReadAngle()
        {
            Mock<IRotatable> Spaceship = new();
            Spaceship.SetupGet<Fraction>(m => m.angle[0]).Throws<Exception>().Verifiable();
            RotateCommand rc = new RotateCommand(Spaceship.Object);
            Assert.Throws<Exception>(() => { rc.Execute(); });
        }
    }
}