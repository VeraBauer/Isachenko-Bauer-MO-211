using System;
using System.Collections.Generic;

namespace Spaceship__Server
{
    public interface ICommand
    {
        public void Execute();
    }

    public class Vector

    {
        public List<int> coords { get; set; } = new List<int>();

        public Vector(List<int> lis)

        {

            for (int i = 0; i < lis.Count; i++)

            {

                coords.Add(lis[i]);

            }

        }

        public Vector(params int[] args)

        {

            for (int i = 0; i < args.Length; i++)

            {

                coords.Add(args[i]);

            }

        }

        public override string ToString()

        {

            string str = "";

            str += "Vector(";

            for (int i = 0; i < coords.Count; i++)

            {

                if (i != coords.Count - 1)

                {

                    str += coords[i] + ", ";

                }

                else

                {

                    str += coords[i];

                }

            }

            str += ")";

            return str;

        }

#nullable enable
        public override bool Equals(object? obj)
        {
            if (!(obj is Vector vector))
            {
                return false;
            }

            if (vector.coords.Count != this.coords.Count)
            {
                return false;
            }

            for (int i = 0; i < this.coords.Count; i++)
            {
                if (this.coords[i] != vector.coords[i])
                {
                    return false;
                }
            }
            return true;

        }
#nullable disable
        public override int GetHashCode()
        {
            return HashCode.Combine(coords, this.coords.Count);
        }

        public int this[int index]

        {

            get

            {
                if (index <= this.coords.Count)
                {
                    return coords[index];
                }
                else
                {
                    throw new ArgumentException();
                }

            }
            set

            {
                if (index <= this.coords.Count)
                {
                    coords[index] = value;
                }
                else
                {
                    throw new ArgumentException();
                }

            }

        }

        public static Vector operator +(Vector a, Vector b)

        {

            if (a.coords.Count != b.coords.Count)

            {

                throw new ArgumentException();

            }

            else

            {

                List<int> ansvec = new List<int>();

                for (int i = 0; i < a.coords.Count; i++)

                {

                    ansvec.Add(a[i] + b[i]);

                }

                Vector ans = new Vector(ansvec);

                return ans;

            }

        }

        public static Vector operator -(Vector a, Vector b)

        {

            if (a.coords.Count != b.coords.Count)

            {

                throw new ArgumentException();

            }

            else

            {

                List<int> ansvec = new List<int>();

                for (int i = 0; i < a.coords.Count; i++)

                {

                    ansvec.Add(a[i] - b[i]);

                }

                Vector ans = new Vector(ansvec);

                return ans;

            }

        }

        public static bool operator ==(Vector a, Vector b)

        {

            int size = Math.Max(a.coords.Count, b.coords.Count);

            if (a.coords.Count != b.coords.Count)

            {

                return false;

            }

            for (int i = 0; i < size; i++)

            {

                if (a[i] != b[i])

                {

                    return false;

                }

            }

            return true;

        }

        public static bool operator !=(Vector a, Vector b)

        {

            if (a == b)

            {

                return false;

            }

            else

            {

                return true;

            }

        }
    }

    public interface IMovable
    {
        Vector Speed { get; }
        Vector Position { get; set; }
    }
    public class Fraction
    {
        int numerator { get; set; }
        int denominator { get; set; }
        public Fraction(int num, int den)
        {
            numerator = num;
            denominator = den;
        }
        public static Fraction operator +(Fraction a, Fraction b)
        {
            if (a.denominator != b.denominator)
            {
                int denom = a.denominator*b.denominator;
                int numer_a = a.numerator*b.denominator;
                int numer_b = b.numerator*a.denominator;
                int numer = numer_a + numer_b;
                int min = Math.Min(denom, numer);
                for (int i = min; i>1; i--)
                {
                    if ((numer % i==0) && (denom % i==0))
                    {
                        numer /= i;
                        denom /= i;
                    }
                }
                return new Fraction(numer, denom);
            }
            else
            {
                int numer = a.numerator+b.numerator;
                int denom = a.denominator;
                int mimin = Math.Min(denom, numer);
                for (int i = mimin; i>1; i--)
                {
                    if (numer%i==0 && denom%i==0)
                    {
                        numer /= i;
                        denom /= i;
                    }
                }
                return new Fraction(numer, denom);
            }
        }
        public static bool operator ==(Fraction a, Fraction b)
        {
            int mimin = Math.Min(a.denominator, a.numerator);
            for (int i = mimin; i>1; i--)
            {
                if (a.numerator%i==0 && a.denominator%i==0)
                {
                    a.numerator /= i;
                    a.denominator /= i;
                }
            }
            int mmin = Math.Min(b.denominator, b.numerator);
            for (int i = mmin; i>1; i--)
            {
                if (b.numerator%i==0 && b.denominator%i==0)
                {
                    b.numerator /= i;
                    b.denominator /= i;
                }
            }
            if (a.numerator != b.numerator || a.denominator != b.denominator)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            int mimin = Math.Min(a.denominator, a.numerator);
            for (int i = mimin; i>1; i--)
            {
                if (a.numerator%i==0 && a.denominator%i==0)
                {
                    a.numerator /= i;
                    a.denominator /= i;
                }
            }
            int mmin = Math.Min(b.denominator, b.numerator);
            for (int i = mmin; i>1; i--)
            {
                if (b.numerator%i==0 && b.denominator%i==0)
                {
                    b.numerator /= i;
                    b.denominator /= i;
                }
            }
            if (a.numerator == b.numerator && a.denominator == b.denominator)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
#nullable enable
        public override bool Equals(object? obj)
        {
            if (!(obj is Fraction objec)) return false;
            if (this == objec) return true;
            else return false;
        }
#nullable disable
        public override int GetHashCode()
        {
            return HashCode.Combine(numerator, denominator);
        }

        public override string ToString()
        {
            return numerator + "/" + denominator;
        }

    }

    public interface IRotatable
    {
        Fraction[] angle { get; set; }
        Fraction[] angle_velocity { get; }
    }
    public class RotateCommand : ICommand
    {
        IRotatable _object;
        public RotateCommand(IRotatable obj)
        {
            _object = obj;
        }

        public void Execute()
        {
            _object.angle[0] = _object.angle[0] + _object.angle_velocity[0];
            _object.angle[1] = _object.angle[1] + _object.angle_velocity[1];
            Fraction[] resultAngle = new Fraction[2] { _object.angle[0], _object.angle[1] };
            _object.angle = resultAngle;
        }
    }
    public class MoveCommand : ICommand
    {
        IMovable _obj;
        public MoveCommand(IMovable obj)
        {
            _obj = obj;
        }

        public void Execute()
        {
            if (_obj.Speed.coords.Count != _obj.Position.coords.Count)
            {
                throw new Exception();
            }
            Vector newpos = _obj.Position;
            for (int i = 0; i < _obj.Position.coords.Count; i++)
            {
                newpos[i] += _obj.Speed[i];
            }
            _obj.Position = newpos;
        }
    }
}
