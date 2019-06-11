using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessMonitor
{
    public partial class Form1 : Form
    {
        private float _X;
        private float _cpuY;
        private float _proY;
        private float _memY;
        private int _graphHeight;
        private int _graphWidth;
        private int _line1Y;
        private int _line2Y;
        private int _line3Y;
        private bool remove = true;
        private bool draw = false;

        static Color colorBack = Color.FromName("ControlLight");

        Random rndcpu = new Random(123);
        Random rndpro = new Random(456);
        Random rndmem = new Random(789);

        Graphics _graphics;

        Pen penCPU = new Pen(Color.Red);
        Pen penProc = new Pen(Color.Green);
        Pen penMem = new Pen(Color.Blue);
        Pen penFore = new Pen(Color.Black);
        Pen penBack = new Pen(colorBack);
        Pen penBack2 = new Pen(colorBack, 20);
        Pen penLine = new Pen(Color.Black);

        const Single INCREMENT = 3;

        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 50;
            toolTipTrackbar.SetToolTip(trackBar1, timer1.Interval.ToString() + "ms");

            _graphHeight = panelGraph.Height;
            _graphWidth = panelGraph.Width;
            _line1Y = _graphHeight / 4 * 1;
            _line2Y = _graphHeight / 4 * 2;
            _line3Y = _graphHeight / 4 * 3;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text == "&Start")
            {
                buttonStart.Text = "&Stop";
                timer1.Enabled = true;
            }
            else
            {
                buttonStart.Text = "&Start";
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int rndHeight = _graphHeight / 4;
            int rndMin = -(rndHeight / 2);
            int rndMax = (rndHeight / 2) + 1;

            Single cpu = rndcpu.Next(rndMin, rndMax);
            Single pro = rndpro.Next(rndMin, rndMax);
            Single mem = rndmem.Next(rndMin, rndMax);

            Zeichnen(cpu, pro, mem);

            labelCPU.Text = cpu.ToString();
            labelProcesse.Text = pro.ToString();
            labelSpeicher.Text = mem.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int value = (int)trackBar1.Value;
            timer1.Interval = value * 50;
            toolTipTrackbar.SetToolTip(trackBar1, timer1.Interval.ToString() + "ms");
        }

        private void Zeichnen(float cpuWert, float proWert, float memWert)
        {
            float cpuY;
            float proY;
            float memY;

            // positionierer übermalen
            _graphics.DrawLine(penBack, _X, 0, _X, _graphHeight);
            _graphics.DrawLine(penBack2, _X + 8, 0, _X + 8, _graphHeight);

            DrawLines(draw);

            cpuY = _line1Y - (cpuWert) - 1;
            _graphics.DrawLine(penCPU, _X - INCREMENT, _cpuY, _X, cpuY);
            _cpuY = cpuY;

            proY = _line2Y - (proWert) - 1;
            _graphics.DrawLine(penProc, _X - INCREMENT, _proY, _X, proY);
            _proY = proY;

            memY = _line3Y - (memWert) - 1;
            _graphics.DrawLine(penMem, _X - INCREMENT, _memY, _X, memY);
            _memY = memY;

            _X += INCREMENT;

            if (_X > panelGraph.Width)
            {
                _X = 0;
                DrawLines(draw);
            }

            // positionierer neu zeichnen
            _graphics.DrawLine(penFore, _X, 0, _X, _graphHeight);

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            _graphHeight = panelGraph.Height;
            _graphWidth = panelGraph.Width;

            _line1Y = _graphHeight / 4 * 1;
            _line2Y = _graphHeight / 4 * 2;
            _line3Y = _graphHeight / 4 * 3;

            DrawLines(remove);
            DrawLines(draw);

        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            DrawLines(remove);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DrawLines(draw);
        }

        private void DrawLines(bool act)
        {
            if (act == remove)
            {
                _graphics.DrawLine(penBack, 0, _line1Y, _graphWidth, _line1Y);
                _graphics.DrawLine(penBack, 0, _line2Y, _graphWidth, _line2Y);
                _graphics.DrawLine(penBack, 0, _line3Y, _graphWidth, _line3Y);
            }
            else
            {
                _graphics = panelGraph.CreateGraphics();
                _graphics.DrawLine(penLine, 0, _line1Y, panelGraph.Width, _line1Y);
                _graphics.DrawLine(penLine, 0, _line2Y, panelGraph.Width, _line2Y);
                _graphics.DrawLine(penLine, 0, _line3Y, panelGraph.Width, _line3Y);
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            _graphics.Clear(colorBack);

            penBack2 = new Pen(colorBack, 20);
            penFore = new Pen(Color.Black);

            // zeichnet ein quadrat
            //Point[] points =
            //{
            //     new Point(10,  10),
            //     new Point(10, 50),
            //     new Point(10,  50),
            //     new Point(50, 50),
            //     new Point(50, 10),
            //     new Point(10, 10)
            //};

            // using drawlines instate of drawline
            _graphics.DrawLines(penFore, new Point[] { new Point(0, _line1Y), new Point(panelGraph.Width, _line1Y) });
            _graphics.DrawLines(penFore, new Point[] { new Point(0, _line2Y), new Point(panelGraph.Width, _line2Y) });
            _graphics.DrawLines(penFore, new Point[] { new Point(0, _line3Y), new Point(panelGraph.Width, _line3Y) });

        }
    }
}
