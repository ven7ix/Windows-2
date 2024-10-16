using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace winforms_2
{
    public partial class FormRenderFunction : Form
    {
        private PointF O; //(0, 0); ценрт экрана
        private IFunction function; //чтобы определить какую функцию надо рисовать
        private float unit = 100.0f; //еденичная длина
        private readonly List<PointF> functionPointsList = new List<PointF>();
        private PointF offset = new PointF(0, 0);
        private float levelOfDetail = 0.1f;

        struct Backgound
        {
            public Brush brush;
            public string style;

            public Backgound(Brush brush)
            {
                this.brush = brush;
                style = "Solid";
            }
        }
        private Backgound background;
        private Color graphColor = Color.Black;

        private bool isDragging = false;
        private Point previousMousePosition;

        public FormRenderFunction()
        {
            InitializeComponent();
            CalculateCenter();
            background = new Backgound(new SolidBrush(panelGraph.BackColor));
        }

        private void CalculateCenter()
        {
            O = new PointF(panelGraph.Width / 2 + offset.X, panelGraph.Height / 2 + offset.Y);
        }

        private PointF MakePointNormal(PointF point)
        {
            point.X = point.X * unit + O.X;
            point.Y = -point.Y * unit + O.Y; //отсчет у Y идет сверху вниз (самый верх - 0, самый низ - screen.Height)
            return point;
        }

        private void DrawStartThings(Graphics screen)
        {
            //Coordinate Axes
            screen.DrawLine(Pens.Black, new PointF(O.X, 0), new PointF(O.X, panelGraph.Height));
            screen.DrawLine(Pens.Black, new PointF(0, O.Y), new PointF(panelGraph.Width, O.Y));

            //Unit Circle
            IFunction forCircle = new Sine();
            List<PointF> pointsList = new List<PointF>();

            for (float x = 0; x < 2 * (float)Math.PI; x += 0.1f)
            {
                PointF p = new PointF(forCircle.Calc(x - (float)Math.PI / 2), forCircle.Calc(x));
                p = MakePointNormal(p);
                pointsList.Add(p);
            }

            PointF[] pointsArray = pointsList.ToArray();

            for (int i = 0; i < pointsArray.Length - 2; i += 2)
                screen.DrawLine(Pens.Black, pointsArray[i], pointsArray[i + 1]);
        }

        private void CalculateLevelOfDetail()
        {
            if (unit < 25)
                levelOfDetail = 0.5f;
            else if (unit > 500)
                levelOfDetail = 0.01f;
            else
                levelOfDetail = 0.1f;
        }

        private void CalculateFunctionPoints()
        {
            functionPointsList.Clear();
            CalculateLevelOfDetail();

            float boundsX = panelGraph.Width / 2 / unit + 1;
            for (float x = -boundsX; x <= boundsX; x += levelOfDetail)
            {
                float xOffsetUnited = x - offset.X / unit;

                PointF p = new PointF(xOffsetUnited, function.Calc(xOffsetUnited));
                p = MakePointNormal(p);
                functionPointsList.Add(p);

                //показ количества точек
                labelPointsCount.Text = functionPointsList.Count.ToString();
            }
        }
        
        private void PanelGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics screen = e.Graphics;

            if (background.brush != null)
            {
                if (background.style == "Picture")
                    screen.DrawImage(Properties.Resources.deathStare, 0, 0, panelGraph.Width, panelGraph.Height);
                else
                    screen.FillRectangle(background.brush, panelGraph.ClientRectangle);
            }

            DrawStartThings(screen);

            labelCoordinateX.Text = "X: " + (-offset.X).ToString();
            labelCoordinateY.Text = "Y: " + offset.Y.ToString();

            if (functionPointsList.Count != 0)
            {
                CalculateFunctionPoints();

                for (int i = 0; i < functionPointsList.Count - 1; i++)
                    screen.DrawLine(new Pen(graphColor), functionPointsList[i], functionPointsList[i + 1]);
            }
        }

        private void ResetScale()
        {
            unit = 100.0f;
            labelScale.Text = "1:" + Math.Round(unit / 100, 2).ToString();
        }

        private void ButtonRandomFunction_Click(object sender, EventArgs e)
        {
            ResetScale();

            Random random = new Random();
            switch(random.Next(0, 5))
            {
                case 0:
                    function = new Sine();
                    break;
                case 1:
                    function = new Tangent();
                    break;
                case 2:
                    function = new RandomAssFuckingLinearExpression();
                    break;
                case 3:
                    function = new Cubed();
                    break;
                case 4:
                    function = new Squared();
                    break;
            }

            CalculateFunctionPoints();
            panelGraph.Invalidate();
        }

        private void PanelGraph_Resize(object sender, EventArgs e)
        {
            CalculateCenter();
            BackgroundStyle_Change(panelGraph.BackColor, background.style);
            panelGraph.Invalidate();
        }

        private void PanelGraph_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && unit < 2000)
                unit *= 1.1f;
            else if (e.Delta < 0 && unit > 10)
                unit /= 1.1f;

            labelScale.Text = "1:" + Math.Round(unit / 100, 2).ToString();

            panelGraph.Invalidate();
        }

        private void PanelGraph_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                previousMousePosition = e.Location;
            }
        }

        private void PanelGraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void PanelGraph_MouseMove(object sender, MouseEventArgs e)
        {
            CalculateCenter();
            if (isDragging)
            {
                PointF delta = new PointF(e.X - previousMousePosition.X, e.Y - previousMousePosition.Y);

                offset.X += delta.X;
                offset.Y += delta.Y;

                previousMousePosition = e.Location;
                panelGraph.Invalidate();
            }
        }

        private void ButtonSaveFunction_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            };

            string dataToSave = "";

            foreach (PointF p in functionPointsList)
                dataToSave += p.X.ToString() + "\t" + p.Y.ToString() + "\n";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, dataToSave);
            }
        }

        private void ButtonChangeGraphColor_Click(object sender, EventArgs e)
        {
            colorDialogGraphColor.Color = graphColor;
            if (colorDialogGraphColor.ShowDialog() == DialogResult.Cancel)
                return;
            graphColor = colorDialogGraphColor.Color;
            panelGraph.Invalidate();
        }

        private void BackgroundStyle_Change(Color color, string style)
        {
            panelGraph.BackColor = color;
            background.style = style;

            switch (style)
            {
                case "Solid":
                    background.brush = new SolidBrush(color);
                    break;
                case "Gradient":
                    background.brush = new LinearGradientBrush(panelGraph.ClientRectangle, color, ControlPaint.Dark(color), 45);
                    break;
                case "Hatch":
                    background.brush = new HatchBrush(HatchStyle.DiagonalCross, color, ControlPaint.Dark(color));
                    break;
                case "Texture":
                    background.brush = new TextureBrush(Properties.Resources.deathStare, new Rectangle(220, 220, 120, 120));
                    break;
                default:
                    break;
            }
            panelGraph.Invalidate();
        }

        private void ButtonBackgroudStyle_Click(object sender, EventArgs e)
        {
            BackgroundStyle background = new BackgroundStyle();
            background.OnDataSubmitted += BackgroundStyle_Change;
            background.ShowDialog();
        }
    }
}