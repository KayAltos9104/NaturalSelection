using System;
using BlackWitchEngine;

namespace NaturalSelectionLogic
{
    public class AnimalBornedArgs
    {
        public Animal Child { get; set; }
    }
    public abstract class Animal : MaterialObject
    {
        public Random rnd = new Random();
        public float Speed { get; protected set; }
        public float Health { get; protected set; }
        public float Attack { get; protected set; }
        public bool IsLive { get; protected set; }
        

        //public float Armor { get; protected set; }
        //public float ChildrenCount { get; protected set; }
        public byte CurrentAge { get; protected set; }
        public byte LiveAge { get; protected set; }
        //public float Hunger { get; protected set; }
        public event EventHandler Died = delegate { };
        public abstract event EventHandler<AnimalBornedArgs> GaveBirth;
        public byte BirthCooldown { get; set; }
        public Animal(Vector2D pos) : this(10, 100, 0, 7, pos)
        {

        }
        public Animal(float s, float h, float a, float size, Vector2D pos)
        {
            Speed = s;
            Health = h;
            Attack = a;
            Pos = pos;
            LiveAge = 200;
            CurrentAge = 1;
            CircleCollider = new CircleCollider2D(Pos, size);
            IsLive = true;
        }
        public override void Update()
        {            
            if (CurrentAge >= LiveAge)
            {
                Died.Invoke(this, new EventArgs());
                IsLive = false;
            }
            if (BirthCooldown > 0)
                BirthCooldown--;
            CurrentAge++;
            base.Update();//Включает событие апдейта
        }
        public void Move(Vector2D dir)//Нужен нормализованный вектор на вход
        {
            Pos += dir;
            CircleCollider.Center = Pos;
        }
        public abstract void Born();
    }
}
