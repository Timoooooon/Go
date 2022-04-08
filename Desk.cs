using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Go
{
  


    class Desk {

        static bool?[,] self;
        static bool xod;

        public Desk(int[2] size, bool first_xod = false)
        {
            self = new bool?[size[0], size[1]];
            xod = first_xod;
        }
        static Dictionary<bool?, string> my_enum = new Dictionary<bool?, string>() { { null, "empty" }, { true, "white" }, { false, "black" } };
        //const


        public bool? get (Point p)
        {
            return self[p.X, p.Y];
        }        
        public IEnumerable<Point> neighbors (Point a)
        {
            List<Point> udrl = new List<Point> { 
                new Point(0, 1), new Point(0, -1), new Point(1, 0), new Point(-1, 0) };
                
            foreach (Point i in udrl) {
                Point res = new Point(a.X + i.X, a.Y + i.Y);

                bool err_hapened = true;
                try
                {
                    get(res);
                    err_hapened = false;
                }
                catch (IndexOutOfRangeException) { }
                if (err_hapened) { }
                else
                {
                    yield return res;
                }

            }

        }

        private void set(Point p, bool? value)
        {
            self[p.X, p.Y] = value;
        }

        public void set (int x, int y, bool stone)
        {
            Point p = new Point(x, y);
            bool enemy = !xod;

            if (stone == xod)
            {
                if (get(p).HasValue())
                {
                    throw new PlayerException("Seat is taken!", "You tried to put "+ my_enum )
                }
                set (p, xod);

            } else
            {
                throw new OrderException(stone);
            }
            try
            {

                foreach (Point i in neighbors(p))
                {
                    if (get(i) == enemy)
                    {
                        (bool i_res, HashSet<Point> group) = check(i, enemy);
                        if (!i_res)
                        {
                            kill(group);
                        }
                    }
                }
                (bool res, HashSet<Point> _) = check(p, xod);
                if (!res)
                {

                    throw new PlayerException("Suicide", "Suicide is not an option! Stop!");
                }
            }
            catch
            {
                {   //undo
                    set(p, null);
                }
                throw;
            }
            xod = enemy;
            last_xod = p;
        }

        static Point last_xod;
        private void kill(HashSet<Point> group)
        {
            //if (group.Contains(last_xod) && group.Count == 1)
            if (group == new HashSet<Point> {last_xod})
            {
                throw new PlayerException ("Ko!", "You can eat each other endlessly");
            }

            foreach (Point i in group)
            {
                set (i, null);
            }
        }
        public (bool res, HashSet<Point>) check (Point p, bool team)
        {
            HashSet<Point> group = new HashSet<Point>();

            bool res = check(p, team, group);
            if (res)
            {
                group = null;
            }
            return (res, group);
        }


        private bool check ( Point p, bool team, HashSet<Point> group)
        {
            if (group.Contains(p)) {
                return false;   //already cheked
            }
            group.Add(p);

            bool? stone = get(p);
            if (stone == team)
            {
                //group.Any
                foreach (Point i in neighbors(p))
                {
                    if (check(i, team, group))
                    {
                        return true;
                    }
                    
                }
                return false;
            }
            else if (stone == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Paint(PictureBox pic)
        {
            Graphics polotno = Graphics.FromHwnd(pic.Handle);
            polotno.Clear(Color.LightYellow);

            Size size = new Size(self.GetUpperBound(0), self.GetUpperBound(1));
            
            Matrix.Draw_grid(polotno, size);

            for (int i = 0; i < self.GetUpperBound (0); i++)
            {
                for (int k = 0; k < self.GetUpperBound(1); k++)
                {
                    bool? stone = self[i, k];

                    polotno.draw
                }
            }
        }
    }
    static class Matrix
    {
        public void Draw_grid(Graphics polotno, Size size, Pen pen = null)
        {
            if (pen == null)
            {
                pen = new Pen(Color.Black, 2);
            }


            float step = polotno.Width / size.Width;
            for (int i = 1; i < size.Width; i++)
            {
                Point start = new Point((int)(i * step), 0);
                Point end = new Point((int)(i * step), polotno.Height);
                polotno.DrawLine(pen, start, end);
            }
        }
        public IEnumerable<Point> index (Point a)
        {

        }


    }

    class PlayerException : InvalidOperationException
    {
        string name, text;
        public PlayerException(string err_Name, string err_Text)        //Link to text, maybe int

        {
            name = err_Name;
            text = err_Text;
        }
        public void Show()
        {
            MessageBox.Show(text, name, buttons, boxIcon);
        }
        MessageBoxButtons buttons = MessageBoxButtons.OK;
        //MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.
        MessageBoxIcon boxIcon = MessageBoxIcon.Error;
    }
}
