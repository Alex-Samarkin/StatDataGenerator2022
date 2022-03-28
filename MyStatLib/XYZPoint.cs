// SimpleStat
// MyStatLib
// XYZPoint.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 2:10 28 03 2022

using System;

namespace MyStatLib
{
    public class XYZPoint
    {
        public double[] Data { get; set; } = new[] { 0.0, 0.0, 0.0 };
        public double X
        {
            get => Data[0];
            set => Data[0] = value;
        }
        public double Y
        {
            get => Data[1];
            set => Data[1] = value;
        }
        public double Z
        {
            get => Data[2];
            set => Data[2] = value;
        }

        XYZPoint Move(XYZPoint vector)
        {
            X += vector.X;
            Y += vector.Y;
            Z += vector.Z;
            return this;
        }

        XYZPoint RotateXY(double alpha)
        {
            RAToXY(RXY,AngleXY);
            return this;
        }
        XYZPoint RotateYZ(double alpha)
        {
            RAToXY(RYZ, AngleYZ);
            return this;
        }
        XYZPoint RotateZX(double alpha)
        {
            RAToXY(RZX, AngleZX);
            return this;
        }

        public double R2 => X * X + Y * Y + Z * Z;
        public double R => Math.Sqrt(R2);
        public double RXY => Math.Sqrt(X * X + Y * Y);
        public double RZX => Math.Sqrt(X * X + Z * Z);
        public double RYZ => Math.Sqrt(Y * Y + Z * Z);

        public void RAToXY(double R, double a)
        {
            X = R * Math.Cos(a);
            Y = R * Math.Sin(a);
        }
        public void RAToZX(double R, double a)
        {
            Z = R * Math.Cos(a);
            X = R * Math.Sin(a);
        }
        public void RAToYZ(double R, double a)
        {
            Y = R * Math.Cos(a);
            Z = R * Math.Sin(a);
        }

        public double AngleXY => Math.Atan2(Y, X);
        public double AngleZX => Math.Atan2(X, Z);
        public double AngleYZ => Math.Atan2(Z, Y);

    }
}