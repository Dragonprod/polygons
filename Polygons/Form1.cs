﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Polygons
{
    public partial class Form1 : Form
    {
        Color col, pen;
        List<Shape> figures = new List<Shape>();
        int _x, _y;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            bool IsMove = false;
            _x = e.X; _y = e.Y;
            foreach (Shape p in figures.ToArray())
            {
                if(p.IsInside(e.X, e.Y))
                {
                    p.FLAG = true;
                    IsMove = true;
                }
                if(e.Button == MouseButtons.Right)
                {
                    if (p.ToRemove(e.X, e.Y)) p.REMOVE = true;
                    if (p.REMOVE) { figures.Remove(p); Refresh(); }
                }
            }
            if (!IsMove && e.Button == MouseButtons.Left)
            {
                if (circleToolStripMenuItem.Checked == true)
                {
                    figures.Add(new Circle(col, pen, e.X, e.Y));
                    Refresh();
                }
                else if (triangleToolStripMenuItem1.Checked == true)
                {
                    figures.Add(new Triangle(col, pen, e.X, e.Y));
                    Refresh();
                }
                else if(squareToolStripMenuItem1.Checked == true)
                {
                    figures.Add(new Rectangl(col, pen, e.X, e.Y));
                    Refresh();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            col = Color.Red;
            pen = Color.Green;
            //if (circleToolStripMenuItem.Checked == true)
            //{
            //    figures.Add(new Circle(col, pen, ClientSize.Width / 2, ClientSize.Height / 2));
            //    Refresh();
            //}
            //else if (triangleToolStripMenuItem1.Checked == true)
            //{
            //    figures.Add(new Triangle(col, pen, ClientSize.Width / 2, ClientSize.Height / 2));
            //    Refresh();
            //}
            //else if (squareToolStripMenuItem1.Checked == true)
            //{
            //    figures.Add(new Rectangl(col, pen, ClientSize.Width / 2, ClientSize.Height / 2));
            //    Refresh();
            //}
            figures.Add(new Circle(col, pen, ClientSize.Width / 2, ClientSize.Height / 2));
            Refresh();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Shape p1 in figures)
            {
                if (p1.FLAG)
                {
                    p1.X += e.X - _x;
                    p1.Y += e.Y - _y;
                    Refresh();
                } 
            }
            _x = e.X;
            _y = e.Y;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Shape p1 in figures)
            {
                p1.FLAG = false;
                p1.REMOVE = false;
            }
        }
        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            triangleToolStripMenuItem1.Checked = false;
            squareToolStripMenuItem1.Checked = false;
        }
        private void triangleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            circleToolStripMenuItem.Checked = false;
            squareToolStripMenuItem1.Checked = false;
        }
        private void squareToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            triangleToolStripMenuItem1.Checked = false;
            circleToolStripMenuItem.Checked = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape p1 in figures)
            {
                p1.Draw(e.Graphics);
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
