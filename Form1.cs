using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memoryGame
{
    public partial class Form1 : Form
    {
        
        private Button[][] arrayFruits = new Button[4][];
        private List<int> numbers = new List<int>();
        bool click = false;
        Button firstCard;
        Button secondCard;
        private Timer timer = new Timer();
        private int counter=0;
        private Random rnd = new Random();

        int x = 80;
        int y = 120;
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1500;
            timer.Tick += delay;
            startGame();




        }
        public void startGame()
        {
            
            foreach (Button[] array in arrayFruits)
            {
                if(array!=null)
                foreach (Button fruit in array)
                {
                    if (fruit != null)
                        fruit.Dispose();
                }
            }
            click = false;
            firstCard=null;
            secondCard=null;
            counter = 0;
        
            for (int i = 0; i < 8; i++)
            {
                numbers.Add(i + 1);
                numbers.Add(i + 1);
            }
            Size = new Size(5 * x, y * 5);
            string random_text = "";
            for (int i = 0; i < arrayFruits.Length; i++)
            {
                arrayFruits[i] = new Button[4];
                for (int j = 0; j < arrayFruits[i].Length; j++)
                {
                    Button btn = arrayFruits[i][j] = new Button();
                    btn.Location = new Point(x * j + 35, y * i + 35);
                    btn.Size = new Size(x, y);
                    int random_index = rnd.Next(numbers.Count);
                    random_text = numbers[random_index].ToString();
                    btn.Click += clickEvent;
                    btn.Name = random_text;
                    numbers.RemoveAt(random_index);
                    Controls.Add(btn);
                }
            }
        }
        private void clickEvent(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            btn.BackgroundImage = Image.FromFile("../" + btn.Name + ".jpg");
            btn.BackgroundImageLayout = ImageLayout.Zoom;
            btn.Enabled = false;
            if (!click)
            {
                click = true;
                firstCard = btn;
            }
            else
            {
                click = false;
                secondCard = btn;
                foreach (Button[] array in arrayFruits)
                {
                    foreach (Button fruit in array)
                    {
                        if (fruit != null)
                            fruit.Enabled = false;
                    }
                }
                timer.Start();
            }

        }
        private void delay(object sender, EventArgs e)
        {
            if (firstCard.Name == secondCard.Name)
            {
                // secondCard = null;
                // firstCard= null;
                //firstCard.Enabled = false;
                //secondCard.Enabled = false;
                secondCard.Dispose();
                firstCard.Dispose();
                counter++;
                if (counter == arrayFruits.Length * 2)
                {
                    MessageBox.Show("Winner!!");
                    Application.Exit();
                }
            }
            // firstCard.Enabled = true;
            // secondCard.Enabled = true;
            secondCard.BackgroundImage = null;
            firstCard.BackgroundImage = null;
            
            timer.Stop();
            foreach (Button[] array in arrayFruits)
            {
                foreach (Button fruit in array)
                {
                    if (fruit != null)
                        fruit.Enabled = true;
                }
            }
        }

        

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startGame();
        }
    }
}
