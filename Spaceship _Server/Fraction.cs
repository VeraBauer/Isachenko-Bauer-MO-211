using System;

namespace Spaceship__Server
{
    public class Fraction
    {
        int numerator
        {
            get;
            set;
        }
        int denominator
        {
            get;
            set;
        }
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
}
