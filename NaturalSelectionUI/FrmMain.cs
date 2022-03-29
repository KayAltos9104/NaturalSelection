using BlackWitchEngine;
using NaturalSelectionLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;


namespace NaturalSelectionUI
{
    public partial class FrmMain : Form, IGameCycleView
    {
        private int _scale = 3;
        private (float X, float Y) _fieldSize;
        Image lamb = Image.FromFile("lamb3.png");
        Image wolf = Image.FromFile("wolf.png");
        FrmGraphics statisticsGraphs;
        List<IObject> animals = new List<IObject>();
        Dictionary<string, List<int>> statPoints = new Dictionary<string, List<int>>();

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

                var sortedAnimalsNumber = from a in animalsNumber orderby a.Key select a;

                foreach (var a in sortedAnimalsNumber)
                {
                    s = string.Format("{0} : {1}", a.Key, a.Value);
                    LbxStatistics.Items.Add(s);
                    if (statPoints.ContainsKey(a.Key))
                    {
                        statPoints[a.Key].Add(a.Value);
                    }
                    else
                    {
                        statPoints.Add(a.Key, new List<int>());
                        statPoints[a.Key].Add(a.Value);
                    }
                }
                LbxStatistics.Refresh();
                statisticsGraphs.UpdateGraph(statPoints);
                statisticsGraphs.Refresh();
            };
            statisticsGraphs = new FrmGraphics();
        }
        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            PbxField.Paint += PaintField;
            CycleInitialized.Invoke(this, new InitializedCycleEventArgs() { FieldSize = (500, 300), SheepsNum = 40, WolfsNum = 20 });
            statisticsGraphs.Show();
            statPoints.Clear();
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
                Image sprite=null;
                Animal a = (Animal)o;
                if (a is Sheep)
                {
                    b.Color = Color.Blue;
                    sprite = lamb;
                }                    
                else if (a is Wolf)
                {
                    b.Color = Color.Red;
                    sprite = wolf;
                }   
                DrawUnit(g, sprite, b, p, (a.Pos.X - a.CircleCollider.Radius) * _scale + shift.X, (a.Pos.Y - a.CircleCollider.Radius) * _scale + shift.Y,
                    2 * a.CircleCollider.Radius * _scale, 2 * a.CircleCollider.Radius * _scale);
            }
        }
        public void DrawUnit (Graphics g, Image sprite, SolidBrush b, Pen p, float X, float Y, float sizeX, float sizeY)
        {
            g.FillEllipse(b, X, Y, sizeX, sizeY);
            g.DrawEllipse(p, X, Y, sizeX, sizeY);
            g.DrawImage(sprite, X, Y, sizeX, sizeY);
        }
        public new void Show()
        {
            Application.Run(this);
        }
        private void BtnLaunchCycle_Click(object sender, EventArgs e)
        {
            CycleLaunched.Invoke(this, new EventArgs());
        }
        public void RenderObjects(List<IObject> objects, (float X, float Y) FieldSize)
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
