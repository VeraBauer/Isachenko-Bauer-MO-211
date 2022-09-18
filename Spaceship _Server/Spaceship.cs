using System;

namespace Spaceship__Server
{
    public interface IEntity
    {
        double[] Position { get; set; }
    }

    public interface IMovable : IEntity
    {
        double[] Speed { get; set; }
        public void Move();
    }

    public class Speacship : IMovable
    {
        public double[] Speed { set; get; }

        public double[] Position { set; get; }
        public void Move()
        {
            try
            {
                if (Speed.Length != Position.Length)
                {
                    throw new Exception("Неверный формат вектора скорости.") ;
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Не полетели...");
            }
            try
            {
                var temp = Position;
            }
            catch(Exception)
            {
                Console.WriteLine("Попытка сдвинуть объект, у которого невозможно прочитать положение объекта в пространстве.");
            }
            try
            {
                var temp = Speed;
            }
            catch (Exception)
            {
                Console.WriteLine("Попытка сдвинуть объект, у которого невозможно прочитать значение мгновенной скорости.");
            }
            try
            {
                for (var i = 0; i < Position.Length; i++)
                {
                    Position[i] += Speed[i];
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Попытка сдвинуть объект, у которого невозможно изменить положение в пространстве.");
            }
        }
    }
}
