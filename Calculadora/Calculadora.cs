using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Calculadora : Form
    {
        private bool puntoUsado = false; //Variable para saber si btnPunto ha sido utilizado o no
        private double numeroA = 0.0, numMemoria = 0.0; //Variables para el primer número ingresado y para el número en memoria
        private string operacionActual = ""; //Variable para la operación seleccionada

        public Calculadora()
        {
            InitializeComponent(); //Se inicializa el formulario
            //Se conectan los eventos click de cada botón numérico con el método numero_Click
            btn0.Click += numero_Click;
            btn1.Click += numero_Click;
            btn2.Click += numero_Click;
            btn3.Click += numero_Click;
            btn4.Click += numero_Click;
            btn5.Click += numero_Click;
            btn6.Click += numero_Click;
            btn7.Click += numero_Click;
            btn8.Click += numero_Click;
            btn9.Click += numero_Click;

            btnPunto.Click += punto_Click; //Se conecta el evento click de btnPunto con el método punto_Click

            //Se conectan los eventos click de cada botón de operación con el método operacion_Click
            btnSumar.Click += operacion_Click;
            btnRestar.Click += operacion_Click;
            btnMultiplicar.Click += operacion_Click;
            btnDivision.Click += operacion_Click;

            btnCalcular.Click += igual_Click; //Se conecta el evento click del btnCalcular con el método igual_Click
        }

        private void numero_Click(object sender, EventArgs e) //Método para concatenar lo que hay en pantalla con el botón numérico detonante
        {
            Button boton = (Button)sender; //Convierte el objeto sender a button
            txtDisplay.Text = txtDisplay.Text + boton.Text; //Actualización del texto en el display
        }

        private void punto_Click(object sender, EventArgs e) //Método para verificar que btnPunto pueda usarse solo una vez
        {
            if (!puntoUsado) //Verifica si puntoUsado está en false
            {
                txtDisplay.Text = txtDisplay.Text + "."; //Concatena un punto
                puntoUsado = true; //Cambia el estado de puntoUsado a true
            }
        }

        private void operacion_Click(object sender, EventArgs e) //Método con el que trabajan los botones de operaciones
        {
            Button boton = (Button)sender; //Convierte el objeto sender a button
            operacionActual = boton.Text; //Guarda la opción seleccionada en la variable operacionActual
            numeroA = double.Parse(txtDisplay.Text); //Guarda el contenido del display en la variable numeroA
            txtDisplay.Text = ""; //Reinicia el display
            puntoUsado = false; //Reinicia la variable puntoUsado
        }

        private void igual_Click(object sender, EventArgs e) //Método con el que trabaja el botón =
        {
            double numeroB = double.Parse(txtDisplay.Text); //Variable para el segundo número ingresado
            double resultado = 0.0; //Variable para el resultado

            switch (operacionActual) //Condiciones para tratar cada operación
            {
                case "+":
                    resultado = numeroA + numeroB; //Suma el primer número ingresado con el segundo
                    break;
                case "-":
                    resultado = numeroA - numeroB; //Resta del primer número ingresado el segundo
                    break;
                case "*":
                    resultado = numeroA * numeroB; //Multiplica el primer número ingresado con el segundo
                    break;
                case "/":
                    resultado = numeroA / numeroB; //Divide el primer número ingresado entre el segundo
                    break;
            }
            txtDisplay.Text = resultado.ToString(); //Convierte el resultado en texto
            puntoUsado = false; //Reinicia la variable puntoUsado
        }

        private void btnRetroceso_Click(object sender, EventArgs e) //Evento click del botón Retroceso
        {
            if (txtDisplay.Text.Length > 0) //Se verifica que el display no esté vacío
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1); //Toma el texto que contiene el display desde el inicio (0) y le quita el ultimo dígito (length-1)
        }

        private void btnMS_Click(object sender, EventArgs e) //Evento click del botón MS (Guardar en memoria)
        {
            numMemoria = double.Parse(txtDisplay.Text); //Convertir el texto del display a número con decimales
        }

        private void btnMR_Click(object sender, EventArgs e) //Evento click del botón MR (Recuperar memoria)
        {
            txtDisplay.Text = numMemoria.ToString(); //Convierte el número de la variable numMemoria en texto
        }

        private void btnCE_Click(object sender, EventArgs e) //Evento click del botón CE (Limpiar Memoria)
        {
            numMemoria = 0.0;
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e) //Opción cortar del menú superior
        {
            Clipboard.SetText(txtDisplay.Text); //Copia el texto del display al portapapeles
            txtDisplay.SelectedText = ""; //Limpia el display
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e) //Opción pegar del menú superior
        {
            if (Clipboard.ContainsText()) //Revisa si el portapapeles tiene texto guardado
                txtDisplay.Text += Clipboard.GetText(); //Muestra el contenido del portapapeles en el display
        }

        private void btnC_Click(object sender, EventArgs e) //Evento click del botón C (Limpiar pantalla)
        {
            //Se reinician los valores y el display
            txtDisplay.Text = string.Empty;
            numeroA = 0;
            operacionActual = "";
            puntoUsado = false;
        }
    }
}
