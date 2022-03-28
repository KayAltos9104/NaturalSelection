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
                float x = (float)(rnd.NextDouble() * FieldSize.X);
                float y = (float)(rnd.NextDouble() * FieldSize.Y);
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
                while ((a.Pos.X+a.Speed * dir.X) < 0 || (a.Pos.Y+a.Speed * dir.Y) < 0 || (a.Pos.X + a.Speed * dir.X) > FieldSize.X 
                    || (a.Pos.Y + a.Speed * dir.Y) > FieldSize.Y)
                {
                    dir = new Vector2D((float)(2 * rnd.NextDouble() - 1), (float)(2 * rnd.NextDouble() - 1));
                }
                a.Move(dir);
                a.Update();
            }
            CycleUpdated(this, new GameCycleEventArgs(animals, FieldSize));
        }
    }
}
