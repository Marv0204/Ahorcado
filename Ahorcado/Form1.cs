using System.Media;

namespace Ahorcado

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SoundPlayer sonido = new SoundPlayer();
            sonido.SoundLocation = "C:/Users/andr9/Desktop/Proyecto/Ahorcado/Música/Juego.wav";
            sonido.PlayLooping();
            //hacer que cada cierto tiempo se vuelva a reproducir
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Game();
            frm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
