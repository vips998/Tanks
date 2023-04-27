﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Танки_курсач
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(true);
            this.Hide();
            form1.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(false);
            this.Hide();
            form1.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
