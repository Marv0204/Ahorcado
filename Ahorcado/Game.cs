using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ahorcado
{
    public partial class Game : Form
    {
        private int errores = 0; // Contador de errores
        private int timeRemaining; // Tiempo en segundos (2 minutos)
        private string[] palabras;
        private string palabraActual;
        private int letrasAcertadas;
        private SoundPlayer sonido;
        private int puntaje;

        public Game()
        {
            InitializeComponent();
            timeRemaining = 120; // 2 minutos
            letrasAcertadas = 0;
            puntaje = 0;
            palabras = [
                 "computadora",
                "programacion",
                "ahorcado",
                "juego",
                "desarrollador",
                "tecnologia",
                "ventana",
                "csharp",
                "algoritmo",
                "aplicacion",
                "Juego De Mesa"
                ];
            EstablecerPalabra();
            sonido = new SoundPlayer();

        }

        private void EstablecerPalabra()
        {
            Random random = new Random(); // Crear un objeto Random

            // Generar un número aleatorio entre 0 y 100 (incluyendo 0 pero excluyendo 100)
            int numeroAleatorio = random.Next(11); // El rango es 0 a 100
            palabraActual = palabras[numeroAleatorio]; // Obtener la palabra


            for (int i = 0; i < palabraActual.Length; i++)
            {
                char letra = palabraActual[i];
                Console.WriteLine($"La letra en la posición {i} es: {letra}");


                Label letraLabel = new Label();
                if (letra == ' ')
                {
                    letraLabel.Text = " ";
                }
                else
                {
                    letraLabel.Text = "_";
                }

                letraLabel.AutoSize = true; // Ajusta el tamaño automáticamente
                letraLabel.Font = new Font("Arial", 14, FontStyle.Bold); // Opcional: estilo de fuente
                letraLabel.Margin = new Padding(5); // Espaciado entre letras

                // Agrega la etiqueta al FlowLayoutPanel
                flowLayoutPanel2.Controls.Add(letraLabel);
            }
        }



        private void ActualizarErrores(string letra)
        {
            errores++; // Incrementa los errores
            panel1.Invalidate(); // Fuerza el redibujado

            Label letraLabel = new Label();
            letraLabel.Text = letra;
            letraLabel.AutoSize = true;
            letraLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            letraLabel.ForeColor = Color.Red;
            letraLabel.Margin = new Padding(5); // Espaciado entre letras

            // Agrega la etiqueta al FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(letraLabel);
            if (errores == 7)
            {
                sonido.SoundLocation = "C:/Users/andr9/Desktop/Proyecto/Ahorcado/Música/defeat.wav";
                sonido.Play();
                MessageBox.Show($"HAS SUPERADO LOS INTENTOS FALLIDOS" +
                    $"\n PUNTOS OBTENIDOS: {puntaje}"); // O manejar el fin del juego
                reiniciarAlPerder();
            }
        }

        private void DibujarMuneco(int errores, Graphics g)
        {
            // Estructura base de la horca
            g.DrawLine(Pens.BlueViolet, 50, 200, 50, 50); // Poste vertical
            g.DrawLine(Pens.BlueViolet, 50, 50, 100, 50); // Brazo horizontal
            g.DrawLine(Pens.BlueViolet, 50, 200, 20, 220); // Base diagonal izquierda
            g.DrawLine(Pens.BlueViolet, 50, 200, 80, 220); // Base diagonal derecha

            // Dibuja todas las partes del muñeco según los errores
            if (errores >= 1) g.DrawLine(Pens.BlueViolet, 100, 50, 100, 70); // Cuerda
            if (errores >= 2) g.DrawEllipse(Pens.BlueViolet, 90, 70, 20, 20); // Cabeza
            if (errores >= 3) g.DrawLine(Pens.BlueViolet, 100, 90, 100, 130); // Cuerpo
            if (errores >= 4) g.DrawLine(Pens.BlueViolet, 100, 100, 80, 120); // Brazo izquierdo
            if (errores >= 5) g.DrawLine(Pens.BlueViolet, 100, 100, 120, 120); // Brazo derecho
            if (errores >= 6) g.DrawLine(Pens.BlueViolet, 100, 130, 80, 160); // Pierna izquierda
            if (errores >= 7) g.DrawLine(Pens.BlueViolet, 100, 130, 120, 160); // Pierna derecha
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DibujarMuneco(errores, e.Graphics);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ActualizarErrores();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }
        private void ActualizarPuntaje()
        {
            labelPuntaje.Text = $"PUNTAJE: {puntaje}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (timeRemaining > 0)
            {
                timeRemaining--; // Decrementa el tiempo
                int minutes = timeRemaining / 60;
                int seconds = timeRemaining % 60;
                label1.Text = $"{minutes:D2}:{seconds:D2}"; // Actualiza el Label

            }
            else
            {
                timer1.Stop(); // Detiene el temporizador cuando llega a 0
                sonido.SoundLocation = "C:/Users/andr9/Desktop/Proyecto/Ahorcado/Música/defeat.wav";
                sonido.Play();
                MessageBox.Show($"HAS SUPERADO EL TIEMPO!" +
                     $"\n PUNTOS OBTENIDOS: {puntaje}");
                reiniciarAlPerder();
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            // Convierte sender al tipo Button
            Button botonClicado = sender as Button;

            if (botonClicado != null)
            {
                // Obtén el texto del botón clicado
                string textoBoton = botonClicado.Text;
                Label letraLabel = new Label();
                letraLabel.Text = textoBoton; // Convierte la tecla a texto
                letraLabel.AutoSize = true; // Ajusta el tamaño automáticamente
                letraLabel.Font = new Font("Arial", 14, FontStyle.Bold); // Opcional: estilo de fuente
                letraLabel.Margin = new Padding(5); // Espaciado entre letras

                // Agrega la etiqueta al FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(letraLabel);


            }
        }
        private void ReemplazarLetra(int index, string nuevaLetra)
        {
            // Verifica si el índice es válido
            if (index >= 0 && index < flowLayoutPanel2.Controls.Count)
            {
                // Accede al control en la posición 'index'
                Label letraLabel = flowLayoutPanel2.Controls[index] as Label;

                if (letraLabel != null)
                {
                    // Reemplaza el texto del control con la nueva letra
                    letraLabel.Text = nuevaLetra;
                }
            }
        }

        private void ValidarLetra(object sender, EventArgs e)
        {

            Button botonClicado = sender as Button;
            string letra = botonClicado.Text.ToLower();
            bool yaExiste = flowLayoutPanel2.Controls.Cast<Label>().Any(label => label.Text == letra);
            bool yaExiste2 = flowLayoutPanel1.Controls.Cast<Label>().Any(label => label.Text == letra);
            if (yaExiste || yaExiste2) return;

            palabraActual = palabraActual.ToLower();
            bool existe = palabraActual.Contains(letra);
            for (int i = 0; i < palabraActual.Length; i++)
            {
                if (palabraActual[i] == letra[0])
                {
                    letrasAcertadas++;
                    puntaje++;
                    ActualizarPuntaje();
                    ReemplazarLetra(i, letra);
                }

            }
            if (!existe)
            {
                puntaje -= 5;
                ActualizarPuntaje();
                ActualizarErrores(letra);

            }
            checkWinner();

        }

        private void checkWinner()
        {
            // Remueve espacios de la palabra para comparar solo las letras
            string palabraSinEspacios = palabraActual.Replace(" ", "");

            if (palabraSinEspacios.Length == letrasAcertadas)
            {
                sonido.SoundLocation = "C:/Users/andr9/Desktop/Proyecto/Ahorcado/Música/victoria.wav";
                sonido.Play();
                puntaje += 50;
                ActualizarPuntaje();
                MessageBox.Show("¡Felicidades, has ganado!");
                reiniciarJuego();
            }
        }
        private void reiniciarJuego()
        {
            timeRemaining = 120; // 2 minutos
            letrasAcertadas = 0;
            errores = 0;
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            panel1.Refresh();
            EstablecerPalabra();
            sonido.SoundLocation = "C:/Users/andr9/Desktop/Proyecto/Ahorcado/Música/Juego.wav";
            sonido.PlayLooping();
            timer1.Start();

        }
        private void reiniciarAlPerder()
        {
            reiniciarJuego();
            puntaje = 0;
            ActualizarPuntaje();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ValidarLetra(sender, e);
        }

        private void cancionPrincipal_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
