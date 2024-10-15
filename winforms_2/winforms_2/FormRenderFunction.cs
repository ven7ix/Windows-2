﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.AxHost;

namespace winforms_2
{
    public partial class FormRenderFunction : Form
    {
        private PointF O; //(0, 0); ценрт экрана
        private IFunction function; //чтобы определить какую функцию надо рисовать
        private float unit = 100.0f; //еденичная длина
        private readonly List<PointF> functionPointsList = new List<PointF>();
        private PointF trueO = new PointF(0, 0);
        private Color graphColor = Color.Black;
        private Brush backgroundBrush;
        private string backgroundStyle = "Solid";

        private bool isDragging = false;
        private Point previousMousePosition;

        public FormRenderFunction()
        {
            InitializeComponent();
            CalculateCenter();
        }

        private void CalculateCenter()
        {
            O = new PointF(panelGraph.Width / 2 + trueO.X, panelGraph.Height / 2 + trueO.Y);
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
                screen.DrawLine(Pens.Gray, pointsArray[i], pointsArray[i + 1]);
        }

        private void CalculateFunctionPoints()
        {
            functionPointsList.Clear();

            float bounds = panelGraph.Width / 2 / unit + 1;
            for (float x = -bounds; x <= bounds; x += 0.01f)
            {
                PointF p = new PointF(x - trueO.X / unit, function.Calc(x - trueO.X / unit));
                p = MakePointNormal(p);
                functionPointsList.Add(p);
            }
        }
        
        private void PanelGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics screen = e.Graphics;

            if (backgroundBrush != null)
                screen.FillRectangle(backgroundBrush, panelGraph.ClientRectangle);

            if (backgroundStyle == "Texture")
                screen.FillEllipse(backgroundBrush, 0, 0, 100, 50);

            DrawStartThings(screen);
            
            if (functionPointsList.Count != 0)
            {
                CalculateFunctionPoints();
                screen.DrawLines(new Pen(graphColor), functionPointsList.ToArray());
            }
        }

        private void ResetScale()
        {
            unit = 100.0f;
            labelScale.Text = "1:" + Math.Round(unit / 100, 1).ToString();
        }

        private void ButtonRandomFunction_Click(object sender, EventArgs e)
        {
            ResetScale();

            Random random = new Random();

            switch(random.Next(0, 4))
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
            panelGraph.Invalidate();
        }

        private void PanelGraph_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                unit *= 1.1f;
            else if (e.Delta < 0)
                unit /= 1.1f;

            labelScale.Text = "1:" + Math.Round(unit / 100, 1).ToString();

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

                trueO.X += delta.X;
                trueO.Y += delta.Y;

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

        private void BackgroundStyle_OnDataSubmited(Color color, string style)
        {
            panelGraph.BackColor = color;

            switch (style)
            {
                case "Solid":
                    backgroundBrush = new SolidBrush(color);
                    break;
                case "LinearGradient":
                    backgroundBrush = new LinearGradientBrush(panelGraph.ClientRectangle, color, ControlPaint.Dark(color), 45);
                    break;
                case "Hatch":
                    backgroundBrush = new HatchBrush(HatchStyle.DiagonalCross, color, ControlPaint.Dark(color));
                    break;
                case "Texture":
                    backgroundBrush = new TextureBrush(Properties.Resources.deathStare, new Rectangle(220, 220, 120, 120));
                    break;
                case "Picture":
                    panelGraph.BackgroundImage = Properties.Resources.deathStare;
                    break;
                default:
                    break;
            }

            panelGraph.Invalidate();
        }

        private void ButtonBackgroudStyle_Click(object sender, EventArgs e)
        {
            BackgroundStyle background = new BackgroundStyle();
            background.OnDataSubmitted += BackgroundStyle_OnDataSubmited;
            background.ShowDialog();
            panelGraph.Invalidate();
        }
    }
}