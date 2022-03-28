using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWitchEngine
{
    public class InitializedCycleEventArgs
    {
        public (float X, float Y) FieldSize { get; set;}
        public int SheepsNum { get; set;}         
    }
    public interface IGameCycleView
    {        
        event EventHandler<InitializedCycleEventArgs> CycleInitialized;
        event EventHandler CycleLaunched;
        public void Show();
        public void RenderObjects(List<IObject> objects, (float X, float Y) FieldSize);
    }
}
