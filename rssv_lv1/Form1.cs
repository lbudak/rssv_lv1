using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rssv_lv1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.t = new System.Timers.Timer(1000);
            this.t.Elapsed += new System.Timers.ElapsedEventHandler(vrijeme);
            this.citac = new StreamReader("value.txt");
            button1.Enabled = false;
           
        }
        public StreamReader citac;
        private System.Timers.Timer t;
       

        
        public void checkCriticalValue(int value)
        {
            
            richTextBox1.Text = value.ToString();

        }
        private void vrijeme(object s, EventArgs e)
        {
            Invoke((MethodInvoker)delegate 
            {
                button1.Enabled = false;
                richTextBox2.Enabled = false;
                if (this.citac.EndOfStream)
                {
                    t.Stop();//t.Enabled = false;                        
                    MessageBox.Show("Provjera gotova, kraj dokumenta");
                    appFinish();

                }

                int criticalValue = Int32.Parse(richTextBox2.Text);
                int value = Int32.Parse(this.citac.ReadLine());
                checkCriticalValue(value);
                if (value > criticalValue)
                {
                    t.Stop();//t.Enabled = false;
                    MessageBox.Show(value.ToString() + " je veci od" +
                    " kriticne vrijednosti, prekidam program.");
                    appFinish();

                }
                
               
                   
                              
            });
        }

        public void appFinish() {
            this.citac.ReadToEnd();
            this.citac.BaseStream.Position = 0;
            button1.Text = "Pokreni";
            button1.Enabled = true;
            richTextBox2.Enabled = true;
            richTextBox1.Text = "";
            return;
        }

        public void checkInputValue(string value)
        {
            if (!int.TryParse(value, out int i))
            {
                button1.Enabled = false;
                label1.Text = "Niste unijeli broj ili je prazno polje";
                return;
            }
            label1.Text = "Kritična vrijednost " + value;
            button1.Enabled = true;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            checkInputValue(richTextBox2.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.citac != null)
                citac.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (t.Enabled == false)
            {
                button1.Text = "Zaustavi";
                t.Start(); //Odgovara: t.Enabled = true;
                checkInputValue(richTextBox2.Text);
            }
           
        }
    }
}


