using System;

namespace BlackWitchEngine
{
    public abstract class TemplateObject:IObject
    {
        public virtual Vector2D Pos { get; set; }
        //public event EventHandler<ObjectEventArgs> ObjectUpdated;
        public event EventHandler ObjectUpdated = delegate { };
        public virtual void Update()
        {
            //ObjectUpdated(this, new ObjectEventArgs());
            ObjectUpdated(this, new EventArgs ());
        }
    }
}
