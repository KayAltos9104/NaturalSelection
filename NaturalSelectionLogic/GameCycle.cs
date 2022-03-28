using BlackWitchEngine;
using System;
using System.Collections.Generic;


namespace NaturalSelectionLogic
{
    public class GameCycleEventArgs    {
        
        public List<IObject> Animals { get; set; }
        public (float X, float Y) FieldSize { get; set;}
        public GameCycleEventArgs (List<IObject> animals, (float , float) field)
        {           
            this.Animals = animals;
            this.FieldSize = field;
        }
    }
    public class GameCycle
    {
        private readonly Random rnd = new Random();
        public List<IObject> animals = new List<IObject>();
        (float X, float Y) FieldSize { get; set; }               
        public event EventHandler<GameCycleEventArgs> CycleUpdated = delegate { };
        public void Initizalize ((float X, float Y) FieldSize, int SheepsNum)
        {
            this.FieldSize = FieldSize;
            for (int i = 1; i <=SheepsNum;i++)
            {
                float x = (float)(rnd.NextDouble() * (FieldSize.X-20)+20);
                float y = (float)(rnd.NextDouble() * (FieldSize.Y)-20+20);
                while (x < 0 || y < 0 || x > FieldSize.X || y > FieldSize.Y)
                {
                    x = (float)(rnd.NextDouble() * FieldSize.X);
                    y = (float)(rnd.NextDouble() * FieldSize.Y);
                }
                Animal a = new Animal(new Vector2D(x, y));
                animals.Add(a);
            }
        }     
        public void Update()
        {
            foreach (var o in animals)
            {
                Vector2D dir = new Vector2D((float)(2 * rnd.NextDouble() - 1), (float)(2 * rnd.NextDouble() - 1));
                var a = (Animal)o;
                //while ((a.Pos.X+a.Speed * dir.X) < 0 || (a.Pos.Y+a.Speed * dir.Y) < 0 || (a.Pos.X + a.Speed * dir.X) > FieldSize.X 
                //    || (a.Pos.Y + a.Speed * dir.Y) > FieldSize.Y)
                //{
                //    dir = new Vector2D((float)(2 * rnd.NextDouble() - 1), (float)(2 * rnd.NextDouble() - 1));
                //}
                while ((a.Pos.X + a.Speed * dir.X) < 20 ||(a.Pos.X + a.Speed * dir.X) > FieldSize.X-20)
                {
                    dir = new Vector2D(-dir.X * 2, dir.Y);
                }
                while ((a.Pos.Y + a.Speed * dir.Y) < 20 || (a.Pos.Y + a.Speed * dir.Y) > FieldSize.Y-20)
                {
                    dir = new Vector2D(dir.X, -dir.Y*2);
                }
                a.Move(dir);
                foreach (var neighbor in animals)
                {
                    if (o == neighbor)
                        continue;
                    var n = (Animal)neighbor;
                    if (Vector2D.CalculateDistance(a.Pos,n.Pos)<2*a.CircleCollider.Radius)
                    {
                        var bounce = Vector2D.Reverse(dir);
                        while (CircleCollider2D.IsIntersected(a.CircleCollider, n.CircleCollider))
                        {
                            //a.Move(Vector2D.MultipleModule(1, bounce));
                            a.Move(bounce);
                        }
                    }
                }                
                a.Update();
            }
            CycleUpdated(this, new GameCycleEventArgs(animals, FieldSize));
        }
    }
}
