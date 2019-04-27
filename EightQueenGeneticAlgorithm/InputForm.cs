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
    public enum selectionType { basic, rouletteWheel };
    public partial class InputForm : Form
    {
        
        public InputForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectionType myselection=selectionType.basic;
            if (radioButton1.Checked)
            {
                myselection = selectionType.basic;
            }
            else if (radioButton2.Checked)
            {
                myselection = selectionType.rouletteWheel;
            }
            int n = Convert.ToInt32(textBox1.Text);
            int p = Convert.ToInt32(textBox2.Text);
            int mutationPossibillity = Convert.ToInt32(textBox3.Text);
            Form1 frm1 = new Form1(n,p,mutationPossibillity,myselection);
            this.Visible = false;
            frm1.Show();


        }
    }
}
