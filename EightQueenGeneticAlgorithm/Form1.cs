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
    /// <summary>
    /// erfan.roshan@gmail.com
    /// </summary>
    public partial class Form1 : Form
    {
        public int n;
        public int p;
        public int mutationPossibillity;
        public selectionType myselection;
        public int[,] currentGeneration;
        public int[,] newGeneration;

        public Form1(int n,int p,int mutationPossibillity, selectionType myselection)
        {
            this.n = n;
            this.p = p;
            this.mutationPossibillity = mutationPossibillity;
            this.myselection = myselection;
            currentGeneration = new int[p, n + 1];
            newGeneration = new int[p, n + 1];
            InitializeComponent();
        }

        public void fitness(int[,] generation)
        {
            for (int i = 0; i < p; i++)
            {
                int count = 0;
                for (int j = 0; j < n; j++)
                {
                    for (int l = j+1; l < n; l++)
                    {
                        if (generation[i,j]==generation[i,l])
                        {
                            count++;
                        }
                        if (Math.Abs(j-l)==Math.Abs(generation[i,j]-generation[i,l]))
                        {
                            count++;
                        }
                    }
                    
                }
                generation[i, n] = 28-count;
            }
        }

        public void sort(int[,] generation)
        {
            int temp;
            for (int i = 0; i < p-1; i++)
            {
                for (int j = i+1; j < p; j++)
                {
                    if(generation[i,n]<generation[j,n])
                        for (int l = 0; l <= n; l++)
                        {
                            temp = generation[i, l];
                            generation[i, l] = generation[j, l];
                            generation[j, l] = temp;
                        }
                }
            }
        }

        public void Print(int [,] generation,ListBox l)
        {
            l.Items.Clear();
            
            for (int i = 0; i < generation.GetLength(0); i++)
            {
                string s="";
                for (int j = 0; j < generation.GetLength(1); j++)
                {
                    
                    if (j == n)
                    {
                        s += " , "+ generation[i, j];
                    }
                    else
                    {
                        s += generation[i, j];
                    }
                }
                l.Items.Add(s);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int nog = 0;
            fitness(currentGeneration);
            if (myselection==selectionType.basic)
            {                
                sort(currentGeneration);
            }
            Print(currentGeneration, listBox1);
            do
            {
                if (nog>0)
                {
                    fitness(currentGeneration);
                    if (myselection == selectionType.basic)
                    {
                        sort(currentGeneration);
                    }
                }
                selection();
                crossover();
                mutation();
                fitness(newGeneration);
                if (myselection==selectionType.basic)
                {
                    sort(newGeneration);
                }                
                currentGeneration = newGeneration;
                nog++;
            } while (newGeneration[0,n]<28);
            Print(newGeneration, listBox2);
            //MessageBox.Show(nog+"", "Number of generation is:");   TODO
            label3.Text = "تعداد نسل ها  :  " + nog;

            label4.Text = "برای مشاهده ی چینش گرافیکی بر روی آیتم های لیست نسل آخر کلیک کنید";
                GUI gui = new GUI();
                gui.n = n;
                gui.chromosome = listBox2.Items[0].ToString();
                gui.ShowDialog();
        }

        private void selection()
        {
            if (myselection==selectionType.basic)
            {
                for (int i = 0; i < p / 2; i++)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        newGeneration[i, j] = currentGeneration[i, j];
                    }
                }
            }
            else if (myselection == selectionType.rouletteWheel)
            {
                Random r = new Random();
                int count = 0;
                int[] indexes = new int[p / 2];
                int totalFitness = 0;
                for (int i = 0; i < p; i++)
                {
                    totalFitness += currentGeneration[i, n];
                }
                do
                {
                    int rouletteRandom = r.Next(0, totalFitness);
                    int sum = 0;
                    for (int i = 0; i < p; i++)
                    {
                        if (rouletteRandom>sum && rouletteRandom<=sum+currentGeneration[i,n])
                        {
                            bool accept = true;
                            for (int j = 0; j <= count; j++)
                            {
                                if (indexes[j]==i)
                                {
                                    accept = false;
                                    break;
                                }
                            }
                            if (accept)
                            {
                                indexes[count] = i;
                                count++;
                                break;
                            }
                        }
                        sum += currentGeneration[i, n];
                    }
                } while (count<p/2);
                for (int i = 0; i < p / 2; i++)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        newGeneration[i, j] = currentGeneration[indexes[i], j];
                    }
                }
            }
        }

        public void crossover()
        {

            for (int i = 0; i < p / 2; i += 2)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (j < n / 2)
                    {
                        newGeneration[i + (p / 2), j] = newGeneration[i, j];
                        newGeneration[i + (p / 2) + 1, j] = newGeneration[i + 1, j];
                    }
                    else
                    {
                        newGeneration[i + (p / 2), j] = newGeneration[i + 1, j];
                        newGeneration[i + (p / 2) + 1, j] = newGeneration[i, j];
                    }
                }
            }
            fitness(newGeneration);
            if (myselection==selectionType.basic)
            {
                sort(newGeneration);
            }
        }

        public void mutation()
        {
            Random r = new Random();
            for (int i = 0; i < p; i++)
            {
                if (r.Next(100)<mutationPossibillity)
                {
                    newGeneration[i, r.Next(n)] = r.Next(n);
                }
            }

        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                GUI gui = new GUI();
                gui.n = n;
                gui.chromosome = listBox2.Items[index].ToString();
                gui.ShowDialog();
                //MessageBox.Show(listBox2.Items[index].ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < p; i++)
            {
                string s = "";
                for (int j = 0; j < n; j++)
                {
                    currentGeneration[i, j] = r.Next(1, n);

                    s += currentGeneration[i, j].ToString();
                }

                listBox1.Items.Add(s);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
