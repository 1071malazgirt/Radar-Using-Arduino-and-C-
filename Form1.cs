using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        int en = 630, boy = 630;
        float yaricap=315;
        int aci=90;
        int aci2 = 0;
        int merkezX, merkezY;
        int x, y;
        int cisimX, cisimY;
        int kalinlik=30;
        int rx, ry;
        int kontrol = 0;
        int i,j;
        float mesafeDeger;
        Bitmap bmp;
        Pen kalem1, kalem2,kalemTara,kalemSil, kalemCisim;
        string mesafe;
        private void timer2_Tick(object sender, EventArgs e)
        {

            //veri iletiminde hızla ilgili problem var
            if (serialPort1.ReadExisting().EndsWith("\n"))
            {
                mesafe = serialPort1.ReadLine();
                mesafeDeger = Convert.ToInt32(mesafe);
                
            }
            

        }

        int silmeAci = 120;
        
       

        string portName = "COM6";  //geçici olarak yazıldı ilerde textbox ile istenilecek
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            


            kalem2 = new Pen(Color.LimeGreen, 1);
            kalem1 = new Pen(Color.LimeGreen, 2);
            kalemTara = new Pen(Color.LawnGreen, 1);
            kalemSil = new Pen(Color.Black, 3);
            kalemCisim = new Pen(Color.Red, 1);
            g = Graphics.FromImage(bmp);

            


            //çemberlerin çizimi:
            for (i = 0; i < 6; i++)
            {
                for (j=0;j<=en/2;j+=en/10)
                {
                    g.DrawEllipse(kalem1,j,j,en-2*j,boy-2*j);
                   
                }
            }


            //koordinat ekseni:
            g.DrawLine(kalem2, new Point(merkezX, 0), new Point(merkezX, boy));
            g.DrawLine(kalem2, new Point(0, merkezY), new Point(en, merkezY));

            
            int cizgiX, cizgiY;
            for (aci2 = 0; aci2 < 360; aci2 += 30)
            {

                cizgiX = merkezX + (int)(yaricap * Math.Cos(Math.PI * aci2 / 180));
                cizgiY = merkezY + (int)(yaricap * Math.Sin(Math.PI * aci2 / 180));
                g.DrawLine(kalem2, new Point(merkezX, merkezY), new Point(cizgiX, cizgiY));

            }



             

            if (aci<=90 && aci>=-270)
            {
                x = merkezX + (int)(yaricap * Math.Sin(Math.PI * aci / 180));
                y = merkezY - (int)(yaricap * Math.Cos(Math.PI * aci / 180));
                cisimX = merkezX + (int)((mesafeDeger*6) * Math.Sin(Math.PI * aci / 180));
                cisimY= merkezY - (int)((mesafeDeger*6) * Math.Cos(Math.PI * aci / 180));
                rx = merkezX + (int)(yaricap * Math.Sin(Math.PI * silmeAci / 180));
                ry = merkezY - (int)(yaricap * Math.Cos(Math.PI * silmeAci / 180));
            }
            else
            {
                x = merkezX - (int)(yaricap * Math.Sin(Math.PI * silmeAci / 180));
                y = merkezY - (int)(yaricap * Math.Cos(Math.PI * silmeAci / 180));
                cisimX = merkezX - (int)((mesafeDeger*6) * Math.Sin(Math.PI * aci / 180));
                cisimY = merkezY - (int)((mesafeDeger*6) * Math.Cos(Math.PI * aci / 180));
                rx = merkezX - (int)(yaricap * -Math.Sin(Math.PI * silmeAci / 180));
                ry = merkezY - (int)(yaricap * Math.Cos(Math.PI * silmeAci / 180));
            }
       
            g.DrawLine(kalemTara, new Point(merkezX, merkezY), new Point(x,y));
            g.DrawLine(kalemCisim, new Point(merkezX, merkezY), new Point(cisimX,cisimY));
            g.DrawLine(kalemSil, new Point(merkezX, merkezY), new Point(rx, ry));

            //radarın 360 derece dönmesini istiyorum projeye göre değiştirilebilir:
            if (aci != -270&&kontrol!=1)
            {
                aci--;
                silmeAci = (aci+kalinlik)%360;

            }
            else if(aci!=90)
           
            {
                kontrol = 1;
                aci++;
                silmeAci = (aci - kalinlik) % 360;

            }
            if (aci == 90)
            {
                kontrol = 0;
               
            }

            pictureBox1.Image = bmp;
            g.Dispose();

            

        }
        
        
        
        Graphics g;


        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            
        }


        public Form1()
        {
            InitializeComponent();
            serialPort1 = new SerialPort();
            
            serialPort1.BaudRate = 9600;
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = portName;
                serialPort1.Open();

            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(en + 1, boy + 1);
            this.BackColor = Color.Black;
            merkezX = en / 2;
            merkezY = boy / 2;
            

      

            

            

            
        }
    }
}
