using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWitchEngine
{
    public class CircleCollider2D
    {
        public Vector2D Center { get; set; }
        public float Radius {get;}
        public CircleCollider2D(Vector2D center, float r)
        {
            Center = center;
            Radius = r;
        }
        public static bool IsIntersected(CircleCollider2D c1, CircleCollider2D c2)
        {
            return Vector2D.CalculateDistance(c1.Center, c2.Center) < c1.Radius + c2.Radius;
        }        
    }
}
