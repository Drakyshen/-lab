using System;

namespace Lab17_v1
{
    public abstract class Figure
    {
        public string Name { get; set; }

        public Figure(string name)
        {
            Name = name;
        }

        public abstract double GetArea();      
        public abstract double GetPerimeter(); 
    }
    public class Rectangle : Figure
    {
        private double width;
        private double height;

        public Rectangle(double w, double h) : base("Прямокутник")
        {
            width = w > 0 ? w : 1; 
            height = h > 0 ? h : 1;
        }

        public override double GetArea() => width * height;
        public override double GetPerimeter() => 2 * (width + height);
    }
    public class Circle : Figure
    {
        private double radius;

        public Circle(double r) : base("Коло")
        {
            radius = r > 0 ? r : 1;
        }

        public override double GetArea() => Math.PI * radius * radius;
        public override double GetPerimeter() => 2 * Math.PI * radius;
    }
    public class Trapezium : Figure
    {
        private double baseA, baseB, sideC;

        public Trapezium(double a, double b, double c) : base("Трапеція")
        {
            baseA = a > 0 ? a : 1;
            baseB = b > 0 ? b : 1;
            sideC = c > 0 ? c : 1;
        }

        public override double GetArea()
        {
            double x = Math.Abs(baseA - baseB) / 2;
            double h = Math.Sqrt(Math.Max(0, sideC * sideC - x * x));
            return ((baseA + baseB) / 2) * h;
        }

        public override double GetPerimeter() => baseA + baseB + 2 * sideC;
    }
}
