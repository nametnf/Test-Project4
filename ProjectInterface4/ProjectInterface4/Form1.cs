//Yun-Ming Hu
//Assignment 4
//testing 1234
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectInterface4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int max_length = 22;
        private String textPath = "Assignment 4 error.txt";

        private void Form1_Load(object sender, EventArgs e)
        {
            labelLength.Text = "0/" + max_length;

            labelDisplay.Text = "0";
        }

        //dragging
        private bool dragging = false;
        private Point start_point = new Point(0, 0);

        private void pictureBoxBorder_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            start_point = new Point(e.X, e.Y);
        }

        private void pictureBoxBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging == true)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.start_point.X, p.Y - this.start_point.Y);
            }
        }

        private void pictureBoxBorder_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void labelTitle_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxBorder_MouseUp(this, e);
        }

        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxBorder_MouseDown(this, e);
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxBorder_MouseMove(this, e);
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //

        public static int ReversePolishResult(string input)
        {
            Stack values = new Stack();

            string[] separators = new string[] { " " };
            foreach (String word in input.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                if (word.Length == 1 &&
                    (word == "+" || word == "-" || word == "*" || word == "/"))
                {
                    int second = Convert.ToInt32(values.Peek().ToString());
                    values.Pop();
                    int first = Convert.ToInt32(values.Peek().ToString());
                    values.Pop();
                    if (word == "+")
                        values.Push(first + second);
                    else if (word == "-" && word.Length == 1)
                    {
                        values.Push(first - second);
                    }
                    else if (word == "*")
                        values.Push(first * second);
                    else
                        values.Push(first / second);
                }
                else if (word != " ")
                {
                    //MessageBox.Show("Converted:" + Convert.ToInt32(word));
                    values.Push(Convert.ToInt32(word));
                }
            }
            return Convert.ToInt32(values.Peek().ToString());
            //return (int)values.Peek();
        }

        public static String InfixToReversePolish(string infix)
        {
            List<String> output = new List<String>();
            List<String> operators = new List<String>();
            String reversePolish = "";

            infix = infix.Replace("+", " + ");
            //infix = infix.Replace("-", " - ");
            infix = infix.Replace("*", " * ");
            infix = infix.Replace("/", " / ");

            string[] separators = new string[] { " " };
            foreach (String word in infix.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                if (word == "+" || word == "-" || word == "*" || word == "/")
                {
                    operators.Insert(0, (word.ToString() + " ")); //add to beginning
                }
                else if (word != " ")
                {
                    output.Add((word + " "));
                }
            }

            //Combine them
            //output.AddRange(operators);
            //ArrayList myOutputList = output.Get();
            output.AddRange(operators);
            foreach (String item in output)
            {
                reversePolish += item;
            }

            //Clean up last space
            if (reversePolish[reversePolish.Length - 1] == ' ')
            {
                reversePolish = reversePolish.Remove(reversePolish.Length - 1, 1); //remove last space
            }

            return reversePolish;
        }
        //
        //Button events
        //
        private void button0_Click(object sender, EventArgs e)
        {
            if (labelDisplay.Text.Length < max_length)
            {
                if (labelDisplay.Text == "0")
                {
                    return;
                }
                if (labelDisplay.Text == "" || labelDisplay.Text.Length == 0)
                {
                    labelDisplay.Text = "0";
                    return;
                }
                for (int i = labelDisplay.Text.Length - 1; i >= 0; --i)
                {
                    //Space, followed by digit another 0
                    if (labelDisplay.Text[i] == ' ' && labelDisplay.Text[i + 1] == '0')
                    {
                        return;
                    }
                }
                //0 after numbers
                if (Char.IsNumber(labelDisplay.Text[labelDisplay.Text.Length - 1]))
                    labelDisplay.Text += "0";
                //0 after operation
                else if (labelDisplay.Text[labelDisplay.Text.Length - 1] != '0')
                    labelDisplay.Text += " 0";
            }
        }

        private void button_OneToNineClick(String number)
        {
            if (labelDisplay.Text.Length < max_length)
            {
                //Replace 0 with number
                if (labelDisplay.Text == "0" || labelDisplay.Text.Length == 0)
                    labelDisplay.Text = number;

                //Add single number after number
                else if (Char.IsNumber(labelDisplay.Text[labelDisplay.Text.Length - 1]))
                {
                    //Eliminate zero after pressing numbers after an operator (1 + 01234)
                    for (int i = labelDisplay.Text.Length - 1; i >= 0; --i)
                    {
                        //Space, followed by digit another 0
                        if (labelDisplay.Text[i] == ' ' && labelDisplay.Text[i + 1] == '0')
                        {
                            labelDisplay.Text = labelDisplay.Text.Remove(i + 1, 1);
                            labelDisplay.Text += number;
                            return;
                        }
                    }
                    labelDisplay.Text += number;
                }
                //Add space plus number after operator
                else
                    labelDisplay.Text += (" " + number);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button_OneToNineClick("9");
        }

        private void button_OperatorClick(String symbol)
        {
            if (labelDisplay.Text.Length < max_length)
            {
                if (labelDisplay.Text == "" || labelDisplay.Text.Length == 0)
                    return;
                else if (Char.IsNumber(labelDisplay.Text[labelDisplay.Text.Length - 1]))
                    labelDisplay.Text += (" " + symbol);
                else if (labelDisplay.Text[labelDisplay.Text.Length - 1] == '+' ||
                        labelDisplay.Text[labelDisplay.Text.Length - 1] == '-' ||
                        labelDisplay.Text[labelDisplay.Text.Length - 1] == '*' ||
                        labelDisplay.Text[labelDisplay.Text.Length - 1] == '/')
                {
                    //Replace operator with another operator
                    labelDisplay.Text = labelDisplay.Text.Remove(labelDisplay.Text.Length - 1, 1);
                    labelDisplay.Text += symbol;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            button_OperatorClick("+");
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            button_OperatorClick("-"); //display bug? causes minus character to have one less space
        }

        private void buttonMulti_Click(object sender, EventArgs e)
        {
            button_OperatorClick("*");
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            button_OperatorClick("/");
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            try
            {
                labelDisplay.Text = ReversePolishResult(InfixToReversePolish(labelDisplay.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to calculate due to " + ex.Message + "\nError will be saved to " + textPath, "Calculation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DateTime now = DateTime.Now;
                try
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(textPath, true);
                    file.WriteLine(now.ToString());
                    file.WriteLine("Error calculating: " + labelDisplay.Text);
                    file.WriteLine("Exception thrown: " + ex.Message);
                    file.WriteLine("----------------\n");
                    file.Close();
                }
                catch (Exception fex)
                {
                    MessageBox.Show("Could not write to file " + textPath + " due to " + fex.Message, "File write error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            labelDisplay.Text = "0";
        }

        private void buttonNeg_Click(object sender, EventArgs e)
        {
            //The negative/positive button is designed to convert the last numeric value to its opposite
            if (labelDisplay.Text == "0" || labelDisplay.Text.Length == 0)
                return;
            //Single number with no operations
            else if (!labelDisplay.Text.Contains(" "))
            {
                if (Char.IsDigit(labelDisplay.Text[0]))
                {
                    labelDisplay.Text = labelDisplay.Text.Insert(0, "-");
                }
                else if (labelDisplay.Text[0] == '-')
                {
                    labelDisplay.Text = labelDisplay.Text.Remove(0, 1);
                }
            }
            else
            {
                for (int i = labelDisplay.Text.Length - 1; i >= 0; --i)
                {
                    try
                    {
                        //Space, followed by digit (results in adding a negative symbol)
                        if (labelDisplay.Text[i] == ' ' && Char.IsDigit(labelDisplay.Text[i + 1]))
                        {
                            //MessageBox.Show("Insert");
                            labelDisplay.Text = labelDisplay.Text.Insert(i + 1, " -");
                            return;
                        }
                        //Space, followed by a number with negative symbol (results in removing the negative symbol)
                        else if (labelDisplay.Text[i] == ' ' && labelDisplay.Text[i + 1] == '-' && Char.IsDigit(labelDisplay.Text[i + 2]))
                        {
                            //MessageBox.Show("Delete");
                            labelDisplay.Text = labelDisplay.Text.Remove(i, 2);
                            return;
                        }
                    }
                    catch (Exception)
                    { 
                        ; //Do nothing and allow program to edit the last numeric value 
                    }
                }
            }
        }

        private void labelDisplay_TextChanged(object sender, EventArgs e)
        {
            labelLength.Text = labelDisplay.Text.Length + "/" + max_length;
        }

        private void buttonBk_Click(object sender, EventArgs e)
        {
            if (labelDisplay.Text.Length != 0)
                labelDisplay.Text = labelDisplay.Text.Remove(labelDisplay.Text.Length - 1, 1);

            if (labelDisplay.Text.Length == 0)
            {
                labelDisplay.Text = "0";
                return;
            }
            //After removing, check for negative symbol
            if (!Char.IsDigit(labelDisplay.Text[labelDisplay.Text.Length - 1]))
                labelDisplay.Text = labelDisplay.Text.Remove(labelDisplay.Text.Length - 1, 1);

            //Clean up spaces
            for (int i = 0; i < 3; i++)
            {
                if (labelDisplay.Text[labelDisplay.Text.Length - 1] == ' ')
                    labelDisplay.Text = labelDisplay.Text.Remove(labelDisplay.Text.Length - 1, 1);
            }
        }


    }
}
