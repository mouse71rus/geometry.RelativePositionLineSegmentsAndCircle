using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L2_geo
{
    class Program
    {
        static void Main(string[] args)
        {
            double EPS = 0.000001;//Точность

            Point O, A, B;
            double x, y;
            Console.WriteLine("Координаты центра окружности: O(x, y)");
            Console.Write("x: ");
            x = Convert.ToDouble(Console.ReadLine());
            Console.Write("y: ");
            y = Convert.ToDouble(Console.ReadLine());

            O = new Point(x, y);
            Console.WriteLine();
            Console.Write("Радиус: ");
            double radius = Convert.ToDouble(Console.ReadLine());

            Circle circle = new Circle(O, radius);
            Console.WriteLine();


            Console.WriteLine("Координаты отрезка AB:");
            Console.WriteLine("A: (x, y)");
            Console.Write("x: ");
            x = Convert.ToDouble(Console.ReadLine());
            Console.Write("y: ");
            y = Convert.ToDouble(Console.ReadLine());
            A = new Point(x, y);
            Console.WriteLine("A: " + A.ToString());
            Console.WriteLine();

            Console.WriteLine("B: (x, y)");
            Console.Write("x: ");
            x = Convert.ToDouble(Console.ReadLine());
            Console.Write("y: ");
            y = Convert.ToDouble(Console.ReadLine());
            B = new Point(x, y);
            Console.WriteLine("B: " + B.ToString());
            Console.WriteLine();

            double AB = A.getDist(B);
            double AO = A.getDist(O);
            double BO = B.getDist(O);

            double p = 0.5 * (AB + AO + BO);
            double h = (2 * (Math.Sqrt(p * (p - AB) * (p - AO) * (p - BO)))) / AB;//длина опущенного перпендикуляра
            Console.WriteLine(h);

            if (((AO - circle.radius) < EPS) || ((BO - circle.radius) < EPS))//(AO == circle.radius && BO == circle.radius)// //
            {
                Console.WriteLine("Крайние точки отрезка лежат на окружности");
            }
            else if (AO > circle.radius && BO > circle.radius)//Если обе крайние точки отрезка лежат вне окружности
            {
                Vector vAO = new Vector(A, O);//P1M
                Vector vAB = new Vector(A, B);//P1P2

                double r1 = vAO.ScalarMultiply(vAB);

                Vector vBO = new Vector(B, O);//P2M
                Vector vBA = new Vector(B, A);//P2P1
                double r2 = vBO.ScalarMultiply(vBA);

                if (r1 < 0 || r2 < 0 || (h - circle.radius) > EPS)//Если одно из скалярынх произведений меньше нуля, то опущенный перпендикуляр не попадает на отрезок или если расстояния от отрезка до окружности больше радиуса
                {
                    //Из этого следует, что отрезок вне окружности
                    Console.WriteLine("Отрезок находится вне окружности");
                }
                else if(Math.Abs(h - circle.radius) < EPS)//if(h == circle.radius)
                {
                    Console.WriteLine("Отрезок касается окружности");
                }
                else
                {
                    Console.WriteLine("Отрезок пересекает окружность в двух точках");
                }
            }
            else if(AO > circle.radius || BO > circle.radius)//Если хотя бы одна из точек отрезка лежит вне окружности
            {
                if (((AO - circle.radius) < EPS) || ((BO - circle.radius) < EPS)) //(AO == circle.radius || BO == circle.radius)
                {
                    Console.WriteLine("Одна из крайних точек отрезка лежит на окружности, вторая находится снаружи");
                }
                else
                {
                    Console.WriteLine("Одна из крайних точек отрезка лежит внутри окружности, вторая находится снаружи");
                }
            }
            else if(AO < circle.radius && BO < circle.radius)//если крайние точки лежат внутри окружности
            {
                Console.WriteLine("Отрезок лежит внутри окружности");
            }
            else //if (AO == circle.radius || BO == circle.radius)
            {
                Console.WriteLine("Одна из крайних точек отрезка лежит на окружности, вторая находится внутри");
            }

            Console.ReadKey();
        }
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point()
        {
            X = 0.0f;
            Y = 0.0f;
        }
        public Point(double _x, double _y)
        {
            X = _x;
            Y = _y;
        }

        public double getDist(Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - this.X, 2) + Math.Pow(p2.Y - this.Y, 2));
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", this.X, this.Y);
        }
    }
    public class Circle
    {
        public Circle(Point pointCenter, double radius)
        {
            this.PointCenter = pointCenter;
            this.radius = radius;
        }

        public Point PointCenter { get; private set; }
        public double radius { get; set; }


    }

    public class Vector
    {
        public double p;
        public double q;
        public double length
        {
            get
            {
                return Math.Sqrt(Math.Pow(this.p, 2) + Math.Pow(this.q, 2));
            }
        }
        public Vector(Point p1, Point p2)
        {
            this.p = p2.X - p1.X;
            this.q = p2.Y - p1.Y;
        }

        //Скалярное произведение векторов
        public double ScalarMultiply(Vector v2)
        {
            return this.p * v2.p + this.q * v2.q;
        }
    }
}
