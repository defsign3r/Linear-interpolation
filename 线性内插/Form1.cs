using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 线性内插
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;
        Point[] p = new Point[3];
        Point3D[] point = new Point3D[4];
        Point3D []point_neicha = new Point3D[5];
        int m = 0;
        Random rand = new Random();
        private Pen pen = new Pen(Color.Red, 2);
        private Brush brush = Brushes.Black;
        int[] x = new int[4];
        int[] y = new int[4];
        int[] z = new int[4];
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
            listBox2.Items.Clear();
            for (int i = 0; i < 3; i++)
            {
                x[i] = rand.Next(50,350);
                y[i] = rand.Next(50, 350);
                z[i] = rand.Next(10, 20);
                g = this.CreateGraphics();
            }
            for (int i = 0; i < 3; i++)
            {
                point[i] = new Point3D(x[i], y[i], z[i]);
                g.FillEllipse(brush, point[i].X, point[i].Y, 5, 5);
            }
            for (int i = 0; i < 3; i++)
            {
                p[i].X = point[i].X;
                p[i].Y = point[i].Y;
            }
            g.DrawLine(pen, p[0], p[1]);
            g.DrawLine(pen, p[1], p[2]);
            g.DrawLine(pen, p[2], p[0]);
        }

        private void Form1_MouseDown_1(object sender, MouseEventArgs e)
        {
            g.FillEllipse(brush, e.X, e.Y, 5, 5);
            point[3] = new Point3D(e.X ,e.Y ,z[3]);
            for (int i = 0; i < 3; i++)
            {
                point_neicha[0] = new Point3D(x[i], y[i], z[i]);
                point_neicha[1] = new Point3D(x[i], y[i], z[i]);
                point_neicha[2] = new Point3D(x[i], y[i], z[i]);
                if ((point[0].Y - point[3].Y) * (point[1].Y - point[3].Y) > 0)
                {
                    point_neicha[0] = new Point3D(x[2], y[2], z[2]);
                    point_neicha[1] = new Point3D(x[0], y[0], z[0]);
                    point_neicha[2] = new Point3D(x[1], y[1], z[1]);
                }
                if ((point[1].Y - point[3].Y) * (point[2].Y - point[3].Y) > 0)
                {
                    point_neicha[0] = new Point3D(x[0], y[0], z[0]);
                    point_neicha[1] = new Point3D(x[1], y[1], z[1]);
                    point_neicha[2] = new Point3D(x[2], y[2], z[2]);
                }
                if ((point[0].Y - point[3].Y) * (point[2].Y - point[3].Y) > 0)
                {
                    point_neicha[0] = new Point3D(x[1], y[1], z[1]);
                    point_neicha[1] = new Point3D(x[0], y[0], z[0]);
                    point_neicha[2] = new Point3D(x[2], y[2], z[2]);
                }
            }
            point_neicha[3] = new Point3D(m, point[3].Y, m);
            point_neicha[4] = new Point3D(m, point[3].Y, m);
            point_neicha[3].X = (int)(point_neicha[0].X + (point_neicha[3].Y - point_neicha[0].Y) * ((double)(point_neicha[1].X - point_neicha[0].X) / (double)(point_neicha[1].Y - point_neicha[0].Y)));
            point_neicha[4].X = (int)(point_neicha[0].X + (point_neicha[4].Y - point_neicha[0].Y) * ((double)(point_neicha[2].X - point_neicha[0].X) / (double)(point_neicha[2].Y - point_neicha[0].Y)));
            point_neicha[3].Z = (int)(point_neicha[0].Z + (point_neicha[1].Z - point_neicha[0].Z) * ((double)(point_neicha[3].X - point_neicha[0].X) / (double)(point_neicha[1].X - point_neicha[0].X)));
            point_neicha[4].Z = (int)(point_neicha[0].Z + (point_neicha[2].Z - point_neicha[0].Z) * ((double)(point_neicha[4].X - point_neicha[0].X) / (double)(point_neicha[2].X - point_neicha[0].X)));
            point[3].Z = (int)(point_neicha[3].Z + (point_neicha[4].Z - point_neicha[3].Z) * ((double)(point[3].X - point_neicha[3].X) / (double)(point_neicha[4].X - point_neicha[3].X)));
            listBox2.Items.Add(point[3].Z);
        }
    }
}
