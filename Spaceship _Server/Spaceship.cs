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
    }

    public class MovablesHandler
    {
        public void Move(object obj)
        {
                if (obj is IMovable movable)
                {
                    for (int i = 0; i < movable.Position.Length; i++)
                    {
                        movable.Position[i] += movable.Speed[i];
                    }
                }
                else
                {
                    throw(new Exception());
                }
        }
    }

    public class Planet 
    {
        public double[] Position { set; get; }

        public Planet()
        {
            Position = new double[] { 0, 0, 0 };
        }
        public Planet(double[] pos)
        {
            Position = pos;
        }
    }

    public class Spaceship : IMovable
    {
        public double[] Speed { set; get; }

        public double[] Position { set; get; }

        public Spaceship()
        {
            Speed = new double[] {0, 0, 0 };
            Position = new double[] { 0, 0, 0 };
        }
        public Spaceship(double[] spd, double[] pos)
        {
            Speed = spd;
            Position = pos;
        }
    }
}
