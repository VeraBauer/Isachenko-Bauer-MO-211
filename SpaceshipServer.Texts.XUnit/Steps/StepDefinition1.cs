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
    public class MoveSteps
    {
        private readonly Mock<IMovable> mockMovable = new();

        [Given(@"Тело находится в точке \((.*), (.*)\) пространства")]
        public void GiveTheBodyByCoords(int c1, int c2)
        {
            this.mockMovable.SetupGet<List<int>>(m => m.Position).Returns(new List<int> { c1, c2 }).Verifiable();
        }

        [Given(@"Невозможно установить новое положение в пространстве")]
        public void UnableToMove()
        {
            this.mockMovable.SetupSet<List<int>>(m => m.Position = It.IsAny<List<int>>()).Throws<Exception>().Verifiable();
        }

        [Given(@"Тело, у которого невозможно определить положение в пространстве")]

        public void GiveTheBodyUnknownCoords()
        {
            this.mockMovable.SetupGet<List<int>>(m => m.Position).Throws<Exception>().Verifiable();
        }

        [Given(@"Имеет скорость \((.*), (.*)\)")]
        public void BodyGotVelocity(int n1, int n2)
        {
            this.mockMovable.SetupGet<List<int>>(m => m.Speed).Returns(new List<int> { n1, n2 }).Verifiable();
        }

        [Given(@"Имеет скорость \((.*), (.*), (.*)\)")]
        public void BodyGotBigVelocity(int n1, int n2, int n3)
        {
            this.mockMovable.SetupGet<List<int>>(m => m.Speed).Returns(new List<int> { n1, n2, n3 }).Verifiable();
        }
        [Given(@"Скорость Тела нечитаема")]
        public void BodyGotUnknownVelocity()
        {
            this.mockMovable.SetupGet<List<int>>(m => m.Speed).Throws<Exception>().Verifiable();
        }

        [When(@"Выполняется операция движения тела")]
        public void MoveAttempt()
        {
            new MoveCommand(this.mockMovable.Object).Execute();
        }

        [When(@"Попытка движения приводит к исключению")]
        public void TryMoveGetExeption()
        {
            Assert.Throws<Exception>(() => new MoveCommand(this.mockMovable.Object).Execute());
        }

        [Then(@"Тело имеет новые координаты \((.*), (.*)\)")]
        public void AssertCoordinates(int n1, int n2)
        {
            List<int> expected = new List<int> { n1, n2 };
            List<int> actual = this.mockMovable.Object.Position;
            Assert.Equal(expected, actual);
        }
    }
}
