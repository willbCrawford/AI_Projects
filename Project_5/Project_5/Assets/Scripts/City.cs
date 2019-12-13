using Assets.Scripts;
using System;
using System.Text;

namespace Project_3.Scripts
{
    public class City : IChromosome<City>
    {
        public int Node { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public City(int node, double x, double y)
        {
            Node = node;
            X = x;
            Y = y;
        }

        public double Distance(City other)
        {
            double delta_x = X - other.X;
            double delta_y = Y - other.Y;

            return Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2));
        }

        public double DistanceToLine(City city1, City city2)
        {
            var deltaXLine = city2.X - city1.X;
            var deltaYPoint = city1.Y - Y;
            var deltaXPoint = city1.X - X;
            var deltaYLine = city2.Y - city1.Y;
            var numerator = Math.Abs((deltaXLine * deltaYPoint) - (deltaXPoint * deltaYLine));
            var deltaXLineSQRD = Math.Pow(deltaXLine, 2);
            var deltaYLineSQRD = Math.Pow(deltaYLine, 2);
            var denominator = Math.Sqrt(deltaXLineSQRD + deltaYLineSQRD);

            return numerator / denominator;
        }

        public double CheckLine(City city1, City city2, out bool isCityInLineBoundary)
        {
            var m = (city2.Y - city1.Y) / (city2.X - city1.X);
            var n = -1 / m;

            double x;
            if (Distance(city1) < Distance(city2))
                x = ( (m * city1.X) - city1.Y - (n * X) + Y ) / (m - n);
            else
                x = ((m * city2.X) - city2.Y - (n * X) + Y) / (m - n);

            var y = n * (x - X) + Y;

            if ((x > city1.X && x < city2.X) || (y > city1.Y && y < city2.Y))
            {
                isCityInLineBoundary = true;
                return Distance(new City(0, x, y));
            }
            else
            {
                isCityInLineBoundary = false;
                if (Distance(city1) < Distance(city2))
                    return Distance(city1);
                else
                    return Distance(city2);
            }
        }

        public override double EvaluationFunction(IChromosome<City> item)
        {
            return Distance((City)item);
        }

        public override City GetT()
        {
            return this;
        }

        public override int GetNode()
        {
            return Node;
        }

        public override string ToString()
        {
            return Node.ToString();
        }
    }
}
