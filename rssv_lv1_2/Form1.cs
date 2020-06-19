using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rssv_lv1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.time = 0;
            
            this.now = DateTime.Now.TimeOfDay;
        }
        private int time;
        
        private TimeSpan now;
        public void checkHours(int hours)
        {
            if (hours > 23 && hours < 0)
            {

                textBox1.Text = "";
                MessageBox.Show("Unesi pravilno sate");
                return;
            }
            else {
                time += hours * 3600000; 
            }
        }

        public void checkMinutes(int minutes)
        {
            if (minutes > 60 && minutes < 0)
            {

                textBox2.Text = "";
                MessageBox.Show("Unesi pravilno minute");
                return;
            }
            else {
                time += minutes * 60000;
            }
        }

        private void setAlarm(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
                checkMinutes(Int32.Parse(textBox2.Text));
            else
            { MessageBox.Show("Unesi pravilno minute"); return; }
            if (textBox1.Text != "")
                checkHours(Int32.Parse(textBox1.Text));
            else
            { MessageBox.Show("Unesi pravilno sate"); return; }
        }
        public void beeping() {
            Console.Beep(3069, 5000);
            MessageBox.Show("ALARM!!!!!");
        }
        private void alarmBeep(object sender, EventArgs e)
        {
            setBtn.Enabled = false;
            alarmBtn.Enabled = false;
            TimeSpan alarmTime = TimeSpan.FromMilliseconds(time) - this.now;
            
            Thread.Sleep(alarmTime);
            beeping();
            alarmBtn.Enabled = true;
            setBtn.Enabled = true;
            time = 0;
        }
    }
}
