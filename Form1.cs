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
        int index_of_vertex = 0, color_code = 0;
        float dX = 40, dY = 40;
        bool sfn01 = true, startMoving = false, controlAddToSelection = false;
        int settings = 1; //1 - Drawing, 2 - Select (Có thể Edit, Xóa, ...), 3 - Move Nếu đã có điểm đc select thì 
        PointF[] vertex = new PointF[10000], og_vertex = new PointF[10000];
        PointF position_MouseDown = new PointF(0, 0), center;
        bool[,] connections = new bool[10000,3];
        string[] colors = { "Black" };// "Blue","DarkGoldenrod", "Brown", "BlueViolet", "DarkOrange", "DarkRed", "ForestGreen", "IndianRed", "Indigo", "LightCoral", "LightSlateGray", "Maroon", "MidnightBlue",  };
        Size FormMain_size;
        ValueTuple<Point[], int[], int, int> selectedPoints; //Points, Positions, NumberOfPoints ContainsOnlyEdge, ContainsEdgeAndPoints
        public FormMain()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            g = this.CreateGraphics();
            color_code = 0;
            p = new Pen(Color.FromName(colors[color_code]), 1f);
            FormMain_size = this.Size;
            xCord.Visible = false;
            yCord.Visible = false;
            Area.Visible = false;
            selectedPoints.Item1 = new Point[20000]; selectedPoints.Item2 = new int[20000]; selectedPoints.Item3 = 0; selectedPoints.Item4 = 0;
            center = new PointF(this.Size.Width/2, this.Size.Height/2);
            this.MouseWheel += this.FormMain_Scroll;
            Cursor.Current = Cursors.Cross;

        }

        public void DrawStuff()
        {
            for (int i = 0; i < index_of_vertex; ++i)
            {
                if (connections[i, i % 3])
                {
                    p = new Pen(Color.Green, 2f);
                    g.DrawPie(p, new Rectangle(new Point((int)vertex[i].X - 2, (int)vertex[i].Y - 2), new Size(4, 4)), 0, 360);
                }
                p = new Pen(Color.Black, 2f);
                if ((i + 1) % 3 != 1)
                {
                    if (connections[i, (i - 1) % 3]) g.DrawLine(p, vertex[i], vertex[i - 1]);
                }

                if ((i + 1) % 3 == 0)
                {
                    if (connections[i, (i - 2) % 3]) g.DrawLine(p, vertex[i], vertex[i - 2]);
                    changeColor();
                }
            }            
        }

        public void changeColor()
        {
            color_code += 1;
            if (color_code >= colors.Count()) color_code = 0;
            p = new Pen(Color.FromName(colors[color_code]), 2f);
        }

        public void Build_Grid() //Xây dựng hệ trục
        {
            int i = 0;
            int dX1 = (int)dX, dY1 = (int)dY;
            p = new Pen(Color.Gray, 1f);
            while (i <= 10*this.Size.Width / (dX1))
            {
                PointF sfn01 = new PointF(i * dX1 + center.X, 0);
                PointF sfn02 = new PointF(i * dX1 + center.X, this.Size.Height);
                PointF sfn03 = new PointF(-i * dX1 + center.X, 0);
                PointF sfn04 = new PointF(-i * dX1 + center.X, this.Size.Height);
                g.DrawLine(p, sfn01, sfn02);
                g.DrawLine(p, sfn03, sfn04);
                i++;
            }
            i = 0;
            while (i <= 10*this.Size.Height / (dY1))
            {
                PointF sfn01 = new PointF(0, i * dY1 + center.Y);
                PointF sfn02 = new PointF(this.Size.Width, i * dY1 + center.Y);
                PointF sfn03 = new PointF(0, -i * dY1 + center.Y);
                PointF sfn04 = new PointF(this.Size.Width, -i * dY1 + center.Y);
                g.DrawLine(p, sfn01, sfn02);
                g.DrawLine(p, sfn03, sfn04);
                i++;
            }
            p = new Pen(Color.Red, 2f);
            g.DrawLine(p, new PointF(0, center.Y), new PointF(this.Size.Width, center.Y));
            g.DrawLine(p, new PointF(center.X, 0), new PointF(center.X, this.Size.Height));
            p = new Pen(Color.Black, 2f);
            sfn01 = false;
        }  

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)//Delete
            {
                switch (selectedPoints.Item4)
                {
                    case 0: //Contains Only Nodes
                        {
                            Console.WriteLine(selectedPoints.Item3);
                            for (int i = 0; i < selectedPoints.Item3; i++)
                            {

                                int j = selectedPoints.Item2[i];
                                if (connections[j, j % 3])
                                { 
                                    connections[j, j % 3] = false;
                                    connections[(j + 1) % 3 + 3 * (j / 3), j % 3] = false;
                                    connections[(j + 2) % 3 + 3 * (j / 3), j % 3] = false;
                                    connections[j, (j + 1) % 3] = false;
                                    connections[j, (j + 2) % 3] = false;
                                }
                            }
                            break;
                        }
                    case 1: //Contains Only Edges imma remove this
                        {
                            int i = 0;
                            while (i < selectedPoints.Item3)
                            {
                                int j = selectedPoints.Item2[i], j1 = selectedPoints.Item2[i + 1];
                                if (connections[j, j1 % 3])
                                {
                                    connections[j, j1 % 3] = false;
                                    connections[j1, j % 3] = false;
                                }
                                i += 2;
                            }
                            break;
                        }
                }
                g.Clear(this.BackColor); Build_Grid(); DrawStuff();

            }
            //if (e.KeyCode == Keys.Enter) //Stop Select and Confirm Move
            {

            }
            if (e.KeyData == Keys.CapsLock)
            {
                controlAddToSelection = true;
            }
            /*if (e.KeyData == Keys.Space) //Transform
            {
                if (selectedPoints.Item3 == 0 || selectedPoints.Item3 > 1) return;
                startMoving = !startMoving;
                if (!startMoving)
                {
                    PointF sfn02 = this.PointToClient(MousePosition);
                    sfn02 = new PointF((int)dX * (int)Math.Round(sfn02.X / dX) + ((center.X) % (int)dX), (int)dY * (int)Math.Round(sfn02.Y / dY) + ((center.Y) % (int)dY));
                    vertex[selectedPoints.Item2[0]] = sfn02;
                    selectedPoints.Item1[0] = new Point((int)sfn02.X, (int)sfn02.Y);
                    g.Clear(this.BackColor);
                    Build_Grid();
                    DrawStuff();
                    p = new Pen(Color.Green, 2f);
                    g.DrawPie(p, new Rectangle(new Point(selectedPoints.Item1[0].X - 3, selectedPoints.Item1[0].Y - 3), new Size(6, 6)), 0, 360);
                }
            }*/
        }

        private void PaintMode(object sender, MouseEventArgs e)
        {
            settings = 1;
            selectedPoints.Item1 = new Point[20000]; selectedPoints.Item2 = new int[20000]; selectedPoints.Item3 = 0; selectedPoints.Item4 = 0;
            xCord.Visible = false;
            yCord.Visible = false;
            Area.Visible = false;
            button1.BackColor = Color.Silver;
            button2.BackColor = Color.WhiteSmoke;
            button4.BackColor = Color.WhiteSmoke;

            Point sfn02 = this.PointToClient(MousePosition);
            float mind = 1000000;
            selectedPoints.Item1[0] = new Point(0, 0);
            selectedPoints.Item2[0] = 0;
            for (int j = 0; j < index_of_vertex; ++j)
            {
                PointF i = vertex[j];
                float sfn03 = (i.X - sfn02.X) * (i.X - sfn02.X) + (i.Y - sfn02.Y) * (i.Y - sfn02.Y);
                if (sfn03 < mind)
                {
                    mind = sfn03;
                    selectedPoints.Item1[0] = new Point((int)i.X, (int)i.Y);
                    selectedPoints.Item2[0] = j;
                }
            }
            g.Clear(this.BackColor);
            color_code = 0;
            Build_Grid();
            p = new Pen(Color.Black, 2f);
            DrawStuff();
        }

        private void FormMain_Scroll(object sender, MouseEventArgs e)
        {
            color_code = 0;
            float dX1 = dX, dY1 = dY; 
            dX = Math.Min(Math.Max(10, dX + (float)(0.05 * e.Delta)), 40);
            dY = Math.Min(Math.Max(10, dY + (float)(0.05 * e.Delta)), 40);
            p = new Pen(Color.Black, 2f);
            if (dX1 != dX)
            {
                for (int i = 0; i < index_of_vertex; ++i)
                {
                    vertex[i].X += (float)(0.05 * e.Delta) * ((vertex[i].X - center.X)/dX1);
                    vertex[i].Y += (float)(0.05 * e.Delta) * ((vertex[i].Y - center.Y)/dY1);
                }
            }
            g.Clear(this.BackColor);
            Build_Grid();
            DrawStuff();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedPoints.Item1 = new Point[20000]; selectedPoints.Item2 = new int[20000]; selectedPoints.Item3 = 0; selectedPoints.Item4 = 0;
            MessageBox.Show(
        "CLICK - chọn điểm" + Environment.NewLine +
        "CAPS LOCK - bật/tắt chọn chồng chất" + Environment.NewLine +
        "MOUSE DRAG - kéo chuột tạo vùng các điểm chọn" + Environment.NewLine +
        "DELETE - xóa điểm" + Environment.NewLine +
        "SCROLL WHEEL - phóng to/thu nhỏ" + Environment.NewLine +
        "MOVE - dịch chuyển điểm được chọn hoặc hệ trục",
        "Shortcut",
        MessageBoxButtons.OK,
        //MessageBoxIcon.Warning // for Warning  
        //MessageBoxIcon.Error // for Error 
        MessageBoxIcon.Information  // for Information
        //MessageBoxIcon.Question // for Question
        );
        }

        private void Move_Click(object sender, EventArgs e)
        {
            settings = 3;
            button1.BackColor = Color.WhiteSmoke;
            button2.BackColor = Color.WhiteSmoke;
            button4.BackColor = Color.Silver;
        }

        private void FormMain_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.LControlKey)
            {
                controlAddToSelection = false;
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            PointF sfn03 = this.PointToClient(MousePosition), sfn04 = position_MouseDown;
            if (sfn03 == position_MouseDown) return;
            sfn03 = new PointF(dX * (int)Math.Round(sfn03.X / dX) + ((center.X) % dX), dY * (int)Math.Round(sfn03.Y / dY) + ((center.Y) % dY));
            sfn04 = new PointF(dX * (int)Math.Round(sfn04.X / dX) + ((center.X) % dX), dY * (int)Math.Round(sfn04.Y / dY) + ((center.Y) % dY));
            switch (settings)
            {
                case 2: //Mass-select
                    {
                        int sfn02 = 0;
                        if (controlAddToSelection) sfn02 = selectedPoints.Item3;
                        else
                        {
                            selectedPoints.Item1 = new Point[20000]; selectedPoints.Item2 = new int[20000]; selectedPoints.Item3 = 0; selectedPoints.Item4 = 0;
                        }
                        for (int i = 0; i < index_of_vertex; i++)
                        {
                            if ((vertex[i].X - sfn03.X) * (vertex[i].X - sfn04.X) <= 0 && connections[i, i % 3])
                            {
                                selectedPoints.Item1[sfn02] = new Point((int)vertex[i].X, (int)vertex[i].Y);
                                selectedPoints.Item2[sfn02] = i;
                                selectedPoints.Item3++;
                                sfn02++;
                            }
                        }
                        p = new Pen(Color.Blue, 2f);
                        for (int i = 0; i < sfn02; i++)
                        {
                            if (connections[selectedPoints.Item2[i], selectedPoints.Item2[i] % 3]) g.DrawPie(p, new Rectangle(new Point(selectedPoints.Item1[i].X - 4, selectedPoints.Item1[i].Y - 4), new Size(8, 8)), 0, 360);
                        }
                        break;
                    }
                case 3: //Move camera or Move objects
                    {
                        bool isThereRealSelection = false;
                        if (selectedPoints.Item3 > 0)
                        {
                            for (int i = 0; i < selectedPoints.Item3; i++)
                            {
                                if (connections[selectedPoints.Item2[i], selectedPoints.Item2[i] % 3])
                                {
                                    isThereRealSelection = true;
                                    vertex[selectedPoints.Item2[i]] = new PointF(vertex[selectedPoints.Item2[i]].X + sfn03.X - sfn04.X, vertex[selectedPoints.Item2[i]].Y + sfn03.Y - sfn04.Y);
                                    selectedPoints.Item1[i] = new Point((int)(selectedPoints.Item1[i].X + sfn03.X - sfn04.X), (int)(selectedPoints.Item1[i].Y + sfn03.Y - sfn04.Y));
                                }
                            }
                            g.Clear(this.BackColor);
                            Build_Grid();
                            DrawStuff();
                            p = new Pen(Color.Blue, 2f);
                            for (int i = 0; i < selectedPoints.Item3; i++)
                            {
                                if (connections[selectedPoints.Item2[i], selectedPoints.Item2[i] % 3]) g.DrawPie(p, new Rectangle(new Point(selectedPoints.Item1[i].X - 4, selectedPoints.Item1[i].Y - 4), new Size(8, 8)), 0, 360);
                            }
                        }
                        if (!isThereRealSelection || selectedPoints.Item3 == 0)
                        {
                            center = new PointF(center.X + sfn03.X - sfn04.X, center.Y + sfn03.Y - sfn04.Y);
                            for (int i = 0; i < index_of_vertex; i++)
                            {
                                vertex[i] = new PointF(vertex[i].X + sfn03.X - sfn04.X, vertex[i].Y + sfn03.Y - sfn04.Y);
                            }
                            g.Clear(this.BackColor);
                            Build_Grid();
                            DrawStuff();
                        }
                        break;
                    }
            }
        }

        private void SelectMode(object sender, MouseEventArgs e)
        {
            settings = 2;
            xCord.Visible = true;
            yCord.Visible = true;
            Area.Visible = true;
            button2.BackColor = Color.Silver;
            button1.BackColor = Color.WhiteSmoke;
            button4.BackColor = Color.WhiteSmoke;
        }
        private void FormMain_SizeChanged(object sender, EventArgs e) //Khi Form đổi kích thước, các phần tử bị đổi theo
        {
            Size sfn00 = this.Size;
            vertex = og_vertex;
            if (index_of_vertex == 0) return;
            //this.Left += (this.ClientSize.Width - this.Width) / 2;
            //this.Top += (this.ClientSize.Height - this.Height) / 2;
            g.Clear(this.BackColor);
            Build_Grid();
            color_code = 0;
            p = new Pen(Color.Black, 2f);
            for (int i = 0; i < index_of_vertex; ++i)
            {
                vertex[i].X += (vertex[i].X / FormMain_size.Width)*(sfn00.Width - FormMain_size.Width);
                vertex[i].Y += (vertex[i].Y / FormMain_size.Height)*(sfn00.Height - FormMain_size.Height);
            }
            DrawStuff();
            FormMain_size = sfn00;
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            position_MouseDown = this.PointToClient(MousePosition); ;
            if (sfn01) Build_Grid();
            if (startMoving)
            {
                PointF sfn03 = position_MouseDown;
                sfn03 = new PointF((int)dX * (int)Math.Round(sfn03.X / dX) + ((center.X) % (int)dX), (int)dY * (int)Math.Round(sfn03.Y / dY) + ((center.Y) % (int)dY));
                vertex[selectedPoints.Item2[0]] = sfn03;
                g.Clear(this.BackColor);
                Build_Grid();
                DrawStuff();
                startMoving = false;
                return;
            }
            PointF sfn02 = position_MouseDown;
            switch (settings)
            {
                case 1:
                    {
                        selectedPoints.Item1 = new Point[20000]; selectedPoints.Item2 = new int[20000]; selectedPoints.Item3 = 0; selectedPoints.Item4 = 0;
                        sfn02 = new PointF((int)dX * (int)Math.Round(sfn02.X/dX) + ((center.X) % (int)dX),(int)dY * (int)Math.Round(sfn02.Y / dY) + ((center.Y)% (int)dY));
                        vertex[index_of_vertex] = sfn02;
                        connections[index_of_vertex, index_of_vertex % 3] = true;
                        if(index_of_vertex % 3 == 1)
                        {
                            connections[index_of_vertex,(index_of_vertex - 1) % 3] = true;
                            connections[index_of_vertex-1,index_of_vertex % 3] = true;
                        }
                        if (index_of_vertex % 3 == 2)
                        {
                            connections[index_of_vertex,(index_of_vertex - 2) % 3] = true;
                            connections[index_of_vertex - 2,index_of_vertex % 3] = true;
                            connections[index_of_vertex,(index_of_vertex - 1) % 3] = true;
                            connections[index_of_vertex - 1,index_of_vertex % 3] = true;
                        }
                        og_vertex[index_of_vertex] = vertex[index_of_vertex];
                        index_of_vertex++;
                        DrawStuff();   
                        break;
                    }
                case 2:
                    {
                        float mind = 1000000;
                        int sfn04 = 0;
                        if (controlAddToSelection)
                        {
                            sfn04 = selectedPoints.Item3;
                            selectedPoints.Item3++;
                        }
                        else
                        {
                            selectedPoints.Item3 = 1;
                        }
                        for (int j = 0; j < index_of_vertex; ++j)
                        {
                            if (connections[j,j%3])
                            {
                                PointF i = vertex[j];
                                float sfn03 = (i.X - sfn02.X) * (i.X - sfn02.X) + (i.Y - sfn02.Y) * (i.Y - sfn02.Y);
                                if (sfn03 < mind)
                                {
                                    mind = sfn03;
                                    selectedPoints.Item1[sfn04] = new Point((int)i.X, (int)i.Y);
                                    selectedPoints.Item2[sfn04] = j;
                                }
                            }
                        }
                        g.Clear(this.BackColor);
                        color_code = 0;
                        p = new Pen(Color.Black, 2f);
                        Build_Grid();
                        DrawStuff();
                        p = new Pen(Color.Blue, 2f);
                        for (int i = 0; i < selectedPoints.Item3; i++)
                        {
                            if (connections[selectedPoints.Item2[i], selectedPoints.Item2[i]%3]) g.DrawPie(p, new Rectangle(new Point(selectedPoints.Item1[i].X - 4, selectedPoints.Item1[i].Y - 4), new Size(8, 8)), 0, 360);
                        }
                        
                        xCord.Text = "x: " + ((selectedPoints.Item1[0].X - center.X) / dX).ToString();
                        yCord.Text = "y: " + ((selectedPoints.Item1[0].Y - center.Y) / dY).ToString();
                        int id = selectedPoints.Item2[0];
                        switch (id % 3)
                        {
                            case 0:
                                {
                                    float x1 = (vertex[id].X - center.X) / dX, y1 = (vertex[id].Y - center.Y) / dY;
                                    float x2 = (vertex[id + 1].X - center.X) / dX, y2 = (vertex[id + 1].Y - center.Y) / dY;
                                    float x3 = (vertex[id + 2].X - center.X) / dX, y3 = (vertex[id + 2].Y - center.Y) / dY;
                                    //Area.Text = "Area: " + (((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)) / 2).ToString();
                                    Area.Text = "Area: " + (Math.Abs(x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2))/2).ToString();
                                    break;
                                }
                            case 1:
                                {
                                    float x1 = (vertex[id - 1].X - center.X) / dX, y1 = (vertex[id - 1].Y - center.Y) / dY;
                                    float x2 = (vertex[id].X - center.X) / dX, y2 = (vertex[id].Y - center.Y) / dY;
                                    float x3 = (vertex[id + 1].X - center.X) / dX, y3 = (vertex[id + 1].Y - center.Y) / dY;
                                    //Area.Text = "Area: " + (((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)) / 2).ToString();
                                    Area.Text = "Area: " + (Math.Abs(x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2).ToString();
                                    break;
                                }
                            case 2:
                                {
                                    float x1 = (vertex[id - 2].X - center.X) / dX, y1 = (vertex[id - 2].Y - center.Y) / dY;
                                    float x2 = (vertex[id - 1].X - center.X) / dX, y2 = (vertex[id - 1].Y - center.Y) / dY;
                                    float x3 = (vertex[id].X - center.X) / dX, y3 = (vertex[id].Y - center.Y) / dY;
                                    //Area.Text = "Area: " + (((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)) / 2).ToString();
                                    Area.Text = "Area: " + (Math.Abs(x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2).ToString();
                                    break;
                                }
                        }


                        break;
                    }
            }
        }
    }
}
