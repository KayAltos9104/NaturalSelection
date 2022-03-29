using BlackWitchEngine;
using System;


namespace NaturalSelectionLogic
{
    public class Wolf: Animal
    {
        public override event EventHandler<AnimalBornedArgs> GaveBirth = delegate { };
        public Wolf(Vector2D pos) : base(pos)
        {

        }
        public Wolf(float s, float h, float a, float size, Vector2D pos) : base(s, h, a, size, pos)
        {

        }
        public override void Update()
        {
            base.Update();
        }
        public override void Born()
        {
            if (BirthCooldown == 0)
            {
                Wolf s = new Wolf(new Vector2D(0, 0));
                GaveBirth.Invoke(this, new AnimalBornedArgs() { Child = s });
            }
        }
    }
}
