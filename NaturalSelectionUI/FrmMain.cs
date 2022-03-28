using System;
using BlackWitchEngine;
using NaturalSelectionLogic;

using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace NaturalSelectionUI
{
    public partial class FrmMain : Form, IGameCycleView
    {
        int scale = 4;       

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
            CycleInitialized.Invoke(this, new InitializedCycleEventArgs() { FieldSize = (100, 100), SheepsNum = 5 });

            PbxField.Refresh();
        }

        private void PaintField(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(Color.White);
            Pen p = new Pen(Color.Black, 1.0f);

            //Инициализация пустого белого поля
            g.FillRectangle(b, e.ClipRectangle);
            //Отрисовка зверей
          
            foreach (var o in animals)
            {
                Animal a = (Animal)o;
                b.Color = Color.Red;
                g.FillEllipse(b, (a.Pos.X - a.CircleCollider.Radius) * scale, (a.Pos.Y - a.CircleCollider.Radius) * scale,
                    a.CircleCollider.Radius * scale, a.CircleCollider.Radius * scale);
                g.DrawEllipse(p, (a.Pos.X - a.CircleCollider.Radius) * scale, (a.Pos.Y - a.CircleCollider.Radius) * scale,
                    a.CircleCollider.Radius * scale, a.CircleCollider.Radius * scale);
            }

        }
        public new void Show()
        {
            Application.Run(this);
        }
        private void BtnLaunchCycle_Click(object sender, EventArgs e)
        {           
            CycleLaunched.Invoke(this, new EventArgs());            
        }

        public void RenderObjects(List<IObject> objects)
        {
            animals = objects;            
            PbxField.Refresh();
        }
    }   
}
