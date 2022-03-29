using BlackWitchEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NaturalSelectionLogic
{
    public class GameCyclePresenter
    {
        private GameCycle _gameCycleModel = null;
        private IGameCycleView _gameCycleView = null;
        public GameCyclePresenter(GameCycle model, IGameCycleView view)
        {
            _gameCycleModel = model;
            _gameCycleModel.CycleUpdated += ViewUpdateCycle;

            _gameCycleView = view;
            _gameCycleView.CycleInitialized += ModelInitializeCycle;
            _gameCycleView.CycleLaunched += ModelLaunchCycleAsync;
        }
        public void ViewUpdateCycle (object sender, GameCycleEventArgs e)
        {
            //_gameCycleView.RenderObjects(e.Animals, e.FieldSize);
            _gameCycleView.RenderObjects(e.Animals, e.FieldSize);
        }
        public void ModelInitializeCycle(object sender, InitializedCycleEventArgs e)
        {
            _gameCycleModel.Initizalize(e.FieldSize, e.SheepsNum);
        }
        public async void ModelLaunchCycleAsync(object sender, EventArgs e)
        {
            if (_gameCycleModel.FieldSize.X != 0)
            {
                int i = 100;
                await Task.Run(()=>
                {
                    while (i > 0)
                    {
                        _gameCycleModel.Update();
                        Thread.Sleep(100);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        i--;
                    }
                });                
            }                
            else
                _gameCycleView.ShowError("Поле не сгенериновано!");
        }
        public void ViewRun()
        {
            _gameCycleView.Show();
        }
    }
}
