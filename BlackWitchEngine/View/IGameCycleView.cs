using System;
using System.Collections.Generic;


namespace BlackWitchEngine
{
    public class InitializedCycleEventArgs
    {
        public (float X, float Y) FieldSize { get; set;}
        public int SheepsNum { get; set;}
        public int WolfsNum { get; set; }
    }
    public interface IGameCycleView
    {        
        event EventHandler<InitializedCycleEventArgs> CycleInitialized;
        event EventHandler CycleLaunched;
        public void Show();
        void ShowError(string errorMessage);        
        
        void RenderObjects(List<IObject> objects, (float X, float Y) FieldSize);
        void ShowStatistics(Dictionary<string, int> AnimalsNumber);
    }
}
