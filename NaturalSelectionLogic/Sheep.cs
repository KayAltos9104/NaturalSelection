using BlackWitchEngine;
using System;

namespace NaturalSelectionLogic
{
    public class Sheep : Animal
    {
        public override event EventHandler<AnimalBornedArgs> GaveBirth = delegate { };        
        public Sheep (Vector2D pos):base(pos)
        {

        }
        public Sheep (float s, float h, float a, float size, Vector2D pos):base(s,h,a,size,pos)
        {

        }
        public override void Update()
        {            
            base.Update();
        }
        public override void Born()
        {      
            if(BirthCooldown==0)
            {
                Sheep s = new Sheep(new Vector2D(0, 0));                
                GaveBirth.Invoke(this, new AnimalBornedArgs() { Child = s });
            }            
        }
    }
}
