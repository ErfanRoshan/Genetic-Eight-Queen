using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EightQueenGeneticAlgorithm
{
    public partial class GUI : Form
    {
        public int n = 0;
        public string chromosome = "";
        List<List<Button>> buttons;
        public GUI()
        {
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            String a = chromosome.Split(',')[0].Remove(chromosome.Split(',')[0].Length - 1,1);
            List<int> queens = new List<int>();
            for (int i = 0; i < a.Length; i++)
            {
                queens.Add(Convert.ToInt32(a[i]-48));
            }
            buttons = new List<List<Button>>();
            int counter = 0;
            for (int i = 0; i < n; i++)
            {
                buttons.Add(new List<Button>());
                for (int j = 0; j < n; j++)
                {
                    Button btn = new Button();
                    if(counter%2==0)
                        btn.BackColor = Color.White;
                    else
                        btn.BackColor = Color.Black;
                    btn.SetBounds(50 + ((400 / n) * i), 50 + ((400 / n) * j), 400 / n, 400 / n);
                    buttons[i].Add(btn);
                    //buttons.Add(btn);
                    this.Controls.Add(buttons[i][j]);
                    counter++;
                }
                counter++;
            }
            int c = 0;
            foreach (var item in queens)
            {
                //buttons[c][item].BackColor = Color.Black;
                buttons[c][item].Image = imageList1.Images[0];
                c++;
            }
        }
    }
}
