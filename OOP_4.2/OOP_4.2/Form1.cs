using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_4._2
{
    public partial class Form1 : Form
    {
        Model model;

        public Form1()
        {
            InitializeComponent();
            
            model = new Model();
            model.observers += new System.EventHandler(this.UpdateFromModel);
            UpdateFromModel(this, null);
        }

        private void UpdateFromModel(object sender, EventArgs e)
        {
            model.stop();

            textBox1.Text = model.getA().ToString();
            numericUpDown1.Text = model.getA().ToString();
            numericUpDown1.Value = model.getA();
            trackBar1.Value = model.getA();

            textBox2.Text = model.getB().ToString();
            numericUpDown2.Text = model.getB().ToString();
            numericUpDown2.Value = model.getB();
            trackBar2.Value = model.getB(); 
            
            textBox3.Text = model.getC().ToString();
            numericUpDown3.Text = model.getC().ToString();
            numericUpDown3.Value = model.getC();
            trackBar3.Value = model.getC();

            model.start();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (model.EmptyLine(textBox1.Text))
                    model.setA(Int32.Parse(textBox1.Text));
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (model.EmptyLine(textBox1.Text))
                model.setA(Int32.Parse(textBox1.Text));
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            model.setA(Decimal.ToInt32(numericUpDown1.Value));
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (model.EmptyLine(numericUpDown1.Text))
                    model.setA(Decimal.ToInt32(numericUpDown1.Value));
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            if (model.EmptyLine(numericUpDown1.Text))
                model.setA(Decimal.ToInt32(numericUpDown1.Value));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            model.setA(Decimal.ToInt32(trackBar1.Value));
        }



        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (model.EmptyLine(textBox2.Text))
                    model.setB(Int32.Parse(textBox2.Text));
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (model.EmptyLine(textBox2.Text))
                model.setB(Int32.Parse(textBox2.Text));
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            model.setB(Decimal.ToInt32(numericUpDown2.Value));
        }

        private void numericUpDown2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (model.EmptyLine(numericUpDown2.Text))
                    model.setB(Decimal.ToInt32(numericUpDown2.Value));
        }

        private void numericUpDown2_Leave(object sender, EventArgs e)
        {
            if (model.EmptyLine(numericUpDown2.Text))
                model.setB(Decimal.ToInt32(numericUpDown2.Value));
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            model.setB(Decimal.ToInt32(trackBar2.Value));
        }



        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (model.EmptyLine(textBox3.Text))
                    model.setC(Int32.Parse(textBox3.Text));
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (model.EmptyLine(textBox3.Text))
                model.setC(Int32.Parse(textBox3.Text));
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            model.setC(Decimal.ToInt32(numericUpDown3.Value));
        }

        private void numericUpDown3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (model.EmptyLine(numericUpDown3.Text))
                    model.setC(Decimal.ToInt32(numericUpDown3.Value));
        }

        private void numericUpDown3_Leave(object sender, EventArgs e)
        {
            if (model.EmptyLine(numericUpDown3.Text))
                model.setC(Decimal.ToInt32(numericUpDown3.Value));
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            model.setC(Decimal.ToInt32(trackBar3.Value));
        }
    }

    public class Model
    {
        private int a;
        private int b;
        private int c;
        private bool update; 

        public System.EventHandler observers;

        public Model()
        {
            a = Properties.Settings.Default.a;
            b = Properties.Settings.Default.b;
            c = Properties.Settings.Default.c;
        }

        ~Model()
        {
            Properties.Settings.Default.a = a;
            Properties.Settings.Default.b = b;
            Properties.Settings.Default.c = c;
            Properties.Settings.Default.Save();
        }

        public void start()
        {
            update = false;
        }

        public void stop()
        {
            update = true;
        }

        public bool EmptyLine(string s)
        {
            if (s == "") 
            {  
                observers.Invoke(this, null);
                return false; 
            }
            
            else 
                return true;
        }

        public void setA(int a)
        {
            if(this.a == a || update)
            {
                return;
            }

            if (a < 0 || a > 100)
            {
                observers.Invoke(this, null);
                return;
            }

            if (a > b)
                b = a;
            if (a > c)
                c = a;

            this.a = a;
            observers.Invoke(this, null);
        }

        public void setB(int b)
        {
            if (this.b == b || update)
            {
                return;
            }

            if (b < a || b > c)
            {
                observers.Invoke(this, null);
                return;
            }

            this.b = b;
            observers.Invoke(this, null);
        }

        public void setC(int c)
        {
            if (this.c == c || update)
            {
                return;
            }

            if (c < 0 || c > 100)
            {
                observers.Invoke(this, null);
                return;
            }

            if (c < b)
                b = c;
            if (c < a)
                a = c;

            this.c = c;
            observers.Invoke(this, null); 
        }

        public int getA()
        {
            return a;
        }

        public int getB() 
        { 
            return b;
        }

        public int getC()
        {
            return c;
        }
    }
}
