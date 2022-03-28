using System;
using BlackWitchEngine;
using NaturalSelectionLogic;

using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;

namespace NaturalSelectionUI
{
    public partial class FrmMain : Form, IGameCycleView
    {
        private int _scale = 2;
        private (float X, float Y) _fieldSize;


        List<IObject> animals=new List<IObject>();

        public event EventHandler<InitializedCycleEventArgs> CycleInitialized = delegate { };
        public event EventHandler CycleLaunched = delegate { };

        public FrmMain()
        {
            InitializeComponent();
        }
        private void BtnTest_Click(object sender, EventArgs e)
        {
            PbxField.Paint += PaintField;            
            CycleInitialized.Invoke(this, new InitializedCycleEventArgs() { FieldSize = (600, 450), SheepsNum = 50 });

            PbxField.Refresh();
        }
        private void PaintField(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(Color.White);
            Pen p = new Pen(Color.Black, 1.0f);
            //Инициализация пустого белого поля
            g.FillRectangle(b, e.ClipRectangle);
            //Отрисовка границ
            g.DrawLine(p, new Point(_scale, _scale), new Point((int)_fieldSize.X * _scale, _scale));
            g.DrawLine(p, new Point(_scale, _scale), new Point(_scale, (int)_fieldSize.Y * _scale));
            g.DrawLine(p, new Point((int)_fieldSize.X * _scale, (int)_fieldSize.Y * _scale), new Point((int)_fieldSize.X * _scale, _scale));
            g.DrawLine(p, new Point((int)_fieldSize.X * _scale, (int)_fieldSize.Y * _scale), new Point(_scale, (int)_fieldSize.Y * _scale));
            //Отрисовка зверей          
            foreach (var o in animals)
            {
                Animal a = (Animal)o;
                b.Color = Color.Red;
                g.FillEllipse(b, (a.Pos.X - a.CircleCollider.Radius) * _scale, (a.Pos.Y - a.CircleCollider.Radius) * _scale,
                    2*a.CircleCollider.Radius * _scale, 2*a.CircleCollider.Radius * _scale);
                g.DrawEllipse(p, (a.Pos.X - a.CircleCollider.Radius) * _scale, (a.Pos.Y - a.CircleCollider.Radius) * _scale,
                    2*a.CircleCollider.Radius * _scale,2*a.CircleCollider.Radius * _scale);
            }
        }
        public new void Show()
        {
            Application.Run(this);
        }
        private void BtnLaunchCycle_Click(object sender, EventArgs e)
        {
            while (true)
            {
                CycleLaunched.Invoke(this, new EventArgs());
                Thread.Sleep(100);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
                       
        }
        public void RenderObjects(List<IObject> objects, (float X, float Y) FieldSize)
        {
            animals = objects;
            _fieldSize = FieldSize;
            PbxField.Refresh();
        }
    }   
}
