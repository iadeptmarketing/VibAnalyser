namespace com.iAM.chart2dnet
{
    using System;

    public class Point3D : ChartObj
    {
        internal double x;
        internal double y;
        internal double z;

        public Point3D()
        {
            this.x = 0.0;
            this.y = 0.0;
            this.z = 0.0;
        }

        public Point3D(double px, double py, double pz)
        {
            this.x = 0.0;
            this.y = 0.0;
            this.z = 0.0;
            this.x = px;
            this.y = py;
            this.z = pz;
        }

        public override object Clone()
        {
            return new Point3D(this.x, this.y, this.z);
        }

        public virtual double Distance(Point3D pt)
        {
            double num = pt.GetX() - this.GetX();
            double num2 = pt.GetY() - this.GetY();
            double num3 = pt.GetZ() - this.GetZ();
            return Math.Sqrt(((num * num) + (num2 * num2)) + (num3 * num3));
        }

        public virtual double Distance(double PX, double PY, double PZ)
        {
            PX -= this.GetX();
            PY -= this.GetY();
            PZ -= this.GetZ();
            return Math.Sqrt(((PX * PX) + (PY * PY)) + (PZ * PZ));
        }

        public static double Distance(double X1, double Y1, double Z1, double X2, double Y2, double Z2)
        {
            X1 -= X2;
            Y1 -= Y2;
            Z1 -= Z2;
            return Math.Sqrt(((X1 * X1) + (Y1 * Y1)) + (Z1 * Z1));
        }

        public virtual double DistanceSq(Point3D pt)
        {
            double num = pt.GetX() - this.GetX();
            double num2 = pt.GetY() - this.GetY();
            double num3 = pt.GetZ() - this.GetZ();
            return (((num * num) + (num2 * num2)) + (num3 * num3));
        }

        public virtual double DistanceSq(double PX, double PY, double PZ)
        {
            PX -= this.GetX();
            PY -= this.GetY();
            PZ -= this.GetZ();
            return (((PX * PX) + (PY * PY)) + (PZ * PZ));
        }

        public static double DistanceSq(double X1, double Y1, double Z1, double X2, double Y2, double Z2)
        {
            X1 -= X2;
            Y1 -= Y2;
            Z1 -= Z2;
            return (((X1 * X1) + (Y1 * Y1)) + (Z1 * Z1));
        }

        public virtual double GetX()
        {
            return this.x;
        }

        public virtual double GetY()
        {
            return this.y;
        }

        public virtual double GetZ()
        {
            return this.z;
        }

        public virtual void SetLocation(Point3D p)
        {
            this.SetLocation(p.GetX(), p.GetY(), p.GetZ());
        }

        public virtual void SetLocation(double px, double py, double pz)
        {
            this.x = px;
            this.y = py;
            this.z = pz;
        }

        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public double Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }
    }
}

