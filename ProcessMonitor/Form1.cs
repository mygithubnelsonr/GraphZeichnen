using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessMonitor
{
    public partial class Form1 : Form
    {
        private Single _X;
        private Single _sngY;
        private Single _cpuY;
        private Single _proY;
        private Single _memY;
        private int _graphHeight;
        private int _graphWidth;
        private int _line1Y;
        private int _line2Y;
        private int _line3Y;
        private bool remove = true;
        private bool draw = false;

        Random rndcpu = new Random(123);
        Random rndpro = new Random(456);
        Random rndmem = new Random(789);

        Graphics _graphics;

        Pen penCPU = new Pen(Color.Red);
        Pen penProc = new Pen(Color.Green);
        Pen penMem = new Pen(Color.Blue);
        Pen penFore = new Pen(SystemColors.ActiveCaption);
        Pen penBack = new Pen(SystemColors.ControlLight);
        Pen penLine = new Pen(Color.Black);

        const Single INCREMENT = 5;

        public Form1()
        {
            InitializeComponent();

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
            Single cpu = rndcpu.Next(_graphHeight);
            Single pro = rndpro.Next(_graphHeight);
            Single mem = rndmem.Next(_graphHeight);

            Zeichnen(cpu, pro, mem);

            labelCPU.Text = cpu.ToString();
            labelProcesse.Text = pro.ToString();
            labelSpeicher.Text = mem.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int value = (int)trackBar1.Value;
            timer1.Interval = value * 100;
            toolTipTrackbar.SetToolTip(trackBar1, timer1.Interval.ToString() + "ms");
        }

        private void Zeichnen(Single cpuWert, Single proWert, Single memWert)
        {
            //Graphics graphics = panel1.CreateGraphics();
            Single height = _graphHeight;
            Single cpuY;
            Single proY;
            Single memY;

            _graphics.DrawLine(penBack, _X, 0, _X, height);

            cpuY = height - (cpuWert) - 1;
            _graphics.DrawLine(penCPU, _X - INCREMENT, _cpuY, _X, cpuY);
            _cpuY = cpuY;

            proY = height - (proWert) - 1;
            _graphics.DrawLine(penProc, _X - INCREMENT, _proY, _X, proY);
            _proY = proY;

            memY = height - (memWert) - 1;
            _graphics.DrawLine(penMem, _X - INCREMENT, _memY, _X, memY);
            _memY = memY;

            _X += INCREMENT;

            if (_X > panelGraph.Width)
            {
                _X = 0;
                _graphics.Clear(SystemColors.ControlLight);
            }

            _graphics.DrawLine(penFore, _X, 0, _X, height);

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
    }
}
