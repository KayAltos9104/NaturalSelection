using BlackWitchEngine;
using BlackWitchEngine.PhysL;
using System;
using System.Collections.Generic;


namespace NaturalSelectionLogic
{
    public class GameCycleEventArgs
    {
        public List<IObject> Animals { get; set; }
        public (float X, float Y) FieldSize { get; set; }
        public Dictionary<string, int> AnimalsNumber { get; set; }
        public GameCycleEventArgs(List<IObject> animals, (float, float) field, Dictionary<string, int> count)
        {
            this.Animals = animals;
            this.FieldSize = field;
            this.AnimalsNumber = count;
        }
    }
    public class GameCycle
    {        
        private Random rnd = new Random();
        public List<IObject> animals = new List<IObject>();
        public List<IObject> animalsToDel = new List<IObject>();
        public List<IObject> animalsToBorn = new List<IObject>();
        public (float X, float Y) FieldSize {get; set;}
        public event EventHandler<GameCycleEventArgs> CycleUpdated = delegate { };
        public void Initizalize((float X, float Y) FieldSize, int SheepsNum, int WolfsNum)
        {
            animals.Clear();
            animalsToDel.Clear();
            animalsToBorn.Clear();
            this.FieldSize = FieldSize;
            for (int i = 1; i <= SheepsNum; i++)
            {
                AnimalFabric("овца");                                
            }
            for (int i = 1; i <= WolfsNum; i++)
            {
                AnimalFabric("волк");               
            }
        }
        public void Update()
        {
            Dictionary<string, int> animalsNumber = new Dictionary<string, int>();
            animalsToDel.Clear();
            animalsToBorn.Clear();            
            foreach (var animal in animals)
            {                 
                Vector2D dir = new Vector2D((float)(2 * rnd.NextDouble() - 1), (float)(2 * rnd.NextDouble() - 1));
                var a = (Animal)animal;               

                if ((a.Pos.X + a.Speed * dir.X) < a.CircleCollider.Radius ||
                    (a.Pos.X + a.Speed * dir.X) > FieldSize.X - a.CircleCollider.Radius)
                {
                    dir = new Vector2D(-dir.X * 2, dir.Y);
                }
                if ((a.Pos.Y + a.Speed * dir.Y) < a.CircleCollider.Radius ||
                    (a.Pos.Y + a.Speed * dir.Y) > FieldSize.Y - a.CircleCollider.Radius)
                {
                    dir = new Vector2D(dir.X, -dir.Y * 2);
                }
                if (a.Pos.X < 0 || a.Pos.Y < 0 || a.Pos.X > FieldSize.X || a.Pos.Y > FieldSize.Y)
                {
                    animalsToDel.Add(a);
                    continue;
                }
                for (float m = a.Speed; m >= 0; m--)
                {                   
                    a.Move(dir);
                    foreach (var neighbor in animals)
                    {
                        if (animal == neighbor)
                            continue;
                        var n = (Animal)neighbor;
                        if (PhysL.ObsctacleCollide(dir, a, n))
                        {                           
                            if (a.GetType() == n.GetType())
                            {
                                if (a is Sheep&& a.CurrentAge > 10 && n.CurrentAge > 10)
                                {
                                    for (int i = 0; i < 2; i++)
                                    {
                                        a.Born();
                                    }
                                    a.BirthCooldown = 5;
                                    n.BirthCooldown = 5;
                                }
                                else if (a is Wolf&& a.CurrentAge > 10 && n.CurrentAge > 10)
                                {
                                    for (int i = 0; i < 2; i++)
                                    {
                                        a.Born();
                                    }
                                    //a.BirthCooldown = 10;
                                    //n.BirthCooldown = 10;
                                }                                
                            }
                            else if (a is Wolf && a.GetType()!=n.GetType())
                            {
                                animalsToDel.Add(n);
                                a.Hunger -= n.Health;
                            }
                        }                       
                    }
                }
                a.Update();
            }
            foreach (var da in animalsToDel)
            {
                animals.Remove(da);
            }
            foreach (var ba in animalsToBorn)
            {
                animals.Add(ba);
            }
            foreach (var a in animals)
            {
                if (a is Sheep)
                {
                    if (animalsNumber.ContainsKey("Овцы"))
                        animalsNumber["Овцы"] += 1;
                    else
                        animalsNumber.Add("Овцы", 1);
                }
                else if (a is Wolf)
                {
                    if (animalsNumber.ContainsKey("Волки"))
                        animalsNumber["Волки"] += 1;
                    else
                        animalsNumber.Add("Волки", 1);

                }
            }
            CycleUpdated(this, new GameCycleEventArgs(animals, FieldSize, animalsNumber));
        }
        private void BirthHandler(object sender, AnimalBornedArgs e)
        {
            Animal a = (Animal)sender;
            e.Child.Pos.X = GenRandom(a.Pos.X - a.CircleCollider.Radius, a.Pos.X + a.CircleCollider.Radius);
            e.Child.Pos.Y = GenRandom(a.Pos.Y - a.CircleCollider.Radius, a.Pos.Y + a.CircleCollider.Radius);
            e.Child.Died += (sender, e) => DeathHandler(sender, e);
            e.Child.GaveBirth += (sender, e) => BirthHandler(sender, e);
            animalsToBorn.Add(e.Child);
        }
        private void DeathHandler(object sender, EventArgs e)
        {
            animalsToDel.Add((IObject)sender);            
        }
        private float GenRandom(float left, float right)
        {
            return (float)(rnd.NextDouble() * (right - left)+left);
        }

        private void AnimalFabric(string type)
        {
            Animal a = null;
            if (type.ToLower() == "овца")
            {
                a = new Sheep(new Vector2D(0, 0));
            }
            else if (type.ToLower() == "волк")
            {
                a = new Wolf(new Vector2D(0, 0));
            }
            else
            {
                return;
            }
            float x;
            float y;
            do
            {
                x = GenRandom(a.CircleCollider.Radius, FieldSize.X - a.CircleCollider.Radius);
                y = GenRandom(a.CircleCollider.Radius, FieldSize.Y - a.CircleCollider.Radius);
            } while (x < 0 || y < 0 || x > FieldSize.X || y > FieldSize.Y);
            a.Pos = new Vector2D(x, y);
            animals.Add(a);
            a.GaveBirth += (sender, e) => BirthHandler(sender, e);
            a.Died += (sender, e) => DeathHandler(sender, e);
            //return a;

        }
    }
}
