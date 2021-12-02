using DIPLi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoFinal_Macas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string imageLocation = "";

        public double[] calcularHistograma(DIPLi.Imagem IMG)
        {
            double[] valores = new double[256];
            int niveldecinza;

            for (int i = 0; i < IMG.Altura; i++)
            {
                for (int j = 0; j < IMG.Largura; j++)
                {
                    niveldecinza = (int)IMG[i, j];
                    valores[niveldecinza] = valores[niveldecinza] + 1;
                }
            }
            return valores;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {

            if (imageLocation == "")
            {
                MessageBox.Show("É preciso carregar a imagem!", "Ocorreu um erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Imagem I = new Imagem(imageLocation);

                Imagem R = I.Separar(PlanoImagem.Red);
                Imagem G = I.Separar(PlanoImagem.Green);

                // Nivel vermelho
                double somaYGFim = 0;
                double somaYRFim = 0;

                double[] y = new double[256];

                y = calcularHistograma(R);

                for (int i = 127; i < 256; i++)
                {
                    somaYRFim += y[i];
                }

                // Nivel verde
                y = calcularHistograma(G);

                for (int i = 127; i < 256; i++)
                {
                    somaYGFim += y[i];
                }

                if (somaYRFim > somaYGFim)
                {
                    MessageBox.Show("A Maçã está madura!", "Maçã Madura", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label3.Text = "Madura";
                }
                else
                {
                    MessageBox.Show("A Maçã está verde!", "Maçã Verde", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label3.Text = "Verde";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Arquivo JPG(*.jpg)|*.jpg|Arquivo PNG (*.png)|*.png|Todos os Arquivos (*.*)|*.*";

                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    pictureBox1.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
