using BlackWitchEngine;
using System;
using System.Collections.Generic;


namespace NaturalSelectionLogic
{
    public class GameCycleEventArgs
    {
        object sender;
        public List<IObject> animals { get; set; }
        public GameCycleEventArgs (object sender, List<IObject> animals)
        {
            this.sender = sender;
            this.animals = animals;
        }
    }
    public class GameCycle
    {
        Random rnd = new Random();
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
                a.Move(dir);
                a.Update();
            }
            CycleUpdated(this, new GameCycleEventArgs(this,animals));
        }
    }
}
