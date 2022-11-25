using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Moq;
using Xunit;
using Spaceship__Server;


namespace SpaceshipServer.Texts.XUnit.Steps
{

    [Binding]
    public class VectorSteps
    {
        private Vector vector = new Vector();
        private Vector secondVector = new Vector();
        private Vector resultVector = new Vector();
        private string vec = new string("");

        [Given(@"Существует вектор \((.*), (.*)\)")]
        public void GivenVectorByCoords(int n1, int n2)
        {
            List<int> lis = new List<int> { n1, n2 };
            this.vector = new Vector(lis);
        }

        [Given(@"Существует вектор \((.*), (.*), (.*)\)")]
        public void GivenVectorByCoords(int n1, int n2, int n3)
        {
            this.vector = new Vector(n1, n2, n3);
        }

        [Given(@"Существует второй вектор \((.*), (.*)\)")]

        public void GivenSecondVectorByArrat(int n1, int n2)
        {
            this.secondVector = new Vector(n1, n2);
        }

        [Given(@"Существует второй вектор \((.*), (.*), (.*)\)")]

        public void GivenSecondVectorByArrat(int n1, int n2, int n3)
        {
            this.secondVector = new Vector(n1, n2, n3);
        }

        [When(@"Происходит операция сложения")]
        public void SumOperation()
        {
            this.resultVector = this.vector + this.secondVector;
        }

        [When(@"Происходит операция вычитания")]
        public void MinOperation()
        {
            this.resultVector = this.vector - this.secondVector;
        }

        [When(@"Операция вычитания выкидывает исключение")]
        public void MinOperationException()
        {
            bool didThrow = false;
            try
            {
                this.resultVector = this.vector - this.secondVector;
            }
            catch (ArgumentException)
            {
                didThrow = true;
            }
            Assert.True(didThrow);
        }


        [When(@"Операция сложения выкидывает исключение")]
        public void SumOperationException()
        {
            bool didThrow = false;
            try
            {
                this.resultVector = this.vector + this.secondVector;
            }
            catch (ArgumentException)
            {
                didThrow = true;
            }
            Assert.True(didThrow);
        }

        [When(@"Происходит помещение вектора в строку")]
        public void VectorToString()
        {
            vec = this.vector.ToString();
        }

        [When(@"Происходит замена (.*)-й координаты на (.*)")]
        public void SetCoord(int n1, int n2)
        {
            this.resultVector = this.vector;
            this.resultVector[n1] = n2;
        }

        [When(@"Происходит замена (.*)-й координаты на (.*), кидается ArgumentException")]
        public void SetCoordException(int n1, int n2)
        {
            this.resultVector = this.vector;
            bool didThrow = false;
            try
            {
                this.resultVector[n1] = n2;
            }
            catch (ArgumentException)
            {
                didThrow = true;
            }
            Assert.True(didThrow);
        }

        [Then(@"При получении (.*)-й координаты имеем (.*)")]
        public void GetCoord(int n1, int n2)
        {
            this.resultVector = this.vector;
            Assert.Equal(this.resultVector[n1], n2);
        }

        [Then(@"При получении (.*)-й координаты имеем ArgumentException")]
        public void GetCoordException(int n1)
        {
            this.resultVector = this.vector;
            int n = 0;
            bool didThrow = false;
            try
            {
                n = this.resultVector[n1];
            }
            catch (ArgumentException)
            {
                didThrow = true;
            }
            Assert.True(didThrow);
        }

        [Then(@"Метод GetHashCode работает верно")]
        public void HashCodeTrue()
        {
            var hc = this.vector.GetHashCode();
            Assert.True(hc == this.vector.GetHashCode());
        }

        [Then(@"Операция сравнения возвращает True")]
        public void EqualVectorsTrue()
        {
            Assert.True(this.secondVector == this.vector);
        }

        [Then(@"Операция сравнения возвращает False")]
        public void EqualVectorsFalse()
        {
            Assert.True(!(this.secondVector == this.vector));
        }

