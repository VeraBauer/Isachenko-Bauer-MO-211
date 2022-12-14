using System;
using Xunit;
using Spaceship__Server;
using Moq;


namespace Spaceship_Server_Rotate_Tests
{
    public class FractionTest
    {
        [Fact]
        public void RightSum_dif_denom()
        {
            Fraction a = new Fraction(1, 2);
            Fraction b = new Fraction(2, 4);
            Fraction c = a + b;
            Assert.Equal(c, new Fraction(1, 1));
        }
        [Fact]
        public void UnequalityTest_true()
        {
            Assert.True(new Fraction(6, 10) != new Fraction(3, 6));
        }
        [Fact]
        public void EqualityTest_true()
        {
            Assert.True(new Fraction(1, 2) == new Fraction(2, 4));
        }
        [Fact]
        public void UnequalityTest_false()
        {
            Assert.True(!(new Fraction(2, 4) != new Fraction(1, 2)));
        }
        [Fact]
        public void EqualityTest_false()
        {
            Assert.True(!(new Fraction(3, 5) == new Fraction(4, 7)));
        }
        [Fact]
        public void Equals_false()
        {
            Assert.False(new Fraction(3, 5).Equals(new Fraction(4, 7)));
        }
        [Fact]
        public void Equals_true()
        {
            Assert.True(new Fraction(3, 5).Equals(new Fraction(6, 10)));
        }
        [Fact]
        public void Equals_False()
        {
            Assert.False(new Fraction(3, 5).Equals("Hello"));
        }
        [Fact]
        public void HashCodeTest()
        {
            Fraction a = new Fraction(1, 2);
            int Return = HashCode.Combine(1, 2);
            Assert.Equal(a.GetHashCode(), Return);
        }
        [Fact]
        public void ToStringTest()
        {
            Fraction a = new Fraction(1, 2);
            Assert.Equal("1/2", a.ToString());
        }
        [Fact]
        public void ReductableSum()
        {
            Assert.Equal(new Fraction(2, 4)+new Fraction(2, 4), new Fraction(1, 1));
        }
        [Fact]
        public void ReductableFraction()
        {
            Fraction a = new Fraction(2, 4);
            Assert.Equal(a, new Fraction(1, 2));
        }
        [Fact]
        public void ReductableFractionTest()
        {
            Fraction a = new Fraction(4, 2);
            Assert.Equal(a, new Fraction(2, 1));
        }
    }
}

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
