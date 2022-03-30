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
        private float _scale = 1;
        //private (int X, int Y) _renderShift = (0,0);
        private (int X, int Y) shift = (0, 0);
        private bool _scrollActive = false;
        private (bool xRight, bool yRight) _scrollDirection = (true,true);

        private (float X, float Y) _fieldSize;
        private Image lamb = Image.FromFile("lamb3.png");
        private Image wolf = Image.FromFile("wolf.png");
        private FrmGraphics statisticsGraphs;
        private List<IObject> animals = new List<IObject>();
        private Dictionary<string, List<int>> statPoints = new Dictionary<string, List<int>>();

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
            CycleInitialized.Invoke(this, new InitializedCycleEventArgs() { FieldSize = (1000, 1000), SheepsNum = 100, WolfsNum = 100 });
            statisticsGraphs.Show();
            statPoints.Clear();
            PbxField.Refresh();
        }
        private void PaintField(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(Color.White);
            Pen p = new Pen(Color.Black, 1.0f);
            if (_scrollActive)
            {
                if (_scrollDirection.xRight)
                    shift.X -= 10;
                else
                    shift.X += 10;

                if (_scrollDirection.yRight)
                    shift.Y -= 10;
                else
                    shift.Y += 10;
            }            
            //Инициализация пустого белого поля
            g.FillRectangle(b, e.ClipRectangle);
            b.Color = Color.LightGreen;
            g.FillRectangle(b, shift.X, shift.Y, (int)_fieldSize.X * _scale, (int)_fieldSize.Y * _scale);
            //Отрисовка границ
            g.DrawRectangle(p, shift.X, shift.Y, (int)_fieldSize.X * _scale, (int)_fieldSize.Y * _scale);
            //Отрисовка зверей
            try
            {
                foreach (var o in animals)
                {
                    Image sprite = null;
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
            catch
            {

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
            //if (_scaleChangeCooldown > 0)
            //    _scaleChangeCooldown--;
        }
        public void ShowStatistics(Dictionary<string, int> AnimalsNumber)
        {
            this.Invoke(UpdateStatistics, AnimalsNumber);
        }
        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка!");
        }
        //TODO: Почему-то, при прокрутке колеса он иногда вылетает с изменением списка animals O_O
        public void PbxFieldWheel (object sender, MouseEventArgs e)
        {    
                if (e.Delta > 0 && _scale < 10)
                {
                    _scale += 0.1f;
                    PbxField.Refresh();
                }
                else if (e.Delta < 0 && _scale > 0.1)
                {
                    _scale-=0.1f;
                    PbxField.Refresh();
                }
        }
        public void PbxFieldClick (object sender, MouseEventArgs e)
        {
            var c = (Control)sender;
            (int X, int Y) center = (c.Width / 2, c.Height / 2);            
            if (e.Button == MouseButtons.Right)
            {
                c.Cursor = Cursors.SizeAll;
                if (e.X > center.X)
                    _scrollDirection.xRight = true;
                else
                    _scrollDirection.xRight = false;

                if (e.Y > center.Y)
                    _scrollDirection.yRight = true;
                else
                    _scrollDirection.yRight = false;
                c.Refresh();
                _scrollActive = true;
            }
            c.Cursor = Cursors.Default;
        }
        public void PbxFieldMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _scrollActive = false;                
            }
               
        }
        
    }
}
