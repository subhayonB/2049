using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace _2049
{
    public partial class Form1 : Form
    {
        int bscore = 0;
        public System.Timers.Timer q;
        int score = 0;
        int[] scores = new int[100];
        bool gOver;
        Random a = new Random();
        Label[] bob = new Label[16];
        AImanager ai = new AImanager();
        moves m;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bob[0] = label1;
            bob[1] = label2;
            bob[2] = label3;
            bob[3] = label4;
            bob[4] = label5;
            bob[5] = label6;
            bob[6] = label7;
            bob[7] = label8;
            bob[8] = label9;
            bob[9] = label10;
            bob[10] = label11;
            bob[11] = label12;
            bob[12] = label13;
            bob[13] = label14;
            bob[14] = label15;
            bob[15] = label16;
            label17.Text = "0";
            int ns = (int)(a.NextDouble() * 15 + 1);
            bob[ns].Text = ""+((int)(a.NextDouble() * 2) == 1 ? 2 : 4);
            ai.populate(new int[] { 0 });
           // while (true)
             //   move();
           q = new System.Timers.Timer();
            q.Interval = 170;
            q.Elapsed += new System.Timers.ElapsedEventHandler(_q_Elapsed);
            q.Enabled = true;
            m = move;
            Control.CheckForIllegalCrossThreadCalls = false;
        }
       public delegate void moves();
        
        private void _q_Elapsed(object sender, System.Timers.ElapsedEventArgs e )
        {

            m();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gOver)
            {
                
                if (e.KeyCode == Keys.Up)
                {
                    mUp(0);
                    mUp(1);
                    mUp(2);
                    mUp(3);
                   
                }
                if (e.KeyCode == Keys.Down)
                {
                    mDown(15);
                    mDown(14);
                    mDown(13);
                    mDown(12);
                   
                }
                if (e.KeyCode == Keys.Right)
                {
                    mRight(3);
                    mRight(7);
                    mRight(11);
                    mRight(15);
                   
                }
                if (e.KeyCode == Keys.Left)
                {
                    mLeft(0);
                    mLeft(4);
                    mLeft(8);
                    mLeft(12);
                  
                }
                label17.Text = "Score: " + score;
           
                
                 
                
            }
            for (int i = 0; i < 16; i++)
            {
                if (bob[i].Text == "")
                {
                    break;
                }
                if (i == 15 && bob[i].Text != "")
                {
                    gOver = true;
                }
            }
                if (gOver == true)
                { 
                    scores[ai.CI] = score;
                    
                    for (int j=0;j<16;j++)
                    {
                        bob[j].Text = "";
                    }
                    //label18.Visible = true;
                    ai.gOver(scores);
                    if (score > bscore)
                        bscore = score;
                    label20.Text = "Best Score: " + bscore;
                    label19.Text = "Gen: " + ai.GENERATION;
                    score = 0;
                    int ns = (int)(a.NextDouble() * 15 + 1);
                    bob[ns].Text = "" + ((int)(a.NextDouble() * 2) == 1 ? 2 : 4);
                    gOver = false;
                    label21.Text = "Individual: " + ai.CI;
                  //  label18.Visible = false;
                }
            
        }
        public void mUp(int x)
        {
            int n = 0;
            for (int i = x; i <=15; i += 4)
            {
                if (bob[i].Text == "")
                {
                    n++;
                    continue;
                }
                else if (i != 0 && i != 1 && i != 2 & i != 3)
                {
                    if (n > 0)
                    {
                        bob[i - (n * 4)].Text = bob[i].Text;
                        bob[i].Text = "";
                        n = 1;
                    }
                }
            }
            n = 0;
            for (int i= x; i <=15;i+=4)
            {
                if (bob[i].Text == "")
                {
                    n++;
                    continue;
                }
                if (i != 0 && i != 1 && i != 2 & i != 3)
                {
                    if (n > 0)
                    {
                        bob[i - (n * 4)].Text = bob[i].Text;
                        bob[i].Text = "";
                        n = 1;
                    }
                }
                if (i!=12&& i != 13 && i != 14 && i != 15)
                {
                    if (bob[i].Text!=""&&bob[i+4].Text!="")
                    {
                        if (bob[i].Text == bob[i + 4].Text)
                        {
                            score += 2*Int16.Parse(bob[i].Text);
                            bob[i].Text = "" + 2 * Int16.Parse(bob[i].Text);
                            bob[i + 4].Text = "";
                        }
                    }
                }
            }
        }
        public void mDown(int x)
        {
            int n = 0;
            for (int i = x; i>=0; i -= 4)
            {
                if (bob[i].Text == "")
                {
                    n++;
                    continue;
                }
                else if (i != 15 && i != 14 && i != 13 & i != 12)
                {
                    if (n > 0)
                    {
                        bob[i + (n * 4)].Text = bob[i].Text;
                        bob[i].Text = "";
                        n = 1;
                    }
                }
            }
            n = 0;
            for (int i = x; i >= 0; i -= 4)
            {
                if (bob[i].Text == "")
                {
                    n++;
                    continue;
                }
                if (i != 15 && i != 14 && i != 13 & i != 12)
                {
                    if (n > 0)
                    {
                        bob[i + (n * 4)].Text = bob[i].Text;
                        bob[i].Text = "";
                        n = 1;
                    }
                }
                if (i != 0 && i != 1 && i != 2 && i != 3)
                {
                    if (bob[i].Text != "" && bob[i - 4].Text != "")
                    {
                        if (bob[i].Text == bob[i - 4].Text)
                        {
                            score += 2 * Int16.Parse(bob[i].Text);
                            bob[i].Text = "" + 2 * Int16.Parse(bob[i].Text);
                            bob[i - 4].Text = "";
                        }
                    }
                }
            }
        }
        public void mRight(int x)
        {
            int n = 0;
            for (int i = x; i >= x-3; i --)
            {
                if (bob[i].Text == "")
                {
                    n++;
                    continue;
                }
                else if (i != 15 && i != 11 && i != 7 & i != 3)
                {
                    if (n > 0)
                    {
                        bob[i + (n )].Text = bob[i].Text;
                        bob[i].Text = "";
                        n = 1;
                    }
                }
            }
            n = 0;
            for (int i = x; i >= x-3; i --)
            {
                if (bob[i].Text == "")
                {
                    n++;
                    continue;
                }
                if (i != 15 && i != 11 && i != 7 & i != 3)
                {
                    if (n > 0)
                    {
                        bob[i + (n )].Text = bob[i].Text;
                        bob[i].Text = "";
                        n = 1;
                    }
                }
                if (i != 0 && i != 4 && i != 8 && i != 12)
                {
                    if (bob[i].Text != "" && bob[i - 1].Text != "")
                    {
                        if (bob[i].Text == bob[i - 1].Text)
                        {
                            score += 2 * Int16.Parse(bob[i].Text);
                            bob[i].Text = "" + 2 * Int16.Parse(bob[i].Text);
                            bob[i - 1].Text = "";
                        }
                    }
                }
            }
        }
        public void mLeft(int x)
        {
            int n = 0;
            for (int i = x; i <=x+3; i++)
            {
                if (bob[i].Text == "")
                {
                    n++;
                    continue;
                }
                else if (i != 0 && i != 4 && i != 8 & i != 12)
                {
                    if (n > 0)
                    {
                        bob[i - (n)].Text = bob[i].Text;
                        bob[i].Text = "";
                        n = 1;
                    }
                }
            }
            n = 0;
            for (int i = x; i <= x + 3; i++)
            {
                if (bob[i].Text == "")
                {
                    n++;
                    continue;
                }
                if (i != 0 && i != 4 && i != 8 & i != 12)
                {
                    if (n > 0)
                    {
                        bob[i - (n)].Text = bob[i].Text;
                        bob[i].Text = "";
                        n = 1;
                    }
                }
                if (i != 3 && i != 7 && i != 11 && i != 15)
                {
                    if (bob[i].Text != "" && bob[i + 1].Text != "")
                    {
                        if (bob[i].Text == bob[i + 1].Text)
                        {
                            score += 2 * Int16.Parse(bob[i].Text);
                            bob[i].Text = "" + 2 * Int16.Parse(bob[i].Text);
                            bob[i + 1].Text = "";
                        }
                    }
                }
            }
        }

        public void move()
        {
            int[] inputs = new int[16];

            for (int i = 0; i < 16; i++)
            {
                if (bob[i].Text=="")
                {
                    inputs[i] = 0;
                }
                else
                {
                    inputs[i] = Int16.Parse(bob[i].Text);
                }
            }
         
            switch (ai.move(inputs))
            {
                case 0:
                    Form1_KeyDown(0, new KeyEventArgs(Keys.Up));
                    break;
                case 1:
                    Form1_KeyDown(0, new KeyEventArgs(Keys.Down));
                    break;
                case 2:
                    Form1_KeyDown(0, new KeyEventArgs(Keys.Left));
                    break;
                case 3:
                    Form1_KeyDown(0, new KeyEventArgs(Keys.Right));
                    break;
            }
            int[] cc = new int[16];
            for (int i = 0; i < 16; i++)
            {
                if (bob[i].Text == "")
                {
                   cc[i] = 0;
                }
                else
                {
                    cc[i] = Int16.Parse(bob[i].Text);
                }
            }
            if (cc == inputs)
            {
                gOver = true;

            }
            else
            {
                int[] ecell = new int[16];
                int c = 0;
                for (int i = 0; i < 16; i++)
                {
                    if (bob[i].Text == "")
                    {
                        ecell[c] = i;
                        c++;
                    }
                }
                bob[ecell[(int)(a.NextDouble() * c)]].Text = "" + ((int)(a.NextDouble() * 2) == 1 ? 2 : 4);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            q.Enabled = false;
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.Description = "Where to save?";
            f.ShowNewFolderButton = true;
          //  f.RootFolder = Environment.SpecialFolder.Personal;
            f.ShowDialog();
            String a = f.SelectedPath;
            for (int i=0;i<25;i++)
            {
                using (FileStream fs = File.Create(a +@"\"+ i + ".txt"))
                {
                    string ww="";
                    for(int j=0;j<1200;j++)
                    {
                        ww += ai.SUR[i].WEIGHTS[j]+" ";
                    }
                    Byte[] w = new UTF8Encoding(true).GetBytes(ww);
                    fs.Write(w, 0, w.Length);
                }
            }
            q.Enabled = true;
        }
    }
}