        [Then(@"Операция неравенства возвращает False")]
        public void UnequalVectorsFalse()
        {
            Assert.True(!(this.secondVector != this.vector));
        }

        [Then(@"Операция неравенства возвращает True")]
        public void UnequalVectorsTrue()
        {
            Assert.True(this.secondVector != this.vector);
        }

        [Then(@"Значение строки результата Vector\((.*), (.*)\)")]
        public void AssertString(int n1, int n2)
        {
            string expected = string.Format("Vector({0}, {1})", n1, n2);
            Assert.Equal(expected, this.vec);
        }

        [Then(@"Значение вектора результата \((.*), (.*)\)")]
        public void AssertResult(int n1, int n2)
        {
            Vector expected = new Vector(n1, n2);
            Vector actual = this.resultVector;
            Assert.True(expected == actual);
        }

        [Then(@"Метод Equals возвращает True")]
        public void EqualsTrue()
        {
            Assert.True(vector.Equals(secondVector));
        }

        [Then(@"Метод Equals возвращает False при сравнении с объектом неверного типа")]
        public void EqualsFalseWrongType()
        {
            Assert.True(!(vector.Equals(vec)));
        }

        [Then(@"Метод Equals возвращает False")]
        public void EqualsFalse()
        {
            Assert.True(!(vector.Equals(secondVector)));
        }

    }
    [Binding]
    public class MoveSteps
    {
        private readonly Mock<IMovable> mockMovable = new();

        [Given(@"Тело находится в точке \((.*), (.*)\) пространства")]
        public void GiveTheBodyByCoords(int c1, int c2)
        {
            this.mockMovable.SetupGet<Vector>(m => m.Position).Returns(new Vector (c1, c2 )).Verifiable();
        }

        [Given(@"Невозможно установить новое положение в пространстве")]
        public void UnableToMove()
        {
            this.mockMovable.SetupSet<Vector>(m => m.Position = It.IsAny<Vector>()).Throws<Exception>().Verifiable();
        }

        [Given(@"Тело, у которого невозможно определить положение в пространстве")]
        public void GiveTheBodyUnknownCoords()
        {
            this.mockMovable.SetupGet<Vector>(m => m.Position).Throws<Exception>().Verifiable();
        }

        [Given(@"Имеет скорость \((.*), (.*)\)")]
        public void BodyGotVelocity(int n1, int n2)
        {
            this.mockMovable.SetupGet<Vector>(m => m.Speed).Returns(new Vector (n1, n2 )).Verifiable();
        }

        [Given(@"Имеет скорость \((.*), (.*), (.*)\)")]
        public void BodyGotBigVelocity(int n1, int n2, int n3)
        {
            this.mockMovable.SetupGet<Vector>(m => m.Speed).Returns(new Vector( n1, n2, n3 )).Verifiable();
        }
        [Given(@"Скорость Тела нечитаема")]
        public void BodyGotUnknownVelocity()
        {
            this.mockMovable.SetupGet<Vector>(m => m.Speed).Throws<Exception>().Verifiable();
        }

        [When(@"Выполняется операция движения тела")]
        public void MoveAttempt()
        {
            new MoveCommand(this.mockMovable.Object).Execute();
        }

        [When(@"Попытка движения приводит к исключению")]
        public void TryMoveGetExeption()
        {
            bool didThrow = false;
            try
            {
                new MoveCommand(this.mockMovable.Object).Execute();
            }
            catch (Exception)
            {
                didThrow = true;
            }
            Assert.True(didThrow);
        }

        [Then(@"Тело имеет новые координаты \((.*), (.*)\)")]
        public void AssertCoordinates(int n1, int n2)
        {
            Vector expected = new Vector ( n1, n2 );
            Vector actual = new Vector (this.mockMovable.Object.Position.coords);
            Assert.True(expected==actual);
        }
    }
}
