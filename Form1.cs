using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw
{
    public partial class FormMain : Form
    {
        Graphics g;
        Pen p;
        int index_of_vertex = 0, color_code = 0, dX = 20, dY = 20;
        bool sfn01 = true;
        int settings = 1; //1 - Drawing, 2 - Select (Có thể Edit, Xóa, ...)
        PointF[] vertex = new PointF[10000];
        PointF[] og_vertex = new PointF[10000];
        string[] colors = {"Black"};//"Aqua", "Blue", "Red", "Brown", "BlueViolet", "Chartreuse", "DarkOrange", "DeepPink", "Green", "GreenYellow", "IndianRed", "Indigo", "LightCoral" } ;
        Size FormMain_size;
        ValueTuple<Point,int> selectedPoint;

        public FormMain()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            color_code = 0;
            p = new Pen(Color.FromName(colors[color_code]), 1f);
            FormMain_size = this.Size;
        }

        public void changeColor()
        {
            color_code = (color_code + 1) % 1;
            p = new Pen(Color.FromName(colors[color_code]), 2f);
        }
        
        public void Build_Grid() //Xây dựng hệ trục
        {
            int i = 0;
            p = new Pen(Color.Black, 1f);
            while (i <= this.Size.Width / (2*dX))
            {
                Point sfn01 = new Point(i*dX + this.Size.Width / 2, 0);
                Point sfn02 = new Point(i*dX + this.Size.Width / 2, this.Size.Height);
                Point sfn03 = new Point(-i*dX + this.Size.Width / 2, 0);
                Point sfn04 = new Point(-i*dX + this.Size.Width / 2, this.Size.Height);
                g.DrawLine(p, sfn01, sfn02);
                g.DrawLine(p, sfn03, sfn04);
                i++;
            }
            i = 0;
            while (i <= this.Size.Height / (2*dY))
            {
                Point sfn01 = new Point(0, i*dY + this.Size.Height / 2);
                Point sfn02 = new Point(this.Size.Width, i*dY + this.Size.Height / 2);
                Point sfn03 = new Point(0, -i*dY + this.Size.Height / 2);
                Point sfn04 = new Point(this.Size.Width, -i*dY + this.Size.Height / 2);
                g.DrawLine(p, sfn01, sfn02);
                g.DrawLine(p, sfn03, sfn04);
                i++;
            }
            p = new Pen(Color.Red, 1f);
            g.DrawLine(p, new Point(0, this.Size.Height/2), new Point(this.Size.Width, this.Size.Height / 2));
            g.DrawLine(p, new Point(this.Size.Width / 2, 0), new Point(this.Size.Width / 2, this.Size.Height));
            p = new Pen(Color.Black, 2f);
            sfn01 = false;
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)//Delete
            {

            }
            if (e.KeyCode == Keys.Enter) //Stop Select and Confirm Move
            {

            }
            if (e.KeyCode == Keys.Shift) //Move
            {

            }
        }

        private void PaintMode(object sender, MouseEventArgs e)
        {
            settings = 1;
        }

        private void SelectMode(object sender, MouseEventArgs e)
        {
            settings = 2;
        }
        private void FormMain_SizeChanged(object sender, EventArgs e) //Khi Form đổi kích thước, các phần tử bị đổi theo
        {
            Size sfn00 = this.Size;
            vertex = og_vertex;
            if (index_of_vertex == 0) return;
            //this.Left += (this.ClientSize.Width - this.Width) / 2;
            //this.Top += (this.ClientSize.Height - this.Height) / 2;
            g.Clear(this.BackColor);
            //Build_Grid();
            color_code = 0;
            p = new Pen(Color.FromName(colors[color_code]), 2f);
            for (int i = 0; i < index_of_vertex; ++i)
            {
                vertex[i].X += (vertex[i].X / FormMain_size.Width)*(sfn00.Width - FormMain_size.Width);
                vertex[i].Y += (vertex[i].Y / FormMain_size.Height)*(sfn00.Height - FormMain_size.Height);
                if ((i + 1) % 3 != 1)
                {
                    g.DrawLine(p, vertex[i], vertex[i - 1]);
                }

                if ((i + 1) % 3 == 0)
                {
                    g.DrawLine(p, vertex[i], vertex[i - 2]);
                    changeColor();
                }
            }
            FormMain_size = sfn00;
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (sfn01) Build_Grid();
            Point sfn02 = this.PointToClient(MousePosition);
            switch (settings)
            {
                case 1:
                    {
                        p = new Pen(Color.Black, 2f);
                        sfn02 = new Point(dX * (sfn02.X / dX) + (this.Size.Width / 2) % dX, dY * (sfn02.Y / dY) + (this.Size.Height / 2) % dY);
                        vertex[index_of_vertex] = sfn02;
                        og_vertex[index_of_vertex] = vertex[index_of_vertex];
                        index_of_vertex++;

                        if (index_of_vertex % 3 != 1)
                        {
                            g.DrawLine(p, vertex[index_of_vertex - 1], vertex[index_of_vertex - 2]);
                        }

                        if (index_of_vertex % 3 == 0)
                        {
                            g.DrawLine(p, vertex[index_of_vertex - 1], vertex[index_of_vertex - 3]);
                            //changeColor();
                        }
                        break;
                    }
                case 2:
                    {
                        float mind = 1000000;
                        selectedPoint.Item1 = new Point(0,0);
                        selectedPoint.Item2 = 0;
                        for (int j = 0; j < index_of_vertex; ++j)
                        {
                            PointF i = vertex[j];
                            float sfn03 = (i.X - sfn02.X) * (i.X - sfn02.X) + (i.Y - sfn02.Y) * (i.Y - sfn02.Y);
                            if (sfn03 < mind)
                            {
                                mind = sfn03;
                                selectedPoint.Item1 = new Point((int)i.X, (int)i.Y);
                                selectedPoint.Item2 = j;
                            }
                        }
                        g.Clear(this.BackColor);
                        p = new Pen(Color.Black, 2f);
                        Build_Grid();
                        for (int i = 0; i < index_of_vertex; ++i)
                        {
                            if ((i + 1) % 3 != 1)
                            {
                                g.DrawLine(p, vertex[i], vertex[i - 1]);
                            }

                            if ((i + 1) % 3 == 0)
                            {
                                g.DrawLine(p, vertex[i], vertex[i - 2]);
                            }
                        }
                        p = new Pen(Color.Green, 3f);
                        g.DrawPie(p, new Rectangle(new Point(selectedPoint.Item1.X-3,selectedPoint.Item1.Y-3), new Size(6, 6)), 0, 360);
                        break;
                    }
            }
        }
    }
}
