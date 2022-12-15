using System;
using Moq;
using Spaceship__Server;
using Xunit;

namespace Spaceship_Server_Rotate_Tests
{
    public class RotateTest
    {
        public bool DidThrowTry(RotateCommand rc)
        {
            try
            {
                rc.Execute();
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }
        [Fact]
        public void MoqRotate()
        {
            Mock<IRotatable> Spaceship = new();
            Spaceship.SetupGet<Fraction[]>(m => m.angle).Returns(new Fraction[2] { new Fraction(1, 4), new Fraction(0, 0) });
            Spaceship.SetupGet<Fraction[]>(m => m.angle_velocity).Returns(new Fraction[2] { new Fraction(1, 2), new Fraction(0, 0) });
            RotateCommand rc = new RotateCommand(Spaceship.Object);
            DidThrowTry(rc);
            Assert.Equal(Spaceship.Object.angle[0], new Fraction(3, 4));
        }
        [Fact]
        public void CantReadAngle()
        {
            Mock<IRotatable> Spaceship = new();
            Spaceship.SetupGet<Fraction[]>(m => m.angle).Throws<Exception>().Verifiable();
            RotateCommand rc = new RotateCommand(Spaceship.Object);
            Assert.True(DidThrowTry(rc));
        }
        [Fact]
        public void CantReadAngleVelocity()
        {
            Mock<IRotatable> Spaceship = new();
            Spaceship.SetupGet<Fraction[]>(m => m.angle_velocity).Throws<Exception>().Verifiable();
            RotateCommand rc = new RotateCommand(Spaceship.Object);
            Assert.True(DidThrowTry(rc));
        }
        [Fact]
        public void CantChangeAngle()
        {
            Mock<IRotatable> Spaceship = new();
            Spaceship.SetupGet<Fraction[]>(m => m.angle).Returns(new Fraction[2] { new Fraction(1, 4), new Fraction(0, 0) });
            Spaceship.SetupGet<Fraction[]>(m => m.angle_velocity).Returns(new Fraction[2] { new Fraction(1, 2), new Fraction(0, 0) });
            Spaceship.SetupSet<Fraction[]>(m => m.angle = It.IsAny<Fraction[]>()).Throws<Exception>().Verifiable();
            RotateCommand rc = new RotateCommand(Spaceship.Object);
            Assert.True(DidThrowTry(rc));
        }
        [Fact]
        public void EmptyAngles()
        {
            Mock<IRotatable> Spaceship = new();
            RotateCommand rc = new RotateCommand(Spaceship.Object);
            Assert.True(DidThrowTry(rc));
        }
    }
}
