using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClientCB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient client;
        NetworkStream stream;
        Timer timer;
        int elapsedTime;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            try
            {               
                client = new TcpClient("127.0.0.1", 1000);
                stream = client.GetStream();

                // Invio delle credenziali al server
                string username = textBoxNome.Text;

                byte[] data = Encoding.ASCII.GetBytes(username);
                
                stream.Write(data, 0, data.Length);

                // Ricezione della risposta dal server (se necessario)
                // In questo esempio, supponiamo che il server invii una risposta
                byte[] responseData = new byte[1024];
                int bytesRead = stream.Read(responseData, 0, responseData.Length);
                string response = Encoding.ASCII.GetString(responseData, 0, bytesRead);

                // Visualizzazione della risposta
                if (response == "ok")
                {
                    Wait(client, stream);                  
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message);
            }
        }
        private void Wait(TcpClient client, NetworkStream stream)
        {
            textBoxNome.Visible = false;
            buttonGioca.Visible = false;   

            labelAttesa.Visible = true;

            byte[] responseData = new byte[1024];
            int bytesRead;
            string response;

            while (true)
            {
                bytesRead = stream.Read(responseData, 0, responseData.Length);
                response = Encoding.ASCII.GetString(responseData, 0, bytesRead);

                if (response == "gioca")
                {
                    Gioca();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void Gioca()
        {
            InizioPartita();
            while (elapsedTime < 30)
            {
                
            }
            FinePartita();
        }
        private void InizioPartita()
        {
            timerPartita.Start();
            timerSposta.Start();
            elapsedTime = 0;
        }
        private void FinePartita()
        {            
            button.Visible = false;
            buttonLogout.Visible = false;
            timerSposta.Stop();
            timerPartita.Stop();
        }

        private void Logout(TcpClient client, NetworkStream stream)
        {
            stream.Close();
            client.Close();

            textBoxNome.Visible = true;
            buttonGioca.Visible = true;
        }
        private void timerSposta_Tick(object sender, EventArgs e)
        {
            
        }

        private void timerPartita_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Logout(client, stream);
        }
    }
}
    

