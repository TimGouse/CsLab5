using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsLab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.KeyPress += textBox1_KeyPress;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить ввод только цифр, управляющих символов и одного минуса
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '-') && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Разрешить минус только в первой позиции и только один раз
            if (e.KeyChar == '-' && textBox1.Text.Length > 0 && textBox1.SelectionStart != 0)
            {
                e.Handled = true;
            }

            // Разрешить только одну точку
            if (e.KeyChar == '.' && textBox1.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x;
            if (!double.TryParse(textBox1.Text, out x) || x < -13 || x > 20)
            {
                MessageBox.Show("Введите значение от -13 до 20.");
                return;
            }

            double eps = GetSelectedEps(); // Метод для получения выбранной точности
            double sum = 1; // Начальное значение суммы ряда, т.е. exp(0)
            double term = 1; // Первый член ряда
            int num = 0;
            for (int n = 1; Math.Abs(term) > eps; n++)
            {
                term *= x / n;
                sum += term;
                num = n;
            }

            double exactValue = Math.Exp(x); // Точное значение, используя Math.Exp

            // Отображаем результаты
            label3.Text = $"Исходное значение = {x}";
            label4.Text = "Точность = " + eps.ToString("0.###################");
            label5.Text = $"Точное значение exp(x) = {exactValue.ToString("F" + GetDecimalPlaces(eps))}";
            label6.Text = $"Число слагаемых = " + num.ToString();
            label7.Text = $"Сумма ряда exp(x) = {sum.ToString("F" + GetDecimalPlaces(eps))}";
        }
        private double GetSelectedEps()
        {
            // Возвращает значение eps на основе выбранного RadioButton
            if (radioButton1.Checked) return 0.1;
            if (radioButton2.Checked) return 0.01;
            if (radioButton3.Checked) return 0.0001;
            if (radioButton4.Checked) return 0.001;
            if (radioButton5.Checked) return 0.000001;
            if (radioButton6.Checked) return 0.00001;
            return 0; // По умолчанию или в случае ошибки
        }
        private int GetDecimalPlaces(double eps)
        {
            if (eps >= 0.1) return 1;
            if (eps >= 0.01) return 2;
            if (eps >= 0.001) return 3;
            if (eps >= 0.0001) return 4;
            if (eps >= 0.00001) return 5;
            if (eps >= 0.000001) return 6;
            return 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
