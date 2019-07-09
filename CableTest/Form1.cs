using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Numerics;

namespace CableTest
{
    public partial class Form1 : Form
    {

       
        const int _sFactor = 40;

       
        CableSolver _cSolver;

        public Form1()
        {
            InitializeComponent();
           
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
           // g.PageUnit = GraphicsUnit.Pixel;
           

            foreach (Circle _circle in _cSolver._xCircles)
            {
                Pen MyPen = new Pen(_circle._cColor, 1);
                g.DrawEllipse(MyPen, _circle._cPosition.X - (((float)_circle._cRadius) / 2), _circle._cPosition.Y - (((float)_circle._cRadius) / 2), (float)_circle._cRadius, (float)_circle._cRadius);
               
            }

            g.DrawEllipse(new Pen(Color.Red, 2), _cSolver._mainCircle._cPosition.X, _cSolver._mainCircle._cPosition.Y, (float)_cSolver._mainCircle._cRadius, (float)_cSolver._mainCircle._cRadius);

            

        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            
            _cSolver = new CableSolver(new Vector2(500, 300), timer1, button3);

            if (System.IO.File.Exists("vstup.txt"))
            {
                var fileStream = new FileStream("vstup.txt", FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.ASCII))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line[0] != '#')
                        {
                           try
                            {
                                line = line.Replace(".", ",");
                                _cSolver.CreateCircle((float)(Convert.ToDouble(line)*_sFactor));
                            }
                            catch
                            {
                                MessageBox.Show("Parse file fail !!");
                                Close();
                            }
                        }
                    }

                  
                }
            }
            else
            {
                MessageBox.Show("No file to load!! (vstup.txt)");
                Close();
            }

            _cSolver.SortCircles();
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            _cSolver.MoveCircles();
            Invalidate();
        }

     

        private void Button3_Click_1(object sender, EventArgs e)
        {
            _cSolver._mainCircle._cPosition.X -= 0.05f;
            _cSolver._mainCircle._cPosition.Y -= 0.05f;

            _cSolver._mainCircle._cRadius += 10;

            this.Text = (_cSolver._mainCircle._cRadius / _sFactor).ToString();

            Invalidate();
        }
    }
}
