using System;

namespace MoreCollection.Infra 
{
    internal class SecondDegreeSolver 
    {
        public int A { get; private set; }
        public int B { get; private set; }
        public int C { get; private set; }

        public int Discriminant
        {
            get { return B * B - 4 * A * C; }
        }

        public SecondDegreeSolver(int a, int b, int c) 
        {
            A = a;
            B = b;
            C = c;
        }

        public double? GetMaxSolution()
        {
            if (Discriminant < 0)
                return null;

            return (-B + Math.Sqrt(Discriminant)) / (2 * A);
        }
    }
}
