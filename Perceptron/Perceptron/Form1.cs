using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        double w1, w2, bias;
        double x1, x2;
        double learning_rate;
        int output;
        int[] desired;
        int[,] patterns;
        Random rnd;

        private void Form1_Load(object sender, EventArgs e)
        {
            rnd = new Random();
            w1 = rnd.NextDouble();
            w2 = rnd.NextDouble();
            learning_rate = 0.1;
            bias = rnd.NextDouble();
            patterns = new int[4, 2];
            desired = new int[4];

            patterns[0, 0] = 0;
            patterns[0, 1] = 0;
            patterns[1, 0] = 0;
            patterns[1, 1] = 1;
            patterns[2, 0] = 1;
            patterns[2, 1] = 0;
            patterns[3, 0] = 1;
            patterns[3, 1] = 1;

            desired[0] = 0;
            desired[1] = 0;
            desired[2] = 0;
            desired[3] = 1;
        }

        public Form1()
        {
            InitializeComponent();
        }

        //test
        private void button1_Click(object sender, EventArgs e)
        {
            double v;
            x1 = Convert.ToInt32(textBox1.Text);
            x2 = Convert.ToInt32(textBox2.Text);

            v = x1 * w1 + x2 * w2 + bias;
            if (v >= 0) textBox3.Text = "1";
            else textBox3.Text = "0";
        }
        
        //train
        private void button2_Click(object sender, EventArgs e)
        {
            double v, delta;
            int max_epoch = 10000, epochs = 0;
            int error = 10;
            int pn; //represents the pattern number
            int[] pat_used = new int[4]; //monitor the pattern

            while (error > 0 && epochs < max_epoch)
            {
                //set to 0 every epoch
                pat_used[0] = 0;
                pat_used[1] = 0;
                pat_used[2] = 0;
                pat_used[3] = 0;
                error = 0;

                for (int i = 0; i < 4; i++)
                {
                    //we are selecting a random pattern
                    pn = rnd.Next(4);
                    while (pat_used[pn] == 1)
                    {
                        pn = rnd.Next(4);
                    }
                    x1 = patterns[pn, 0];
                    x2 = patterns[pn, 1];
                    pat_used[pn] = 1;

                    v = x1 * w1 + x2 * w2 + bias;
                    if (v >= 0) output = 1;
                    else output = 0;

                    delta = desired[pn] - output;
                    if (delta != 0)
                    {
                        w1 += learning_rate * delta * x1;
                        w2 += learning_rate * delta * x2;
                        bias += learning_rate * delta;
                    }

                    error += Math.Abs((int)delta);
                }
                epochs++;
            }

            MessageBox.Show("Finished!\nEpochs: " + epochs + "\nErrors: " + error);
        }
    }
     
}