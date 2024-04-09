using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace prySotoMariaLujan
{
    public partial class frmJugarGalaga : Form
    {

        public frmJugarGalaga()
        {
            InitializeComponent();
            InicializarJuego();

        }
        private PictureBox nave;
        private PictureBox pelota;
        private PictureBox[] enemigos;
        private System.Windows.Forms.Timer timer;

        private void InicializarJuego()
        {

            // Configuración del formulario
            this.ClientSize = new Size(800, 600);

            // Configuración de la nave
            nave = new PictureBox();
            nave.Image = Properties.Resources.nave;
            nave.Size = new Size(100, 100);
            nave.SizeMode = PictureBoxSizeMode.StretchImage;
            nave.Location = new Point((this.ClientSize.Width - nave.Width) / 2, this.ClientSize.Height - nave.Height - 20); //ubico la posicion de la nave envX e Y 
            nave.BackColor = Color.Transparent;
            this.Controls.Add(nave); //se agrega el objeto al codigo para poder ser dibujado

            // Configuración de la pelota
            pelota = new PictureBox();
            pelota.Image = Properties.Resources.pelota;
            pelota.Size = new Size(40, 40);
            pelota.SizeMode = PictureBoxSizeMode.StretchImage;
            pelota.BackColor = Color.White;
            pelota.Visible = false;
            pelota.BackColor = Color.Transparent;
            this.Controls.Add(pelota);

            // Configuración de los enemigos
            enemigos = new PictureBox[8];
            int x = 10;
            for (int i = 0; i < enemigos.Length; i++)
            {
                enemigos[i] = new PictureBox();
                enemigos[i].Image = Properties.Resources.enemigo;
                enemigos[i].Size = new Size(60, 60);
                enemigos[i].SizeMode = PictureBoxSizeMode.StretchImage;
                enemigos[i].Location = new Point(x, 20); //coordenadas de ubicacion en X e Y
                enemigos[i].BackColor = Color.Transparent;
                this.Controls.Add(enemigos[i]);
                x += 100;
            }

            // Configuración del temporizador
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 20; // Intervalo de actualización del juego (en milisegundos)
            timer.Tick += timer1_Tick;
            timer.Start();


            this.KeyDown += frmJugarGalaga_KeyDown; //cuando toque una tecla paso al evento
        }


        private void frmJugarGalaga_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Movimiento de la pelota
            if (pelota.Visible)
            {
                pelota.Top -= 10;
                if (pelota.Top < 0)
                {
                    pelota.Visible = false;
                }
                else
                {
                    // Verificar colisiones con los enemigos
                    foreach (PictureBox enemigo in enemigos)
                    {
                        if (pelota.Bounds.IntersectsWith(enemigo.Bounds))
                        {
                            // Eliminar el enemigo
                            this.Controls.Remove(enemigo);
                            enemigo.Dispose(); // Liberar recursos
                            pelota.Visible = false;
                            break;
                        }
                    }
                }
            }
        }

        private void frmJugarGalaga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)// Movimiento de la nave con las flechas 
            {
                if (nave.Left > 0) //controlo que no se salga del borde izq del form, caso contrario no se mueve más
                    nave.Left -= 10; //mueve 10pxl
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (nave.Right < this.ClientSize.Width)
                    nave.Left += 10;
            }

            if (e.KeyCode == Keys.Space) // Disparo de la pelota

            {
                Disparar();
            }
        }

        private void Disparar()
        {
            if (!pelota.Visible)
            {
                // Posición inicial de la pelota
                pelota.Left = nave.Left + nave.Width / 2 - pelota.Width / 2; //establezco la pos horizontal d la pelota respecto a la nave
                pelota.Top = nave.Top - pelota.Height; // establezco la pos vert de la pelota justo sobre l nave
                pelota.Visible = true;
            }
        }
    }
}
