using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWitchEngine
{    
    public interface IObject
    {
        Vector2D Pos { get; set; }
        void Update();
        event EventHandler ObjectUpdated;
    }
}
