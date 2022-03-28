using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWitchEngine
{
    public class ObjectEventArgs:EventArgs
    {

    }
    public interface IObject
    {
        void Update();
        event EventHandler ObjectUpdated;
    }
}
