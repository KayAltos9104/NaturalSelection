using System;

namespace BlackWitchEngine
{
    public abstract class MaterialObject:IObject, IPhysical
    {
        public virtual Vector2D Pos { get; set; }
        public virtual CircleCollider2D CircleCollider { get ; set;}        
        public event EventHandler ObjectUpdated = delegate { };
        public virtual void Update()
        {
            ObjectUpdated(this, new EventArgs ());
        }
    }
}
