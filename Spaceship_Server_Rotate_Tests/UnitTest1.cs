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
}