using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicsAlgorithm
{
    public partial class Form1 : Form
    {
        Graphics g;
        Brush brush = Brushes.Black;

        public Form1()
        {
            InitializeComponent();
        }
        private void getScreenSize(object sender, EventArgs e)
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(width, height);
        }
        //draw coordinates planet
        private void Form1_Shown(object sender, EventArgs e)
        {
            g = panelGraph.CreateGraphics();
            Pen p = Pens.White;
            g.DrawLine(p, 0, panelGraph.Height / 2, panelGraph.Width, panelGraph.Height / 2);
            g.DrawLine(p, panelGraph.Width / 2, 0, panelGraph.Width / 2, panelGraph.Height);
        }
        private float getX(float x)
        {
            return x + panelGraph.Width / 2;
        }
        private float getY(float y)
        {
            return -(y) + panelGraph.Height / 2;
        }

        //Clear Graph
        public void button2_Click(object sender, EventArgs e)
        {
            g = panelGraph.CreateGraphics();
            g.Clear(Color.Silver);
            Form1_Shown(sender, e);
        }

        //DDA_Algorithm
        private void DDA_Button(object sender, EventArgs e)
        {
            //get input from textBox
            if (!String.IsNullOrEmpty(x1Text.Text) && (!String.IsNullOrEmpty(y1Text.Text))
                && (!String.IsNullOrEmpty(x2Text.Text)) && (!String.IsNullOrEmpty(y2Text.Text)))
            {
                float x0 = float.Parse(x1Text.Text), y0 = float.Parse(y1Text.Text);
                float xEnd = float.Parse(x2Text.Text), yEnd = float.Parse(y2Text.Text);
                lineDDA(x0, y0, xEnd, yEnd, Brushes.Red);
            }
        }
        public void lineDDA(float x0, float y0, float xEnd, float yEnd, Brush brush)
        {

            g = panelGraph.CreateGraphics();

            //start algo
            float dx = xEnd - x0, dy = yEnd - y0, steps, k;
            float xIncrement, yIncrement, x = x0, y = y0;

            if (Math.Abs(dx) > Math.Abs(dy)) steps = Math.Abs(dx);
            else steps = Math.Abs(dy);

            xIncrement = (float)(dx) / (float)(steps);
            yIncrement = (float)(dy) / (float)(steps);

            g.FillRectangle(brush, getX((float)Math.Round(x)), getY((float)Math.Round(y)), 2, 2);
            for (k = 0; k < steps; k++)
            {
                x += xIncrement;
                y += yIncrement;
                g.FillRectangle(brush, getX((float)Math.Round(x)), getY((float)Math.Round(y)), 2, 2);
            }
        }

        //Mid Point Circle Algorithm
        private void DrawCircle_Click(object sender, EventArgs e)
        {
            //get input from textBox
            if (!String.IsNullOrEmpty(xCenterText.Text) && (!String.IsNullOrEmpty(yCenterText.Text))
               && (!String.IsNullOrEmpty(radiusText.Text)))
            {
                float xCenter = float.Parse(xCenterText.Text), yCenter = float.Parse(yCenterText.Text);
                float radius = float.Parse(radiusText.Text);
                CicleAlgorithm(radius, xCenter, yCenter);
            }
        }
        public void CicleAlgorithm(float radius, float xCenter, float yCenter)
        {
            g = panelGraph.CreateGraphics();
            float x = 0;
            float y = radius;
            float d = 1 - radius;
            g.FillRectangle(brush, x + 200, y + 200, 2, 2);
            while (y > x)
            {
                if (d < 0) d += 2 * x + 3;
                else
                {
                    y--;
                    d += 2 * (x - y) + 5;
                }
                x++;
                ciclePlotPoints(xCenter, yCenter, x, y);
            }
        }
        public void ciclePlotPoints(float xCenter, float yCenter, float x, float y)
        {
            g = panelGraph.CreateGraphics();

            g.FillRectangle(brush, getX(xCenter + x), getY(yCenter + y), 2, 2);
            g.FillRectangle(brush, getX(xCenter - x), getY(yCenter + y), 2, 2);
            g.FillRectangle(brush, getX(xCenter + x), getY(yCenter - y), 2, 2);
            g.FillRectangle(brush, getX(xCenter - x), getY(yCenter - y), 2, 2);

            g.FillRectangle(brush, getX(xCenter + y), getY(yCenter + x), 2, 2);
            g.FillRectangle(brush, getX(xCenter - y), getY(yCenter + x), 2, 2);
            g.FillRectangle(brush, getX(xCenter + y), getY(yCenter - x), 2, 2);
            g.FillRectangle(brush, getX(xCenter - y), getY(yCenter - x), 2, 2);

        }

        //Bresenham Algorithm
        private void buttonBresenham_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(x1Bresenham.Text) && (!String.IsNullOrEmpty(y1Bresenham.Text))
              && (!String.IsNullOrEmpty(x2Bresenham.Text)) && (!String.IsNullOrEmpty(y2Bresenham.Text)))
            {
                float x0 = float.Parse(x1Bresenham.Text), y0 = float.Parse(y1Bresenham.Text);
                float xEnd = float.Parse(x2Bresenham.Text), yEnd = float.Parse(y2Bresenham.Text);

                float mile = (float)(yEnd - y0) / (float)(xEnd - x0);
                //MessageBox.Show(mile.ToString());
                lineBres(x0, y0, xEnd, yEnd);

            }
        }

        public void lineBres(float x, float y, float x2, float y2)
        {
            g = panelGraph.CreateGraphics();
            float w = x2 - x;
            float h = y2 - y;
            float dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            float farPoint = Math.Abs(w);
            float nearPoint = Math.Abs(h);
            if (!(farPoint > nearPoint))
            {
                farPoint = Math.Abs(h);
                nearPoint = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            float numCounter = farPoint / 2;
            for (int i = 0; i <= farPoint; i++)
            {
                g.FillRectangle(brush, getX(x), getY(y), 2, 2);
                numCounter += nearPoint;
                if (!(numCounter < farPoint))
                {
                    numCounter -= farPoint;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        //Ellipse Algorithm
        private void DrawEllipse_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(xCenterEllipse.Text) && (!String.IsNullOrEmpty(yCenterEllipse.Text))
             && (!String.IsNullOrEmpty(xRadius.Text)) && (!String.IsNullOrEmpty(yRadius.Text)))
            {
                float xc = int.Parse(xCenterEllipse.Text), yc = float.Parse(yCenterEllipse.Text); //point Center
                float xr = int.Parse(xRadius.Text), yr = float.Parse(yRadius.Text); //radius

                ellipseAlgorithm(xc, yc, xr, yr);

            }
        }

        private void ellipseAlgorithm(float xc, float yc, float xr, float yr)
        {
            float dx, dy, p1, p2, x, y;
            g = panelGraph.CreateGraphics();
            x = 0;
            y = yr;

            //calculate initial Value
            p1 = (yr * yr) - (xr * xr * yr) + (0.25f * xr * xr);
            dx = 2 * yr * yr * x;
            dy = 2 * xr * xr * y;

            while (dx < dy)
            {
                g.FillRectangle(brush, getX(x + xc), getY(y + yc), 2, 2);
                g.FillRectangle(brush, getX(-x + xc), getY(y + yc), 2, 2);
                g.FillRectangle(brush, getX(x + xc), getY(-y + yc), 2, 2);
                g.FillRectangle(brush, getX(-x + xc), getY(-y + yc), 2, 2);

                if (p1 < 0)
                {
                    dx = dx + (2 * yr * yr);
                    p1 = p1 + dx + (yr * yr);
                    x++;
                }
                else
                {
                    dx = dx + (2 * yr * yr);
                    dy = dy - (2 * xr * xr);
                    p1 = p1 + dx - dy + (yr * yr);
                    x++;
                    y--;
                }
            }
            //calculate Second initial Value in region 2 with last point
            p2 = ((yr * yr) * ((x + 0.5f) * (x + 0.5f))) +
                 ((xr * xr) * ((y - 1) * (y - 1))) -
                  (xr * xr * yr * yr);

            while (y >= 0)
            {

                g.FillRectangle(brush, getX(x + xc), getY(y + yc), 2, 2);
                g.FillRectangle(brush, getX(-x + xc), getY(y + yc), 2, 2);
                g.FillRectangle(brush, getX(x + xc), getY(-y + yc), 2, 2);
                g.FillRectangle(brush, getX(-x + xc), getY(-y + yc), 2, 2);

                if (p2 > 0)
                {
                    dy = dy - (2 * xr * xr);
                    p2 = p2 + (xr * xr) - dy;
                    y--;
                }
                else
                {
                    dx = dx + (2 * yr * yr);
                    dy = dy - (2 * xr * xr);
                    p2 = p2 + dx - dy + (xr * xr);
                    x++;
                    y--;
                }
            }
        }

        //Draw shape with transformations
        private void DrawShape_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
             && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
             && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
              && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);

                lineDDA(x0, y0, x1, y1, Brushes.Black);
                lineDDA(x1, y1, x2, y2, Brushes.Black);
                lineDDA(x2, y2, x3, y3, Brushes.Black);
                lineDDA(x3, y3, x0, y0, Brushes.Black);
            }
        }

        //Do Scaling
        private void scaling_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
            && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
            && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
             && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text))
              && (!String.IsNullOrEmpty(XT.Text)) && (!String.IsNullOrEmpty(YT.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);

                //scale point
                if(float.Parse(XT.Text) != 0)
                {
                    x1 *= float.Parse(XT.Text);
                    x2 *= float.Parse(XT.Text);
                    x3 *= float.Parse(XT.Text);
                }
                if (float.Parse(YT.Text) != 0)
                {
                    y1 *= float.Parse(YT.Text);
                    y2 *= float.Parse(YT.Text);
                    y3 *= float.Parse(YT.Text);
                }

                lineDDA(x0, y0, x1, y1, Brushes.Blue);
                lineDDA(x1, y1, x2, y2, Brushes.Blue);
                lineDDA(x2, y2, x3, y3, Brushes.Blue);
                lineDDA(x3, y3, x0, y0, Brushes.Blue);
            }
        }

        //Do Transation
        private void transform_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
            && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
            && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
             && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text))
              && (!String.IsNullOrEmpty(XT.Text)) && (!String.IsNullOrEmpty(YT.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);

                //transation point
                x0 += float.Parse(XT.Text);
                y0 += float.Parse(YT.Text);

                x1 += float.Parse(XT.Text);
                y1 += float.Parse(YT.Text);

                x2 += float.Parse(XT.Text);
                y2 += float.Parse(YT.Text);

                x3 += float.Parse(XT.Text);
                y3 += float.Parse(YT.Text);

                lineDDA(x0, y0, x1, y1, Brushes.OrangeRed);
                lineDDA(x1, y1, x2, y2, Brushes.OrangeRed);
                lineDDA(x2, y2, x3, y3, Brushes.OrangeRed);
                lineDDA(x3, y3, x0, y0, Brushes.OrangeRed);
            }
        }

        //Do Reflection Over X
        private void ReflectionOverX(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
            && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
            && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
             && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);

                //transation point
                x0 *= -1;
                x1 *= -1;
                x2 *= -1;
                x3 *= -1;

                lineDDA(x0, y0, x1, y1, Brushes.Yellow);
                lineDDA(x1, y1, x2, y2, Brushes.Yellow);
                lineDDA(x2, y2, x3, y3, Brushes.Yellow);
                lineDDA(x3, y3, x0, y0, Brushes.Yellow);
            }
        }
        //Do Reflection Over Y
        private void ReflectionOverY(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
            && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
            && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
             && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);

                //transation point
                y0 *= -1;
                y1 *= -1;
                y2 *= -1;
                y3 *= -1;

                lineDDA(x0, y0, x1, y1, Brushes.Yellow);
                lineDDA(x1, y1, x2, y2, Brushes.Yellow);
                lineDDA(x2, y2, x3, y3, Brushes.Yellow);
                lineDDA(x3, y3, x0, y0, Brushes.Yellow);
            }

        }
        //Do Reflection Over Origin
        private void ReflectionOverOrigin(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
           && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
           && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
            && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);

                //transation point
                x0 *= -1;
                x1 *= -1;
                x2 *= -1;
                x3 *= -1;
                y0 *= -1;
                y1 *= -1;
                y2 *= -1;
                y3 *= -1;

                lineDDA(x0, y0, x1, y1, Brushes.Yellow);
                lineDDA(x1, y1, x2, y2, Brushes.Yellow);
                lineDDA(x2, y2, x3, y3, Brushes.Yellow);
                lineDDA(x3, y3, x0, y0, Brushes.Yellow);
            }
        }

        //Do Shearing X
        private void ShearingX_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
            && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
            && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
             && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text))
              && (!String.IsNullOrEmpty(XT.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);

                //transation point
                x2 += float.Parse(XT.Text) * 10;
                x3 += float.Parse(XT.Text) * 10;

                lineDDA(x0, y0, x1, y1, Brushes.DarkViolet);
                lineDDA(x1, y1, x2, y2, Brushes.DarkViolet);
                lineDDA(x2, y2, x3, y3, Brushes.DarkViolet);
                lineDDA(x3, y3, x0, y0, Brushes.DarkViolet);
            }
        }

        //Do Shearing Y
        private void ShearingY_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
           && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
           && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
            && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text))
             && (!String.IsNullOrEmpty(YT.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);

                //transation point
                y1 += float.Parse(YT.Text) * 10;
                y2 += float.Parse(YT.Text) * 10;

                lineDDA(x0, y0, x1, y1, Brushes.DarkViolet);
                lineDDA(x1, y1, x2, y2, Brushes.DarkViolet);
                lineDDA(x2, y2, x3, y3, Brushes.DarkViolet);
                lineDDA(x3, y3, x0, y0, Brushes.DarkViolet);
            }
        }

        private void Rotation_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(x1SqaureText.Text) && (!String.IsNullOrEmpty(y1SqaureText.Text))
           && (!String.IsNullOrEmpty(x2SqaureText.Text)) && (!String.IsNullOrEmpty(y2SqaureText.Text))
           && (!String.IsNullOrEmpty(x3SqaureText.Text)) && (!String.IsNullOrEmpty(y3SqaureText.Text))
            && (!String.IsNullOrEmpty(x4SqaureText.Text)) && (!String.IsNullOrEmpty(y4SqaureText.Text))
             && (!String.IsNullOrEmpty(AngleT.Text)))
            {
                float x0 = float.Parse(x1SqaureText.Text), y0 = float.Parse(y1SqaureText.Text);
                float x1 = float.Parse(x2SqaureText.Text), y1 = float.Parse(y2SqaureText.Text);
                float x2 = float.Parse(x3SqaureText.Text), y2 = float.Parse(y3SqaureText.Text);
                float x3 = float.Parse(x4SqaureText.Text), y3 = float.Parse(y4SqaureText.Text);
                float angle = float.Parse(AngleT.Text);

                float x0C = x0, y0C = y0;
                float x1C = x1, y1C = y1;
                float x2C = x2, y2C = y2;
                float x3C = x3, y3C = y3;

                //MessageBox.Show(AngleT.Text);
                //transation point

                x0 = (float)RotatePointX(x0C, y0C, angle);
                y0 = (float)RotatePointY(x0C, y0C, angle);
                x1 = (float)RotatePointX(x1C, y1C, angle);
                y1 = (float)RotatePointY(x1C, y1C, angle);
                x2 = (float)RotatePointX(x2C, y2C, angle);
                y2 = (float)RotatePointY(x2C, y2C, angle);
                x3 = (float)RotatePointX(x3C, y3C, angle);
                y3 = (float)RotatePointY(x3C, y3C, angle);

                //Console.WriteLine(x0 + " " + y0 + " " + x1 + " " + y1 + " " + x2 + " " + y2 + " " + x3 + " " + y3);

                lineDDA(x0, y0, x1, y1, Brushes.Salmon);
                lineDDA(x1, y1, x2, y2, Brushes.Salmon);
                lineDDA(x2, y2, x3, y3, Brushes.Salmon);
                lineDDA(x3, y3, x0, y0, Brushes.Salmon);
            }
        }
        private double RotatePointX(double px, double py, double angle)
        {
            angle = angle * (Math.PI / 180);
            double cosTheta = Math.Cos(angle);
            double sinTheta = Math.Sin(angle);
            Console.WriteLine("X " + cosTheta + '\n');
            double PointX = ((cosTheta * px) - (sinTheta * py));
            return PointX;

        }
        private double RotatePointY(double px, double py, double angle)
        {
            angle = angle * (Math.PI / 180);
            double cosTheta = Math.Cos(angle);
            double sinTheta = Math.Sin(angle);
            //Console.WriteLine("Y " + cosTheta + '\n');
            //Console.WriteLine(Math.Sin(angle));
            double PointY = ((sinTheta * px) + (cosTheta * py));
            return PointY;
        }
    }
}