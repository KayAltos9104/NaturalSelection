using BlackWitchEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _gameCycleView.CycleLaunched += ModelLaunchCycle;
        }
        public void ViewUpdateCycle (object sender, GameCycleEventArgs e)
        {
            _gameCycleView.RenderObjects(e.Animals, e.FieldSize);
        }
        public void ModelInitializeCycle(object sender, InitializedCycleEventArgs e)
        {
            _gameCycleModel.Initizalize(e.FieldSize, e.SheepsNum);
        }
        public void ModelLaunchCycle(object sender, EventArgs e)
        {
            _gameCycleModel.Update();
        }
        public void ViewRun()
        {
            _gameCycleView.Show();
        }
    }
}
