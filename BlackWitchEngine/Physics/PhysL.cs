using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWitchEngine.PhysL
{
    public static class PhysL
    {       
        //Столкновение как об стенку - второй объект не двигается
        public static bool ObsctacleCollide(Vector2D dir, MaterialObject o1, MaterialObject o2)
        {
            bool collided = false;
            if (Vector2D.CalculateDistance(o1.CircleCollider.Center, o2.CircleCollider.Center) < 2 * o1.CircleCollider.Radius)
            {
                var bounce = Vector2D.Reverse(dir);
                int tries = 100;
                collided = CircleCollider2D.IsIntersected(o1.CircleCollider, o2.CircleCollider);
                while (CircleCollider2D.IsIntersected(o1.CircleCollider, o2.CircleCollider) && tries > 0)
                {
                    o1.Pos += bounce;
                    o1.CircleCollider.Center = o1.Pos;
                    tries--;
                }                
            }
            return collided;
        }
    }
}
