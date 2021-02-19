using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {

        public int money = 100;

        Dictionary<CheckBox, Cell> field = new Dictionary<CheckBox, Cell>();

        public Form1()
        {
            InitializeComponent();
            foreach (CheckBox cb in panel1.Controls)
                field.Add(cb, new Cell());
            textBox1.Text = money.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (sender as CheckBox);
            if (cb.Checked) Plant(cb);
            else Harvest(cb);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (CheckBox cb in panel1.Controls)
                NextStep(cb);
        }

        public void CreateMyTextBoxControl()
        {
            TextBox textBox1 = new TextBox();
        }

        private void Plant(CheckBox cb)
        {
            field[cb].Plant();
            if (field[cb].state == CellState.Planted) money -= 2;
            UpdateBox(cb);
            textBox1.Text = money.ToString();
        }

        private void Harvest(CheckBox cb)
        {
            if (field[cb].state == CellState.Immature) money += 3;
            if (field[cb].state == CellState.Mature) money += 5;
            if (field[cb].state == CellState.Overgrown) money -= 1;
            field[cb].Harvest();
            UpdateBox(cb);
            textBox1.Text = money.ToString();
        }

        private void NextStep(CheckBox cb)
        {
            field[cb].NextStep();
            UpdateBox(cb);
        }

        private void UpdateBox(CheckBox cb)
        {
            Color c = Color.White;
            switch (field[cb].state)
            {
                case CellState.Planted: c = Color.Black;
                    break;
                case CellState.Green: c = Color.Green;
                    break;
                case CellState.Immature: c = Color.Yellow;
                    break;
                case CellState.Mature: c = Color.Red;
                    break;
                case CellState.Overgrown: c = Color.Brown;
                    break;
            }
            cb.BackColor = c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in panel1.Controls)
                field[cb].changeTimeMinus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in panel1.Controls)
                field[cb].changeTimePlus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    enum CellState
    {
        Empty,
        Planted,
        Green,
        Immature,
        Mature,
        Overgrown
    }

    class Cell
    {
        public CellState state = CellState.Empty;
        public int progress = 0;
        public int time = 0;
        TextBox textBox1 = new TextBox();
     
        private const int prPlanted = 20;
        private const int prGreen = 100;
        private const int prImmature = 120;
        private const int prMature = 140;

      
        public void changeTimeMinus()
        {
            time += 10;
        }

        public void changeTimePlus()
        {
            time -= 10;
        }
        public void Plant()
        {
            state = CellState.Planted;
            progress = 1;

        }

        public void Harvest()
        {
            state = CellState.Empty;
            progress = 0;
        }

        public void NextStep()
        {
            if ((state != CellState.Empty) && (state != CellState.Overgrown))
            {
                progress++;
                if (progress < prPlanted + time) state = CellState.Planted;
                else if (progress < prGreen + time) state = CellState.Green;
                else if (progress < prImmature + time) state = CellState.Immature;
                else if (progress < prMature + time) state = CellState.Mature;
                else state = CellState.Overgrown;
            }
        }
    }
}
