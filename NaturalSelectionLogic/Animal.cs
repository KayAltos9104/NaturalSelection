﻿using System;
using BlackWitchEngine;

namespace NaturalSelectionLogic
{
    public class Animal : MaterialObject
    {        
        public float Speed { get; protected set; }         
        public float Health { get; protected set; }
        public float Attack { get; protected set; }
        //public CircleCollider2D CircleCollider { get; set; }

        //public float Armor { get; protected set; }
        //public float ChildrenCount { get; protected set; }
        //public byte CurrentAge { get; protected set; }
        //public byte LiveAge { get; protected set; }
        //public float Hunger { get; protected set; }
        public Animal (Vector2D pos):this(10,100,0,5,pos)
        {           
          
        }
        public Animal (float s, float h, float a, float size, Vector2D pos)
        {
            Speed = s;
            Health = h;
            Attack = a;            
            Pos = pos;
            CircleCollider = new CircleCollider2D(Pos, size);
        }
        public override void Update()
        { 
            base.Update();//Включает событие апдейта
        }
        public void Move (Vector2D dir)//Нужен нормализованный вектор на вход
        {            
            //for (float m = Speed; m >0;m--)
            //{
                Pos += dir;
            //}
            CircleCollider.Center = Pos;
        }
    }
}
