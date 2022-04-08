using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Go

{
    public partial class main : Form
    {
        //Random rnd = new Random();
        PictureBox pic;
        Desk desk;
        public main()
        {
            InitializeComponent();
        }
        private void main_Resize(object sender, EventArgs e)
        {
            int size = this.Width;
            this.Height = size;
        
            
            const int border = 10;
            pic.Location = new Point(border, border);
            pic.Size = new Size(Width - pic.Left - border, Height - pic.Top - border);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            main_Resize(null, null);
            pic.Click += new System.EventHandler(pic_Click);
            pic.Resize += new System.EventHandler(pic_Resize);
            pic.Paint =


            //const int n = 2;
            //const double great_border = 2.5;
            //int size = (int) this.Width - (border * n + border * great_border * 1);

            //me = new PictureBox();

            //    this.Controls.Add(pic);
            //}
        }
        private void pic_Resize (PictureBox sender, EventArgs e)
        {
            desk.Paint(sender);
        }
        private void pic_Click(PictureBox sender, MouseEventArgs e)
        {
            Point input = e.Location;
            int x = 0, y = 0;
            for (int i = 0; i < input.X; i+= sender.Width / 10)
            {
                x++;
            }
            for (int i = 0; i < input.Y; i += sender.Height / 10)
            {
                y++;
            }

            try
            {
                desk.set(x, y);
            }
            catch (PlayerException ex)
            {
                Console.WriteLine(ex.Data);
                ex.Show();
            }

            desk.Paint(sender);
        }







        private void btn_Click(object sender, EventArgs e)
        {
            pic = new PictureBox();
            this.Controls.Add(pic);

            desk = new Desk();
           
            desk.Paint(pic);
        }

    }
}
