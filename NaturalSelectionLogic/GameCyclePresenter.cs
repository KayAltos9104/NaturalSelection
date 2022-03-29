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
        private bool _cycleActive = false;
        public GameCyclePresenter(GameCycle model, IGameCycleView view)
        {
            _gameCycleModel = model;
            _gameCycleModel.CycleUpdated += ViewUpdateCycle;

            _gameCycleView = view;
            _gameCycleView.CycleInitialized += ModelInitializeCycle;
            _gameCycleView.CycleLaunched += ModelLaunchCycleAsync;
        }
        public void ViewUpdateCycle(object sender, GameCycleEventArgs e)
        {
            _gameCycleView.RenderObjects(e.Animals, e.FieldSize);
            _gameCycleView.ShowStatistics(e.AnimalsNumber);
        }
        public void ModelInitializeCycle(object sender, InitializedCycleEventArgs e)
        {
            _gameCycleModel.Initizalize(e.FieldSize, e.SheepsNum, e.WolfsNum);
        }
        public async void ModelLaunchCycleAsync(object sender, EventArgs e)
        {
            if (_gameCycleModel.FieldSize.X != 0)
            {
                SwitchCycleActive();
                await Task.Run(() =>
                {
                    while (_cycleActive)
                    {
                        _gameCycleModel.Update();
                        Thread.Sleep(100);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                });
            }
            else
                _gameCycleView.ShowError("Поле не сгенериновано!");
        }
        private void SwitchCycleActive()
        {
            if (_cycleActive)
                _cycleActive = false;
            else
                _cycleActive = true;
        }
        public void ViewRun()
        {
            _gameCycleView.Show();
        }
    }
}
