using System;
using BlackWitchEngine;
using NaturalSelectionLogic;

using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NaturalSelectionUI
{
    public partial class FrmMain : Form, IGameCycleView
    {
        private int _scale = 2;
        private (float X, float Y) _fieldSize;


        List<IObject> animals = new List<IObject>();

        public event EventHandler<InitializedCycleEventArgs> CycleInitialized = delegate { };
        public event EventHandler CycleLaunched = delegate { };

        public FrmMain()
        {
            InitializeComponent();
        }
        private void BtnTest_Click(object sender, EventArgs e)
        {
            PbxField.Paint += PaintField;
            CycleInitialized.Invoke(this, new InitializedCycleEventArgs() { FieldSize = (500, 400), SheepsNum = 50 });

            PbxField.Refresh();
        }
        private void PaintField(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(Color.White);
            Pen p = new Pen(Color.Black, 1.0f);
            (int X, int Y) shift = ((int)(e.ClipRectangle.Width - _fieldSize.X * _scale) / 2, (int)(e.ClipRectangle.Height - _fieldSize.Y * _scale) / 2);
            if (shift.X < 0)
                shift.X = 0;
            if (shift.Y < 0)
                shift.Y = 0;
            //Инициализация пустого белого поля
            g.FillRectangle(b, e.ClipRectangle);
            b.Color = Color.LightGreen;
            g.FillRectangle(b, shift.X, shift.Y, (int)_fieldSize.X * _scale, (int)_fieldSize.Y * _scale);
            //Отрисовка границ
            g.DrawRectangle(p, shift.X, shift.Y, (int)_fieldSize.X * _scale, (int)_fieldSize.Y * _scale);
            //Отрисовка зверей          
            foreach (var o in animals)
            {
                Animal a = (Animal)o;
                b.Color = Color.Red;
                g.FillEllipse(b, (a.Pos.X - a.CircleCollider.Radius) * _scale + shift.X, (a.Pos.Y - a.CircleCollider.Radius) * _scale + shift.Y,
                    2 * a.CircleCollider.Radius * _scale, 2 * a.CircleCollider.Radius * _scale);
                g.DrawEllipse(p, (a.Pos.X - a.CircleCollider.Radius) * _scale + shift.X, (a.Pos.Y - a.CircleCollider.Radius) * _scale + shift.Y,
                    2 * a.CircleCollider.Radius * _scale, 2 * a.CircleCollider.Radius * _scale);
            }
        }
        public new void Show()
        {
            Application.Run(this);
        }
        private async void BtnLaunchCycle_ClickAsync(object sender, EventArgs e)
        {
            //await Task.Run(()=>
            //{
            int i = 100;
            while (i > 0)
            {
                CycleLaunched.Invoke(this, new EventArgs());
                Thread.Sleep(100);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                i--;
            }
            //});
        }
        public void RenderObjects(List<IObject> objects, (float X, float Y) FieldSize)
        {
            animals = objects;
            _fieldSize = FieldSize;
            PbxField.Refresh();
        }
    }
}
