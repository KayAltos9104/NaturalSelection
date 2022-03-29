using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NaturalSelectionUI
{
    public partial class FrmGraphics : Form
    {
        int pointRadius = 5;
        int scaleX = 1;
        int scaleY = 1;
        int axisYLength = 300;
        int axisXLength = 200;
        float lineThickness = 2.0f;
        Dictionary<string, List<int>> points = new Dictionary<string, List<int>>();
        delegate Rectangle GenerateCircle(int x, int y);
        delegate (int xS, int yS) ShiftPointToSystemOfAxes(int x, int y);
        public FrmGraphics()
        {
            InitializeComponent();           
        }
        public void UpdateGraph(Dictionary<string,List<int>> points)
        {
            this.points = points;
        }
        public void PlotGraph (object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(Color.White);
            Pen p = new Pen(Color.Black, lineThickness);
            
            scaleX = (e.ClipRectangle.Width - 20) / axisXLength;
            scaleY = (e.ClipRectangle.Height - 20) / axisYLength;

            int axisXShift = (e.ClipRectangle.Width - axisXLength * scaleX) / 2;
            int axisYShift = (e.ClipRectangle.Height - axisYLength * scaleY) / 2;
            g.Clear(Color.White);
            g.DrawLine(p, axisXShift, e.ClipRectangle.Height - axisYShift, axisXShift + axisXLength * scaleX, 
                e.ClipRectangle.Height - axisYShift);
            g.DrawLine(p, axisXShift, e.ClipRectangle.Height - axisYShift, axisXShift, 
                e.ClipRectangle.Height - axisYShift - axisXLength * scaleX);

            ShiftPointToSystemOfAxes ShiftPoint = (int x, int y) =>
            {
                return (x * scaleX + axisXShift, e.ClipRectangle.Height - y * scaleY - axisYShift);
            };

            GenerateCircle ForPoint = (int x, int y) =>
            {
                var sP = ShiftPoint(x, y);
                return new Rectangle(sP.xS - pointRadius, sP.yS - pointRadius,
                    2 * pointRadius, 2 * pointRadius);
            };

          
            foreach (var plot in points)
            {
                if (plot.Key == "Овцы")
                {
                    b.Color = Color.Blue;
                    p.Color = Color.Blue;
                }
                if (plot.Key == "Волки")
                {
                    b.Color = Color.Red;
                    p.Color = Color.Red;
                }

                int zeroPoint = plot.Value.Count - axisXLength;
                if (zeroPoint < 0)
                    zeroPoint = 0;                
                
                int prevPoint = plot.Value[zeroPoint];
                
                for (int i = zeroPoint+1; i< plot.Value.Count; i++)
                {                   
                    g.FillEllipse(b, ForPoint(i-zeroPoint, plot.Value[i]));                    
                    if (i > 0)
                    {
                        var sP1 = ShiftPoint(i - 1-zeroPoint, prevPoint);
                        var sP2 = ShiftPoint(i-zeroPoint, plot.Value[i]);                        
                        g.DrawLine(p, sP1.xS, sP1.yS, sP2.xS, sP2.yS);
                    }
                    if (plot.Value[i] > axisYLength)
                        axisYLength = plot.Value[i] + 10;
                    prevPoint = plot.Value[i];                   
                }
            }            
        }
      
        
    }
}
