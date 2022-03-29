using System;

namespace BlackWitchEngine
{    
    public interface IObject
    {
        Vector2D Pos { get; set; }
        void Update();
        event EventHandler ObjectUpdated;
    }
}
