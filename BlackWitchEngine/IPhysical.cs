using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWitchEngine
{
    public interface IPhysical //Если объект физически расположен на карте
    {
        CircleCollider2D CircleCollider { get; set;}
    }
}
