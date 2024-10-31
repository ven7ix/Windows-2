using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
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

        private PointF mousePosWindow; //позиция курсора мыши (доп. задание)

        private PointF selectedPointGraph; //выделенная точка (доп. задание)
        private bool rightButtonClicked = false;

        public FormRenderFunction()
        {
            InitializeComponent();
            CalculateCenter();
            background = new Backgound(new SolidBrush(panelGraph.BackColor));
        }


        //
        //Конвертации и пересчеты
        //
        private void ResetScale()
        {
            unit = 100.0f;
            labelScale.Text = Math.Round(unit / 100, 2).ToString();
        }

        private void CalculateCenter()
        {
            O = new PointF(panelGraph.Width / 2 + offset.X, panelGraph.Height / 2 + offset.Y);
        }

        private PointF ConvertGraphToWindow(PointF point)
        {
            point.X = point.X * unit + O.X;
            point.Y = -point.Y * unit + O.Y;
            return point;
        }

        private PointF ConvertWindowToGraph(PointF point)
        {
            if (point.X < O.X)
                point.X = -Math.Abs(O.X - point.X) / unit;
            else
                point.X = Math.Abs(O.X - point.X) / unit;

            if (point.Y < O.Y)
                point.Y = Math.Abs(O.Y - point.Y) / unit;
            else
                point.Y = -Math.Abs(O.Y - point.Y) / unit;

            return point;
        }

        //
        //Все расчеты и рендер
        //
        private void PanelGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics screen = e.Graphics;

            DrawBackground(screen);

            DrawStartThings(screen);

            RenderFunctionPoints(screen);

            RenderMinecart(screen);

            RenderBalls(screen);
        }

        private void CalculateFunctionPoints()
        {
            functionPointsList.Clear();

            for (PointF windowPoint = new PointF(0, 0); windowPoint.X < panelGraph.Width; windowPoint.X += 5)
            {
                PointF graphPoint = ConvertWindowToGraph(windowPoint);

                PointF p = new PointF(graphPoint.X, function.Calc(graphPoint.X));
                p = ConvertGraphToWindow(p);

                if (p.Y < -panelGraph.Height || p.Y > panelGraph.Height * 1.5f)
                    continue;

                functionPointsList.Add(p);
            }
        }

        private void RenderFunctionPoints(Graphics screen)
        {
            if (function == null)
                return;

            CalculateFunctionPoints();

            for (int i = 0; i < functionPointsList.Count - 1; i++)
            {
                if (functionPointsList[i].Y < functionPointsList[i + 1].Y && function is Tangent)
                {
                    //Линии для тангенса (ломаются, при прокрутке вниз или вверх)
                    screen.DrawLine(new Pen(graphColor), functionPointsList[i], new PointF(functionPointsList[i].X, 0));
                    screen.DrawLine(new Pen(graphColor), functionPointsList[i + 1], new PointF(functionPointsList[i].X, panelGraph.Height));
                    continue;
                }

                screen.DrawLine(new Pen(graphColor), functionPointsList[i], functionPointsList[i + 1]);
                //screen.DrawRectangle(Pens.Blue, functionPointsList[i].X - 2, functionPointsList[i].Y - 2, 4, 4); //точки
            }
        }


        //
        //Доп задание (большое)
        //
        PointF windowPointBL; //bottom left
        PointF windowPointBR; //bottom right
        PointF windowPointTL; //top left
        PointF windowPointTR; //top right
        //обязательно передавать точку на ГРАФИКЕ
        PointF pointParallel;
        private void CalculateMinecartPosition(PointF point)
        {
            PointF derivativePoint = new PointF(point.X, function.Derivative(point.X));

            //просто формулы из инета
            //LinearExpression functionDerivative = new LinearExpression(derivativePoint.Y, point.Y - derivativePoint.Y * point.X);
            LinearExpression functionNormal = new LinearExpression(-1 / derivativePoint.Y, point.X / derivativePoint.Y + point.Y);

            float angle = (float)Math.Atan(function.Derivative(point.X));

            PointF graphPointBL = new PointF(point.X - 0.5f * (float)Math.Cos(angle), point.Y - 0.5f * (float)Math.Sin(angle)); //bottom left
            PointF graphPointBR = new PointF(point.X + 0.5f * (float)Math.Cos(angle), point.Y + 0.5f * (float)Math.Sin(angle)); //bottom right

            pointParallel = new PointF(point.X, functionNormal.Calc(point.X));

            PointF graphPointTL = new PointF(pointParallel.X - 0.5f * (float)Math.Cos(angle), pointParallel.Y - 0.5f * (float)Math.Sin(angle)); //top left
            PointF graphPointTR = new PointF(pointParallel.X + 0.5f * (float)Math.Cos(angle), pointParallel.Y + 0.5f * (float)Math.Sin(angle)); //top left

            windowPointBL = ConvertGraphToWindow(graphPointBL);
            windowPointBR = ConvertGraphToWindow(graphPointBR);
            windowPointTL = ConvertGraphToWindow(graphPointTL);
            windowPointTR = ConvertGraphToWindow(graphPointTR);
        }

        private void RenderMinecart(Graphics screen)
        {
            if (function == null)
                return;

            PointF mousePosGraph = ConvertWindowToGraph(mousePosWindow);
            PointF pointGraph = new PointF(mousePosGraph.X, function.Calc(mousePosGraph.X));
            CalculateMinecartPosition(pointGraph);

            screen.DrawLine(Pens.Blue, windowPointBL, windowPointBR);
            screen.DrawLine(Pens.Blue, windowPointTL, windowPointBR);
            screen.DrawLine(Pens.Blue, windowPointTL, windowPointBL);
            screen.DrawLine(Pens.Blue, windowPointTR, windowPointBR);
            screen.DrawLine(Pens.Blue, windowPointTR, windowPointBL);

            screen.DrawRectangle(Pens.Red, ConvertGraphToWindow(pointParallel).X - 2, ConvertGraphToWindow(pointParallel).Y - 2, 4, 4);
            screen.DrawRectangle(Pens.Red, windowPointBL.X - 2, windowPointBL.Y - 2, 4, 4);
            screen.DrawRectangle(Pens.Red, windowPointBR.X - 2, windowPointBR.Y - 2, 4, 4);
        }

        struct Ball
        {
            public PointF destinationGraph;
            public PointF originGraph;
            public PointF currentPositionGraph;
            public bool isRender;
            public float speed;

            public Ball(PointF destinationGraph, PointF originGraph)
            {
                this.destinationGraph = destinationGraph;
                this.originGraph = originGraph;
                currentPositionGraph = originGraph;
                isRender = true;
                speed = 0;
            }
        };
        private readonly List<Ball> fallingBalls = new List<Ball>();

        private readonly Random upOrDown = new Random();
        private Ball CalculateBallOrigin(Ball ball)
        {
            //производная в точке
            PointF derivativePoint = new PointF(ball.destinationGraph.X, function.Derivative(ball.destinationGraph.X));
            //прямая, перпендикулярная касательной функции в точке derivativePoint
            float kNormal = -1 / derivativePoint.Y;
            float bNormal = ball.destinationGraph.X / derivativePoint.Y + ball.destinationGraph.Y;

            //спавнить мяч сверху или снизу
            float yOrigin = ConvertWindowToGraph(new PointF(0, 0)).Y;
            if (upOrDown.Next() % 2 == 0)
                yOrigin = ConvertWindowToGraph(new PointF(0, panelGraph.Height)).Y;

            float xOrigin = (yOrigin - bNormal) / kNormal;
            ball.originGraph = new PointF(xOrigin, yOrigin);

            ball.speed = Math.Sign(ball.destinationGraph.X - ball.originGraph.X) * 0.1f;
            
            return ball;
        }

        readonly Random randomPoint = new Random();
        private Ball GenerateBall()
        {
            Ball ball = new Ball(new PointF(0, 0), new PointF(0, 0));

            //берем рандомную точку из списка точек графика, такую, чтобы ее было видно на экране
            int attempts = 0;
            PointF ballDestinationWindow;
            do
            {
                ballDestinationWindow = functionPointsList[randomPoint.Next(0, functionPointsList.Count - 1)];
                if (attempts++ > 10)
                    break;
            }
            while (ballDestinationWindow.Y < 0 || ballDestinationWindow.Y > panelGraph.Height);

            ball.destinationGraph = ConvertWindowToGraph(ballDestinationWindow);
            ball = CalculateBallOrigin(ball);
            ball.currentPositionGraph = ball.originGraph;

            return ball;
        }

        private Ball CalculateCurrentBallPosition(Ball ball)
        {
            //производная в точке
            PointF derivativePoint = new PointF(ball.destinationGraph.X, function.Derivative(ball.destinationGraph.X));
            //прямая, перпендикулярная касательной функции в точке derivativePoint
            LinearExpression functionNormal = new LinearExpression(-1 / derivativePoint.Y, ball.destinationGraph.X / derivativePoint.Y + ball.destinationGraph.Y);

            //скорость мячика
            ball.currentPositionGraph.X += ball.speed;
            //изменяем Y
            ball.currentPositionGraph.Y = functionNormal.Calc(ball.currentPositionGraph.X);

            return ball;
        }

        private void RenderBalls(Graphics screen)
        {
            if (function == null)
                return;

            if (!timerFallingBalls.Enabled)
                return;

            int ballRadius = (int)(0.2f * unit);
            for (int i = 0; i < fallingBalls.Count; i++)
            {
                if (CollisionCheck(fallingBalls[i]))
                {
                    fallingBalls.RemoveAt(i);
                    continue;
                }

                PointF ballCurrentPositionWindow = ConvertGraphToWindow(fallingBalls[i].currentPositionGraph);
                screen.DrawEllipse(Pens.Red, ballCurrentPositionWindow.X - ballRadius, ballCurrentPositionWindow.Y - ballRadius, 2 * ballRadius, 2 * ballRadius);

                if (fallingBalls[i].speed < 0)
                    screen.DrawLine(Pens.Blue, ConvertGraphToWindow(fallingBalls[i].originGraph), ConvertGraphToWindow(fallingBalls[i].destinationGraph));
                else
                    screen.DrawLine(Pens.Red, ConvertGraphToWindow(fallingBalls[i].originGraph), ConvertGraphToWindow(fallingBalls[i].destinationGraph));
            }
        }

        private bool CollisionCheck(Ball ball)
        {
            if (ball.currentPositionGraph.X > ball.destinationGraph.X && ball.destinationGraph.X > ball.originGraph.X)
                return true;

            if (ball.currentPositionGraph.X < ball.destinationGraph.X && ball.destinationGraph.X < ball.originGraph.X)
                return true;


            PointF collisionPointGraph = new PointF(ball.currentPositionGraph.X, function.Calc(ball.currentPositionGraph.X));
            float epsilon = 0.5f;

            if (collisionPointGraph.Y > ball.currentPositionGraph.Y - epsilon && collisionPointGraph.Y < ball.currentPositionGraph.Y + epsilon)
                return true;

            return false;
        }

        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            if (functionPointsList.Count == 0)
                return;

            timerFallingBalls.Enabled = true;

            for (int i = 0; i < 10; i++)
                fallingBalls.Add(GenerateBall());
        }

        private void TimerFallingBalls_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < fallingBalls.Count; i++)
                fallingBalls[i] = CalculateCurrentBallPosition(fallingBalls[i]);

            panelGraph.Invalidate();
        }

        
        //
        //События панели
        //
        private void PanelGraph_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                rightButtonClicked = true;
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
                    function = new LinearExpression(2, 5);
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
            BackgroundStyle_Change(panelGraph.BackColor, background.style); //чтобы градиент работал

            panelGraph.Invalidate();
        }

        private void PanelGraph_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && unit < 2000)
                unit *= 1.1f;
            else if (e.Delta < 0 && unit > 20)
                unit /= 1.1f;

            labelScale.Text = Math.Round(unit / 100, 2).ToString();

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
            mousePosWindow.X = e.X;
            mousePosWindow.Y = e.Y;

            CalculateCenter();
            if (isDragging)
            {
                PointF delta = new PointF(e.X - previousMousePosition.X, e.Y - previousMousePosition.Y);

                offset.X += delta.X;
                offset.Y += delta.Y;

                previousMousePosition = e.Location;
            }
            panelGraph.Invalidate();
        }

        private void ButtonSaveFunction_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image |*.png",
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(panelGraph.Width, panelGraph.Height);
                panelGraph.DrawToBitmap(bitmap, new Rectangle(0, 0, panelGraph.Width, panelGraph.Height));
                bitmap.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }


        //
        //Стиль фона и начальные части графика
        //
        private void DrawStartThings(Graphics screen)
        {
            //Coordinate Axes
            screen.DrawLine(Pens.Black, new PointF(O.X, 0), new PointF(O.X, panelGraph.Height));
            screen.DrawLine(Pens.Black, new PointF(0, O.Y), new PointF(panelGraph.Width, O.Y));

            //Unit Circle
            IFunction forCircle = new Sine();
            List<PointF> pointsListCircle = new List<PointF>();

            for (float x = 0; x < 2 * (float)Math.PI; x += 0.1f)
            {
                PointF p = new PointF(forCircle.Calc(x - (float)Math.PI / 2), forCircle.Calc(x));
                p = ConvertGraphToWindow(p);
                pointsListCircle.Add(p);
            }

            PointF[] pointsArray = pointsListCircle.ToArray();

            for (int i = 0; i < pointsArray.Length - 2; i += 2)
                screen.DrawLine(Pens.Black, pointsArray[i], pointsArray[i + 1]);
        }

        private void DrawBackground(Graphics screen)
        {
            if (background.brush == null)
                return;

            if (background.style == "Picture")
                screen.DrawImage(Properties.Resources.deathStare, 0, 0, panelGraph.Width, panelGraph.Height);
            else
                screen.FillRectangle(background.brush, panelGraph.ClientRectangle);

            //доп задание
            //RenderChessPattern(screen);
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


        //
        //Доп задания (маленькие)
        //
        private void RenderChessPattern(Graphics screen)
        {
            Point count = new Point(0, 0);
            PointF windowPoint = new PointF(0, 0);
            for (; windowPoint.Y < panelGraph.Height; windowPoint.Y += 30)
            {
                for (; windowPoint.X < panelGraph.Width; windowPoint.X += 30)
                {
                    if (count.X % 2 == 0)
                        screen.FillRectangle(Brushes.Beige, windowPoint.X, windowPoint.Y, windowPoint.X + 30, windowPoint.Y + 30);
                    else
                        screen.FillRectangle(Brushes.White, windowPoint.X, windowPoint.Y, windowPoint.X + 30, windowPoint.Y + 30);

                    count.X++;
                }

                if (count.Y % 2 == 0)
                    count.X = 1;
                else
                    count.X = 0;

                count.Y++;
                windowPoint.X = 0;
            }
        }

        private void DrawSelectedPoint(Graphics screen)
        {
            if (rightButtonClicked)
            {
                selectedPointGraph = ConvertWindowToGraph(mousePosWindow);
                rightButtonClicked = false;
            }

            PointF selectedPointWindow = ConvertGraphToWindow(selectedPointGraph);

            //создаем новый point, чтобы label не перекрывал точку
            labelSelectedCoordinateXY.Location = Point.Round(new PointF(selectedPointWindow.X + 2, selectedPointWindow.Y + 2));
            labelSelectedCoordinateXY.Text = "x: " + selectedPointGraph.X.ToString() + " y: " + selectedPointGraph.Y.ToString();
            labelSelectedCoordinateXY.Update();

            screen.FillRectangle(Brushes.Red, selectedPointWindow.X - 2, selectedPointWindow.Y - 2, 4, 4);
        }

        private void MouseBeam(Graphics screen)
        {
            if (function == null)
                return;

            PointF mousePosGraph = ConvertWindowToGraph(mousePosWindow);

            PointF pointGraph = new PointF(mousePosGraph.X, function.Calc(mousePosGraph.X));
            PointF pointWindow = ConvertGraphToWindow(pointGraph);

            screen.DrawLine(Pens.Red, pointWindow.X, O.Y, pointWindow.X, pointWindow.Y);
            screen.DrawLine(Pens.Red, O.X, pointWindow.Y, pointWindow.X, pointWindow.Y);

            labelCoordinateXY.Location = Point.Round(pointWindow);
            labelCoordinateXY.Text = "x: " + pointGraph.X.ToString() + " y: " + pointGraph.Y.ToString();
            labelCoordinateXY.Update();
        }
    }
}