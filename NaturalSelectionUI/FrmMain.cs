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
        private int _scale = 3;
        private (float X, float Y) _fieldSize;
        Image lamb = Image.FromFile("lamb3.png");

        List<IObject> animals = new List<IObject>();

        public event EventHandler<InitializedCycleEventArgs> CycleInitialized = delegate { };
        public event EventHandler CycleLaunched = delegate { };

        //public delegate void Render(List<IObject> objects, (float X, float Y) FieldSize);
        public delegate void UpdateVisualization(List<IObject> objects, (float X, float Y) FieldSize);
        public UpdateVisualization UpdatePictureBox;
        public delegate void UpdateText(Dictionary<string, int> statistics);
        public UpdateText UpdateStatistics;

        public FrmMain()
        {
            InitializeComponent();           
            UpdatePictureBox = (objects, FieldSize) =>
            {
                animals = objects;
                _fieldSize = FieldSize;
                PbxField.Refresh();
            };
            UpdateStatistics = (animalsNumber) =>
            {
                LbxStatistics.Items.Clear();
                string s = "Кол-во животных: \n";
                LbxStatistics.Items.Add(s);
                foreach (var a in animalsNumber)
                {
                    s = String.Format("{0} : {1}", a.Key, a.Value);
                    LbxStatistics.Items.Add(s);
                }               
                LbxStatistics.Refresh();
            };
        }
        private void BtnTest_Click(object sender, EventArgs e)
        {
            PbxField.Paint += PaintField;
            CycleInitialized.Invoke(this, new InitializedCycleEventArgs() { FieldSize = (500, 300), SheepsNum = 20 });
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
                b.Color = Color.Blue;
                g.FillEllipse(b, (a.Pos.X - a.CircleCollider.Radius) * _scale + shift.X, (a.Pos.Y - a.CircleCollider.Radius) * _scale + shift.Y,
                    2 * a.CircleCollider.Radius * _scale, 2 * a.CircleCollider.Radius * _scale);
                g.DrawEllipse(p, (a.Pos.X - a.CircleCollider.Radius) * _scale + shift.X, (a.Pos.Y - a.CircleCollider.Radius) * _scale + shift.Y,
                    2 * a.CircleCollider.Radius * _scale, 2 * a.CircleCollider.Radius * _scale);
                g.DrawImage(lamb, (a.Pos.X - a.CircleCollider.Radius) * _scale + shift.X, (a.Pos.Y - a.CircleCollider.Radius) * _scale + shift.Y,
                    2 * a.CircleCollider.Radius * _scale, 2 * a.CircleCollider.Radius * _scale);
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
       
        public void RenderObjects (List<IObject> objects, (float X, float Y) FieldSize)
        {
            this.Invoke(UpdatePictureBox, objects, FieldSize);
        }
        public void ShowStatistics(Dictionary<string, int> AnimalsNumber)
        {
            this.Invoke(UpdateStatistics, AnimalsNumber);           
        }
        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка!");
        }
        
    }
}
