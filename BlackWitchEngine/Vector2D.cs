using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWitchEngine
{
    public class Vector2D
    {
        public float X { get; set;}
        public float Y { get; set; }
        public Vector2D (float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public float GetModule ()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }
        public Vector2D NormalizeVector()
        {
            float m = GetModule();
            return new Vector2D(this.X / m, this.Y / m);
        }
        public static Vector2D MultipleModule(float m, Vector2D v)
        {
            return new Vector2D(v.X * m, v.Y * m);
        }
        public static Vector2D Reverse (Vector2D v)
        {
            return new Vector2D(-v.X, -v.Y);
        }
        public static float ScalarMultiple (Vector2D v1, Vector2D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }
        public static float CalculateDistance (Vector2D v1, Vector2D v2)
        {
            return (float)Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
        }
        public static Vector2D operator + (Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }
    }
}
